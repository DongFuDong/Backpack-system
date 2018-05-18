using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour ,IDropHandler{

	public int slotID;
	Inventory inv;



	public void OnDrop(PointerEventData eventData){

		ItemData droppenItem = eventData.pointerDrag.GetComponent<ItemData> ();
		if (inv.items [slotID].ID == -1) {

			inv.items [droppenItem.slotIndex] = new Item ();
			droppenItem.slotIndex = slotID;
			inv.items [slotID] = droppenItem.item;

		} else if (droppenItem.slotIndex != slotID) {
			Transform item = this.transform.GetChild (0);

			item.GetComponent<ItemData> ().slotIndex = droppenItem.slotIndex;

			item.transform.SetParent (inv.slots[droppenItem.slotIndex].transform);
			item.transform.position = item.transform.parent.position;

			inv.items [droppenItem.slotIndex] = item.GetComponent<ItemData>().item;
			droppenItem.slotIndex = slotID;
			inv.items [slotID] = droppenItem.item;
		}


	}
	// Use this for initialization
	void Start () {
		inv = GameObject.Find ("Itemdatabase").GetComponent<Inventory> ();
	}

	// Update is called once per frame
	void Update () {
		
		
	}
}
