using braid_flyhack_trainer.memory;
using braid_flyhack_trainer.processor;

namespace braid_flyhack_trainer.service;

public class KeymapService : KeyEventProcessor
{
    private const char UP = 'i';
    private const char DOWN = 'k';
    private const char LEFT = 'j';
    private const char RIGHT = 'l';

    private const float STEP = 100;


    private BraidMemoryParser _braidMemoryParser;

    private KeyboardHandler _keyboardHandler;

    public KeymapService()
    {
        _braidMemoryParser = new BraidMemoryParser();
        _keyboardHandler = new KeyboardHandler(this);
        _keyboardHandler.Subscribe();
    }

    public void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
    {
        switch (Char.ToLower(e.KeyChar))
        {
            case UP:
                up();
                break;
            case DOWN:
                down();
                break;
            case LEFT:
                left();
                break;
            case RIGHT:
                right();
                break;
        }
    }

    private void up()
    {
        _braidMemoryParser.Y = _braidMemoryParser.Y + STEP;
    }

    private void down()
    {
        _braidMemoryParser.Y = _braidMemoryParser.Y - STEP;
    }

    private void left()
    {
        _braidMemoryParser.X = _braidMemoryParser.X - STEP;
    }

    private void right()
    {
        _braidMemoryParser.X = _braidMemoryParser.X + STEP;
    }
}