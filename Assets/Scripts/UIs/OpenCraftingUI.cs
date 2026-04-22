using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPotion : MonoBehaviour
{
    public void OpenPotionUI()
    {
        // Load the PotionUI scene
        SceneManager.LoadScene("PotionUI", LoadSceneMode.Additive);
    }
}