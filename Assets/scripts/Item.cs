using UnityEngine;
using System.Collections;
//use unity gui
using UnityEngine.UI;

using UnityEngine.EventSystems;
using System;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    public ItemData data;
    public SlotData slotData;
    public int amount = 1;

    private Text stackAmount;

    private Transform originalSlot;
    private Vector3 offSet;
    private CanvasGroup canvasGroup;

	// Use this for initialization
	void Start ()
    {
        stackAmount = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (stackAmount != null) {
            stackAmount.text = amount.ToString();
        }
	}

    public void OnBeginDrag(PointerEventData eventData) {
        //check if item is even in the slot
        if (data != null) {
            //store the original parent slot
            originalSlot = transform.parent;
            //set the parent of this item to the slot panel
            transform.SetParent(originalSlot.parent);
            //block raycast on begin drag
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        //check if item is in the slot
        if (data != null) {
            //set the pos of the item to the event data pos
            transform.position = eventData.position - (Vector2)offSet;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        //check if item is in slot
        if (data != null) {
            offSet = (Vector3)eventData.position - transform.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(slotData.gameObject.transform);
        transform.position = slotData.gameObject.transform.position;
        canvasGroup.blocksRaycasts = true;
    }
}
