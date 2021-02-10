using System;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public CanvasGroup optionsButtonsPanel;
    public CanvasGroup buttonsPanel;
    public CanvasGroup settings;
    enum State
    {
        Pressed,
        Released,
    }
    private State _state;

    void Start()
    {
        Utility.SetCanvasGroupEnabled(optionsButtonsPanel, false);
        Utility.SetCanvasGroupEnabled(settings, false);
        _state = State.Released;
    }

    public void Toggle()
    {
        switch(_state)
        {
            case State.Pressed:
                Utility.SetCanvasGroupEnabled(optionsButtonsPanel, false);
                Utility.SetCanvasGroupEnabled(settings, false);
                Utility.SetCanvasGroupEnabled(buttonsPanel, true);
                _state = State.Released;
                break;
            case State.Released:
                Utility.SetCanvasGroupEnabled(optionsButtonsPanel, true);
                Utility.SetCanvasGroupEnabled(buttonsPanel, false);
                _state = State.Pressed;
                break;
            default: 
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SettingsMenu(bool isActive)
    {
        Utility.SetCanvasGroupEnabled(optionsButtonsPanel, !isActive);
        Utility.SetCanvasGroupEnabled(settings, isActive);
    }
}
