using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemName; // "ywater", "ypoison", etc.

    private GameObject dragIcon;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create duplicate in Drag object
        GameObject dragParent = GameObject.Find("Drag");
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(dragParent.transform);
        dragIcon.transform.SetAsLastSibling();

        Image img = dragIcon.AddComponent<Image>();
        img.sprite = GetComponent<Image>().sprite;
        img.raycastTarget = false; // allow drop through

        dragIcon.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        dragIcon.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            dragIcon.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null) Destroy(dragIcon);
    }
}