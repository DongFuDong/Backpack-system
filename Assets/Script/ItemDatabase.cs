using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour {

	private JsonData itemdata;

	private List<Item> database = new List<Item>();

	// Use this for initialization
	void Start () {
		//连接Json文件 但不能直接使用
		itemdata = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/items.json"));
		ConstructItemDatabase ();
		//Debug.Log (database[0].Desp);
		//Debug.Log (FetchItemByID (1).Title + FetchItemByID (1).Desp);
	}


	
	// Update is called once per frame
	void Update () {
		
	}

	void ConstructItemDatabase(){
		for (int i = 0; i < itemdata.Count; i++) {
			//存储在database
			database.Add (new Item ((int)itemdata [i] ["id"], itemdata [i] ["title"].ToString (), (int)itemdata [i] ["value"], itemdata [i] ["description"].ToString (), itemdata [i] ["madeby"].ToString (),itemdata [i] ["slug"].ToString (),(bool)itemdata [i] ["stackavle"],(int)itemdata [i] ["stackMax"]));

		}
	}
	public Item FetchItemByID(int _id){
		for (int i = 0; i < database.Count; i++) {
			if (_id == database [i].ID) {
				return database [i];
			}

		}
		return null;
	}




}


public class Item{
	public int ID{ get; set; }
	public string Title{ get; set; }
	public int Value { get; set; }
	public string Desp { get; set; }
	public string MadeBy  { get; set; }
	public Sprite Sprite{ get; set; }
	public bool Stackable { get; set;}
	public int StackMax{ get; set;}

	public Item(int _id, string _title, int _value, string _des, string _mader, string _slug,bool _stackable, int _stackMax){
		this.ID = _id;
		this.Title = _title;
		this.Value = _value;
		this.Desp = _des;
		this.MadeBy = _mader;
		this.Sprite = Resources.Load<Sprite> ("Items/" + _slug);
		this.Stackable = _stackable;
		this.StackMax = _stackMax;
	
	}
	public Item(){
		this.ID = -1;
	}

}