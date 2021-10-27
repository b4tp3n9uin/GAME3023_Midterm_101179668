using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Script for the slots in the bag/container
public class Slot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) //When you drop an item in the slot, it's supposed to lock the item's position
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}

