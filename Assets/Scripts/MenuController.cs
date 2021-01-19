using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    enum Screen
    {
        Main,
        Settings,
        SelectLevel,
    }

    public CanvasGroup mainScreen;
    public CanvasGroup settingsScreen;
    public CanvasGroup selectLevelScreen;

    void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(mainScreen, screen == Screen.Main);
        Utility.SetCanvasGroupEnabled(settingsScreen, screen == Screen.Settings);
        Utility.SetCanvasGroupEnabled(selectLevelScreen, screen == Screen.SelectLevel);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentScreen(Screen.Main);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectLevel()
    {
        SetCurrentScreen(Screen.SelectLevel);
    }

    public void OpenSettings()
    {
        SetCurrentScreen(Screen.Settings);
    }

    public void CloseSettings()
    {
        SetCurrentScreen(Screen.Main);
    }

    public void ExitGame()
    {
        //в редакторе не срабатывает, только в билде
        Application.Quit();
    }

}
