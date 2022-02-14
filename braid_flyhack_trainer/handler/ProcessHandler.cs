using System.Runtime.InteropServices;

namespace braid_flyhack_trainer.handler;

public class ProcessHandler
{
    public const int READ_WRITE_ACCESS = 0x1F0FFF;

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
}