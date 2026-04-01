using UnityEngine;
using TMPro;

public class InteractWithE : MonoBehaviour
{
    [Header("Interaction")] //Dialogue system
    [SerializeField] public GameObject targetPanel; //Dialogue system
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed - interaction works!");
            TogglePanel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    
    //Dialogue system
    public void TogglePanel()
    {
        if (targetPanel != null)
        {
            // Toggles the active state of the panel (true/false)
            targetPanel.SetActive(!targetPanel.activeSelf);
        }
    }
}