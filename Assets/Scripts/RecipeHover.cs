using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject recipePopup; // popup panel in Inspector

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
        //  check if we're hovering the popup too
        StartCoroutine(CheckPopupHover());
    }

    private System.Collections.IEnumerator CheckPopupHover()
    {
        // Wait a tiny bit to allow Unity to detect pointer over popup
        yield return null;

        // If pointer is NOT over the popup, hide it
        if (!isHoveringPopup)
        {
            recipePopup.SetActive(false);
        }
    }

    // see if pointer is over popup
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