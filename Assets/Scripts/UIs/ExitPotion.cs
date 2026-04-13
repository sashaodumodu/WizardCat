using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenCraftingUI : MonoBehaviour
{
    
    public void OpenPotionUI()
    {
        // Load the PotionUI scene
        SceneManager.LoadScene("PotionUI", LoadSceneMode.Additive);
    }
}