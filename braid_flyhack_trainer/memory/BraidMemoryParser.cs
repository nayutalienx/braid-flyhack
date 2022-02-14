using System.Diagnostics;
using braid_flyhack_trainer.handler;

namespace braid_flyhack_trainer.memory;

public class BraidMemoryParser : MemoryParser
{
    private const int XOffset = 0x14;
    private const int YOffset = 0x18;
    private const int StatusOffset = 0x10;

    private int[] _possibleStaticPointerOffsetsForHeroPointer = new[]
    {
        0x1F2028,
        0x1F2054,
        0x1F2080,
        0x1F20AC,
        0x1F20D8,
        0x1F2104,
    };

    private const float HeroFlyStatus = 9.4f; // reversed value via cheat engine for changing Y coordinate
    private const float HeroWalkStatus = 2.0f;
    private const float HeroFallStatus = 0.0f;
    private const float HeroStatusInaccuracy = 0.1f;

    private IntPtr _braidProcessBaseAddress;

    private bool _initialized;

    public float X
    {
        get => ReadFloat(new IntPtr(FindBraidEntityPointer().ToInt64() + XOffset));
        set => WriteFloat(new IntPtr(FindBraidEntityPointer().ToInt64() + XOffset), value);
    }

    public float Y
    {
        get => ReadFloat(new IntPtr(FindBraidEntityPointer().ToInt64() + YOffset));
        set
        {
            Status = HeroFlyStatus;
            WriteFloat(new IntPtr(FindBraidEntityPointer().ToInt64() + YOffset), value);
            Status = HeroWalkStatus;
        }
    }

    private float Status
    {
        get => ReadFloat(new IntPtr(FindBraidEntityPointer().ToInt64() + StatusOffset));
        set => WriteFloat(new IntPtr(FindBraidEntityPointer().ToInt64() + StatusOffset), value);
    }


    public BraidMemoryParser()
    {
        init();
    }

    private void init()
    {
        if (_initialized) return;

        Process[] processes = Process.GetProcessesByName("braid");
        if (processes.Length == 0) throw new Exception("braid is not running");

        Process braidProcess = processes[0];


        foreach (ProcessModule processModule in braidProcess.Modules)
        {
            if (processModule.ModuleName.Equals("braid.exe"))
            {
                _braidProcessBaseAddress = processModule.BaseAddress;
                _processHandle = (int) ProcessHandler.OpenProcess(ProcessHandler.READ_WRITE_ACCESS, false,
                    braidProcess.Id);
                _initialized = true;
            }
        }
    }

    private IntPtr FindBraidEntityPointer()
    {
        if (!_initialized) throw new Exception("memory dump not initialized");

        List<IntPtr> ptrs = new List<IntPtr>();
        IDictionary<int, int> addressesCount = new Dictionary<int, int>();

        foreach (int offset in _possibleStaticPointerOffsetsForHeroPointer)
        {
            IntPtr pointerToHeroEntity = ReadPointer(new IntPtr(_braidProcessBaseAddress.ToInt64() + offset));

            float possiblePlayerStatus = ReadFloat(new IntPtr(pointerToHeroEntity.ToInt64() + StatusOffset));

            bool isHeroWalk = IsFloatInRangeWithInaccuracy(possiblePlayerStatus, HeroWalkStatus, HeroWalkStatus,
                HeroStatusInaccuracy);

            bool isHeroFly = IsFloatInRangeWithInaccuracy(possiblePlayerStatus, HeroFlyStatus, HeroFlyStatus,
                HeroStatusInaccuracy);

            bool isHeroFall = IsFloatInRangeWithInaccuracy(possiblePlayerStatus, HeroFallStatus, HeroFallStatus,
                HeroStatusInaccuracy);

            bool isValueMatchStatusParameter = isHeroFall || isHeroFly || isHeroWalk;

            if (isValueMatchStatusParameter)
            {
                bool pointerAdded = false;
                foreach (var (value, i) in ptrs.Select((value, i) => (value, i)))
                {
                    if (pointerToHeroEntity.Equals(value))
                    {
                        pointerAdded = true;

                        int previousCount = addressesCount[i];
                        addressesCount.Remove(i);
                        addressesCount.Add(i, previousCount + 1);
                        break;
                    }
                }

                if (!pointerAdded)
                {
                    addressesCount[ptrs.Count] = 1;
                    ptrs.Add(pointerToHeroEntity);
                }
            }
        }

        if (ptrs.Count == 0 || addressesCount.Count == 0)
        {
            throw new Exception("Entity addresses not found.");
        }

        return ptrs[addressesCount.OrderBy(x => x.Value).Last().Key];
    }

    private bool IsFloatInRangeWithInaccuracy(float value, float start, float end, float inaccuracy)
    {
        return value >= (start - inaccuracy) && value <= (end + inaccuracy);
    }
}