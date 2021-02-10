using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuController : MonoBehaviour
{
    public OptionsButton optionsButton;

    public void Continue()
    {
        optionsButton.Toggle();
    }

    public void ToMainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Settings()
    {
        optionsButton.SettingsMenu(true);
    }

    public void BackToOptions()
    {
        optionsButton.SettingsMenu(false);
    }
}
