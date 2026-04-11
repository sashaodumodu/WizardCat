using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManage : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("TestScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}