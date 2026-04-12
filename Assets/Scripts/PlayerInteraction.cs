 using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            Debug.Log("Hit: " + hit.collider.name);

            IInteraction interactable = hit.collider.GetComponent<IInteraction>();

            if (interactable != null)
            {
                interactable.Talk();
                interactable.Emotions();
            }
        }
    }
}