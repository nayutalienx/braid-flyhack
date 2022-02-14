namespace braid_flyhack_trainer.processor;

public interface KeyEventProcessor
{
    void GlobalHookKeyPress(object sender, KeyPressEventArgs e);
}