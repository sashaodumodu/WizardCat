using UnityEngine;

public class ShowMeshOnTrigger : MonoBehaviour
{
    // Drag the GameObject with the mesh you want to show here in the Inspector
    public GameObject gameObjectToShow;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player (optional)
        if (other.CompareTag("Player"))
        {
            // Enable the Mesh Renderer to show the component
            gameObjectToShow.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}