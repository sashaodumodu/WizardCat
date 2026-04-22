using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

// want it that there's a highlight hover effect so ppl know what they're about to choose. 
// So if it's selected too . 

public class InventoryHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image img;
    private Button btn;

    private Color normalColor = Color.white;
    private Color hoverColor = new Color(1f, 1f, 0.7f); // light highlight

    private bool isSelected = false;

    void Awake()
    {
        img = GetComponent<Image>();
        btn = GetComponent<Button>();
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (isSelected)
            img.color = Color.gray;
        else
            img.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
            img.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
            img.color = normalColor;
    }
}


