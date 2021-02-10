using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private SceneNamesAndSounds[] _sceneNamesAndSounds;
    [SerializeField]
    private Slider _slider;
    private PlaySound _playSound;

    public AudioMixer _audioMixer;
    public string groupName;

    // Start is called before the first frame update
    void Start()
    {
        _playSound = GetComponent<PlaySound>();
        
        string sceneName = SceneManager.GetActiveScene().name;

        foreach (var item in _sceneNamesAndSounds)
        {
            if(item.sceneName == sceneName)
            {
                _playSound.Play(item.musicName);
                break;
            }
        }
    }


    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SliderValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SliderValueChanged);
    }

    private void SliderValueChanged(float value)
    {
        _audioMixer.SetFloat(groupName, value);
    }

    [Serializable]
    public class SceneNamesAndSounds
    {
        public string sceneName;
        public string musicName;
    }
}
