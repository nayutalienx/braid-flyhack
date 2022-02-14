using System.Runtime.InteropServices;

namespace braid_flyhack_trainer.memory;

public abstract class MemoryParser
{
    protected int _processHandle;

    public IntPtr ReadPointer(IntPtr address)
    {
        return new IntPtr(BitConverter.ToInt32(readRawBytes(address, 4)));
    }

    public float ReadFloat(IntPtr address)
    {
        return BitConverter.ToSingle(readRawBytes(address, 4), 0);
    }

    public void WriteFloat(IntPtr address, float value)
    {
        writeRawBytes(address, BitConverter.GetBytes(value));
    }

    private byte[] readRawBytes(IntPtr address, int byteArraySize)
    {
        int bytesRead = 0;
        byte[] valueBuffer = new byte[byteArraySize];
        ReadProcessMemory(_processHandle, address.ToInt32(), valueBuffer,
            valueBuffer.Length, ref bytesRead);
        return valueBuffer;
    }

    private int writeRawBytes(IntPtr address, byte[] valueBuffer)
    {
        int bytesWritten = 0;
        WriteProcessMemory(_processHandle, address.ToInt32(), valueBuffer,
            valueBuffer.Length,
            ref bytesWritten);
        return bytesWritten;
    }

    [DllImport("kernel32.dll")]
    static extern bool ReadProcessMemory(int hProcess,
        int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress,
        byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);
}