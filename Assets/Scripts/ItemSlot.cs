using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;     //using the event system for drag and drop.

// Display item in the slot, update image, make clickable when there is an item, invisible when there is not
public class ItemSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Item itemInSlot = null;
    RectTransform rectTransform; //Rect Transform for the item's position.
    CanvasGroup canvasGroup;
    public BoxCollider2D box; //Added Box Collider to items so, they don't overlap with each other.

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private int itemCount = 0; // Amount of a certain item you have.
    public int ItemCount
    {
        get
        {
            return itemCount;
        }
        set
        {
            itemCount = value;
        }
    }

    [SerializeField]
    private Image icon; // Icon of the item
    [SerializeField]
    private TMPro.TextMeshProUGUI itemCountText; // Text for the item count

    void Start()
    {
        RefreshInfo();
        box = GetComponent<BoxCollider2D>();
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void UseItemInSlot()
    {
        if(itemInSlot != null)
        {
            itemInSlot.Use();
            if (itemInSlot.isConsumable)
            {
                itemCount--;
                RefreshInfo();
            }
        }
    }

    public void RefreshInfo()
    {
        if(ItemCount < 1)
        {
            itemInSlot = null;
        }

        if(itemInSlot != null) // If an item is present
        {
            //update image and text
            itemCountText.text = ItemCount.ToString();
            icon.sprite = itemInSlot.icon;
            icon.gameObject.SetActive(true);
        } else
        {
            // No item
            itemCountText.text = "";
            icon.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData) //when you touch an item with a click
    {
        //Debug.Log("Ptr Down");
    }

    public void OnBeginDrag(PointerEventData eventData) // drag begins
    {
        //Debug.Log("Begin Drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        box.isTrigger = true;
    }

    public void OnEndDrag(PointerEventData eventData) // drag ends
    {
        //Debug.Log("End Drag");
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
        box.isTrigger = false;
    }

    public void OnDrag(PointerEventData eventData) // Dragging the item
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
