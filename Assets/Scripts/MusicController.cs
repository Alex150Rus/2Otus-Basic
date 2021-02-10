using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private SceneNamesAndSounds[] _sceneNamesAndSounds;
    private PlaySound _playSound;
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

    [Serializable]
    public class SceneNamesAndSounds
    {
        public string sceneName;
        public string musicName;
    }
}
