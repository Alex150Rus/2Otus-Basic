using System;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public CanvasGroup optionsButtonsPanel;
    enum State
    {
        Pressed,
        Released,
    }
    private State _state;

    void Start()
    {
        Utility.SetCanvasGroupEnabled(optionsButtonsPanel, false);
        _state = State.Released;
    }

    public void Toggle()
    {
        switch(_state)
        {
            case State.Pressed:
                Utility.SetCanvasGroupEnabled(optionsButtonsPanel, false);
                _state = State.Released;
                break;
            case State.Released:
                Utility.SetCanvasGroupEnabled(optionsButtonsPanel, true);
                _state = State.Pressed;
                break;
            default: 
                throw new ArgumentOutOfRangeException();
        }
    }
}
