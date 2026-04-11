using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject recipePopup; // Assign the popup panel in Inspector

    private bool isHoveringPopup = false;

    void Start()
    {
        if (recipePopup)
            recipePopup.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (recipePopup)
            recipePopup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Start a coroutine to check if we're hovering the popup too
        StartCoroutine(CheckPopupHover());
    }

    private System.Collections.IEnumerator CheckPopupHover()
    {
        // Wait a tiny frame to allow Unity to detect pointer over popup
        yield return null;

        // If pointer is NOT over the popup, hide it
        if (!isHoveringPopup)
        {
            recipePopup.SetActive(false);
        }
    }

    // Optional: detect if pointer is over popup
    public void PointerEnterPopup()
    {
        isHoveringPopup = true;
    }

    public void PointerExitPopup()
    {
        isHoveringPopup = false;
        recipePopup.SetActive(false);
    }
}