using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class FXController : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    public AudioMixer _audioMixer;
    public string groupName;

    private void OnEnable()
    {
        if(_slider) _slider.onValueChanged.AddListener(SliderValueChanged);
    }

    private void OnDisable()
    {
        if (_slider) _slider.onValueChanged.RemoveListener(SliderValueChanged);
    }

    private void SliderValueChanged(float value)
    {
        _audioMixer.SetFloat(groupName, value);
    }
}
