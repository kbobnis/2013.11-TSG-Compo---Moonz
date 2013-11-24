using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Eq : MonoBehaviour 
{
	public List<GameObject> backpack = new List<GameObject>();

	public GameObject[] backpackFromGui;

	public GameObject upSlot;
	public GameObject leftSlot;
	public GameObject rightSlot;
	public GameObject downSlot;


	// Use this for initialization
	void Start () {
		foreach(var gameObject in backpackFromGui){
			backpack.Add(gameObject);
		}
		if (upSlot != null){
			backpack.Add(upSlot);
		}
		if (leftSlot != null){
			backpack.Add(leftSlot);
		}
		if (rightSlot != null){
			backpack.Add(rightSlot);
		}
		if (downSlot != null){
			backpack.Add(downSlot);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // message z dropa
    void AddItemToEq(GameObject tmp) {
        if (tmp.GetComponent<Item>().healing > 0) {
            GetComponent<Critter>().Heal(tmp.GetComponent<Item>().healing);
        } else {
            for(int i=0;i<backpack.Count;++i) {
                if (backpack[i].GetComponent<Item>().name  == tmp.GetComponent<Item>().name) {
                    return;
                }
            }
            backpack.Add(tmp);
        }
    }

    public void RemoveItem(Item what) {
        if (what == GetShield()) {
            rightSlot = null;
        }
        if (what == GetWeapon()) {
            leftSlot = null;
        }
        if (what == GetArmor()) {
            upSlot = null;
        }
        for(int i=0;i<backpack.Count;++i) {
            if (backpack[i].GetComponent<Item>().name  == what.name) {
                backpack.RemoveAt(i);
                return;
            }
        }
    }

	public void Wear(GameObject what) {
		Item item = what.GetComponent<Item>();

		switch(item.slotName){
			case Item.SLOT_UP:
				upSlot = what;
				break;
			case Item.SLOT_DOWN:
				downSlot = what;
				break;
			case Item.SLOT_LEFT:
				leftSlot = what;
				break;
			case Item.SLOT_RIGHT:
				rightSlot = what;
				break;
		}
		this.backpack.Add(what);

	}

    public Item GetWeapon() {
        if (leftSlot != null) {
            return leftSlot.GetComponent<Item>();
        }
        return null;
    }

    public Item GetArmor() {
        if (upSlot != null) {
            return upSlot.GetComponent<Item>();
        }
        return null;
    }

    public Item GetShield() {
        if (rightSlot != null) {
            return rightSlot.GetComponent<Item>();
        }
        return null;
    }

	public void ChangeSlot(string slotName)
	{
		GameObject tmp = null;

		switch(slotName){
			case Item.SLOT_UP:
				tmp = upSlot;
				break;
			case Item.SLOT_DOWN:
				tmp = downSlot;
				break;
			case Item.SLOT_RIGHT:
				tmp = rightSlot;
				break;
			case Item.SLOT_LEFT:
				tmp = leftSlot;
				break;
		}
			
		int lastIndex = 0;

		if (tmp != null) {
			lastIndex = backpack.LastIndexOf(tmp);
		}

		bool stillLooking = true;
		GameObject tmp2 = null;
		int j =0; 
		for(int i=lastIndex+1; stillLooking && j < backpack.Count  + 1; i++, j++) {
			i = i % backpack.Count;
			string slotNameTmp = (backpack[i] as GameObject).GetComponent<Item>().slotName;

			if (slotNameTmp == slotName) {
				stillLooking = false;
				tmp2 = backpack[i] as GameObject;
			}
			Debug.Log ("i is: " + i + ", j: " + j + ", slot name tmp is: " + slotNameTmp + ", slotName : "  + slotName);
		}
		Debug.Log ("got out of for, j: " + j);

		if (tmp2 != null){
			switch(slotName){
			case Item.SLOT_UP:
				upSlot = tmp2;
				break;
			case Item.SLOT_DOWN:
				downSlot = tmp2;
				break;
			case Item.SLOT_RIGHT:
				rightSlot = tmp2;
				break;
			case Item.SLOT_LEFT:
				leftSlot = tmp2;
				break;
			}
		}
	}
}
