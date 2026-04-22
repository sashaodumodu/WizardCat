using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToGame : MonoBehaviour
{
    public void ReturnHome()
    {
        // Unload the PotionUI scene
        SceneManager.UnloadSceneAsync("PotionUI");
    }
}