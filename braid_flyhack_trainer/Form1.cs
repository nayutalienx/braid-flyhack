using System.Runtime.InteropServices;
using braid_flyhack_trainer.service;

namespace braid_flyhack_trainer;

public partial class Form1 : Form
{

    private KeymapService _keymapService;
    
    public Form1()
    {
        InitializeComponent();
        AllocConsole();
        Console.WriteLine("started");
        _keymapService = new KeymapService();
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();
}