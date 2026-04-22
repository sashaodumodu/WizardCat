using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    [Header("Settings UI")]
    public GameObject mainMenuUI;
    public GameObject settingsPopup;
    public GameObject creditsPanel;
    public GameObject controlsPanel;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("main");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        mainMenuUI.SetActive(false);

        settingsPopup.SetActive(true);
        creditsPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void ShowControls()
    {
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPopup.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}