using braid_flyhack_trainer.processor;
using Gma.System.MouseKeyHook;

namespace braid_flyhack_trainer;

public class KeyboardHandler
{
    private IKeyboardMouseEvents m_GlobalHook;

    private KeyEventProcessor _eventProcessor;

    public KeyboardHandler(KeyEventProcessor keyEventProcessor)
    {
        m_GlobalHook = Hook.GlobalEvents();
        _eventProcessor = keyEventProcessor;
    }

    public void Subscribe()
    {
        m_GlobalHook.KeyPress += GlobalHookKeyPress;
    }

    public void Unsubscribe()
    {
        m_GlobalHook.KeyPress -= GlobalHookKeyPress;

        //It is recommened to dispose it
        m_GlobalHook.Dispose();
    }

    private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
    {
        _eventProcessor.GlobalHookKeyPress(sender, e);
    }
}