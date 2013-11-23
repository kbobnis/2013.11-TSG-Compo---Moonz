using UnityEngine;
using System.Collections;

public class Eq : MonoBehaviour 
{
	public ArrayList backpack = new ArrayList();

	public GameObject upSlot;
	public GameObject leftSlot;
	public GameObject rightSlot;
	public GameObject downSlot;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	 * 	const string SLOT_UP = "up";
		const string SLOT_DOWN = "down";
		const string SLOT_LEFT = "left";
		const string SLOT_RIGHT = "right
	*/
	public void Add(GameObject tmp) {
		Item item = tmp.GetComponent<Item>();

		switch(item.slotName){
			case Item.SLOT_UP:
				upSlot = tmp;
				break;
			case Item.SLOT_DOWN:
				downSlot = tmp;
				break;
			case Item.SLOT_LEFT:
				leftSlot = tmp;
				break;
			case Item.SLOT_RIGHT:
				rightSlot = tmp;
				break;
		}
		this.backpack.Add(tmp);

	}

    public Item GetWeapon() {
        if (leftSlot != null) {
            return leftSlot.GetComponent<Item>();
        }
        return null;
    }
}
