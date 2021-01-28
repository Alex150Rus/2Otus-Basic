using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildScript : MonoBehaviour
{
    public Text text;
    void Start()
    {
        #if UNITY_EDITOR
        text.text = "UNITY EDITOR";
        #elif UNITY_STANDALONE_WIN
        text.text = "WIN";
        #elif UNITY_WEBGL
        text.text = "WEBGL";
        #elif UNITY_ANDROID
        text.text = "ANDROID";
        #else
        text.text = "UNDEFINED";
        #endif

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (true)
        {
            Debug.LogError("TIMER " + Time.time);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
