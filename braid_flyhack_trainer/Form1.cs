using System.Runtime.InteropServices;
using braid_flyhack_trainer.service;

namespace braid_flyhack_trainer;

public partial class Form1 : Form
{
    private KeymapService _keymapService;

    public Form1()
    {
        InitializeComponent();
        Label braidLabel = new Label();
        braidLabel.Text = "Keys to fly: \nUP: [i] \nDOWN: [k] \nLEFT: [j] \nRIGHT: [l]";
        braidLabel.AutoSize = true;

        Controls.Add(braidLabel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        AllocConsole();
        Console.WriteLine("flyhack started");

        bool freeConsole = true;

        try
        {
            _keymapService = new KeymapService();
        }
        catch (Exception exception)
        {
            freeConsole = false;
        }

        if (freeConsole) FreeConsole();
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FreeConsole();
}