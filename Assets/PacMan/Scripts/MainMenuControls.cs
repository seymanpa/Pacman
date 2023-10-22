using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuControls : MonoBehaviour
{
    public GameObject SettingsMenu;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        bool isActive = SettingsMenu.activeSelf;
        SettingsMenu.SetActive(!isActive);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
