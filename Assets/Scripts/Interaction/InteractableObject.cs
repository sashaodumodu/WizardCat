using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string interactionText = "Interact";

    public string GetInteractionText()
    {
        return interactionText;
    }

    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}