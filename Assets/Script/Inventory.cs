using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public GameObject slot;
	public GameObject item;

	ItemDatabase itemdatabase;

	public List<GameObject> slots = new List<GameObject> ();

	public List<Item> items = new List<Item> ();

	GameObject slotPanel;

	// Use this for initialization
	void Start () {
		
		itemdatabase = GetComponent<ItemDatabase>();

		slotPanel = GameObject.Find("Slot Panel");

		for(int i=0;i<20;i++){
			slots.Add(Instantiate (slot));
			slots[i].transform.SetParent (slotPanel.transform);
			slots [i].GetComponent<Slot> ().slotID = i;
			items.Add (new Item ());
		}


		Additem (0);
		Additem (1);
		Additem (1);
		Additem (1);
		Additem (1);
		Additem (1);
	}


	public void Additem(int _id){
		
		Item itemToAdd = itemdatabase.FetchItemByID (_id);

		if (itemToAdd.Stackable == true && CheckItemExist(_id,0)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == _id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					if (data.amount < itemToAdd.StackMax) {
						data.amount++;
						data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					} else if(!CheckItemExist(_id,i+1)){
						CreatNewItem (itemToAdd);
						break;
					}

				}
			}

		} else {
			CreatNewItem (itemToAdd);
		}
	}

	bool CheckItemExist(int _id,int _i){
		for (int i = _i; i < items.Count; i++) {
			if (items [i].ID == _id) {
				return true;
			}
		}
		return false;
	}

	void CreatNewItem(Item itemToAdd){
		for (int i = 0; i < items.Count; i++) {
			if (items [i].ID == -1) {
				items [i] = itemToAdd;

				GameObject itemObj = Instantiate (item);

				itemObj.transform.SetParent (slots[i].transform);
				itemObj.transform.position = Vector2.zero;
				itemObj.name = items [i].Title;
				itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;

				itemObj.GetComponent<ItemData> ().item = itemToAdd;
				itemObj.GetComponent<ItemData> ().slotIndex = i;

				break;
			}
		}
	}

}
