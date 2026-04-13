using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private TextMeshProUGUI promptText;
Ray ray = playerCamera.ScreenPointToRay(
        new Vector3(Screen.width / 2, Screen.height / 2)
    );

    void Start()
    {
        promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        InteractRay();
    }

    void InteractRay()
    {
         
    RaycastHit hit;

    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    {
        InteractableObject interactable = hit.collider.GetComponentInParent<InteractableObject>();

        if (interactable != null)
        {
            promptText.text = "Press E to " + interactable.GetInteractionText();
            promptText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }

            return;
        }
    }

    promptText.gameObject.SetActive(false);
    }
}