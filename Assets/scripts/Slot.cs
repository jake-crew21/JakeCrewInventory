using UnityEngine;
using System.Collections;

using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
    public SlotData data;
    public Inventory inventory;

    public void OnDrop(PointerEventData eventData) {
        GameObject droppedGameObject = eventData.pointerDrag;
        Item droppedItem = droppedGameObject.GetComponent<Item>();
        //is slot empty
        if (data.itemData == null) {
            
            //move item into this slot
            droppedItem.slotData.itemData = null;
            droppedItem.slotData = data;
        } else {
            //the slot is not empty
            //get the current item that occupies the slot
            GameObject currentItem = data.itemData.gameObject;
            //get the item script attached to that item
            Item item = currentItem.GetComponent<Item>();
            //set the item's slot to the dropped item's slot
            item.slotData = droppedItem.slotData;
            //set the parent of the current item to the dropped item
            item.transform.SetParent(droppedItem.slotData.gameObject.transform);
            //set the pos to the new parent
            item.transform.position = droppedItem.slotData.gameObject.transform.position;

            //set values inside of dropped item

            //set slot to new slot
            droppedItem.slotData = data;
            //set parent to new parent
            droppedItem.transform.SetParent(transform);
            //set pos to ne pos
            droppedItem.transform.position = transform.position;
        }
    }
}
