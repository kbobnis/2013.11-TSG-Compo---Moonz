using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Eq : MonoBehaviour 
{
	public List<GameObject> backpack = new List<GameObject>();

	public GameObject[] backpackFromGui;

	public GameObject upSlot;
	public GameObject leftSlot;
	public GameObject rightSlot;
	public GameObject downSlot;

	private long lastChangeTime;


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
	    
		for(int i=backpack.Count-1; i >= 0; i--) {
			if (backpack[i] != null){
				Item backpackItem = backpack[i].GetComponent<Item>();
				Item componentItem = tmp.GetComponent<Item>();
		        if (backpackItem.name  == componentItem.name && componentItem.slotName != Item.SLOT_DOWN) {
		            return;
		        }
			}
	    }
		Wear(tmp);
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
        if (what == GetBuff()) {
            downSlot = null;
        }
        for(int i=0;i<backpack.Count;++i) {
            if (backpack[i].GetComponent<Item>().name  == what.name) {
                backpack.RemoveAt(i);
                return;
            }
        }
    }

	private void WearIfEmpty(GameObject what) {
		Item item = what.GetComponent<Item>();

		switch(item.slotName){
		case Item.SLOT_UP:
			if (upSlot == null)
				upSlot = what;
			break;
		case Item.SLOT_DOWN:
			if (downSlot == null)
				downSlot = what;
			break;
		case Item.SLOT_LEFT:
			if (leftSlot == null)
				leftSlot = what;
			break;
		case Item.SLOT_RIGHT:
			if (rightSlot == null)
				rightSlot = what;
			break;
		}
		this.backpack.Add(what);
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

	public Item GetBuff(){
		if (downSlot != null){
			return downSlot.GetComponent<Item>();
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
		long diff = DateTime.Now.Ticks - lastChangeTime;
		if (diff < 1500000) //nie chcemy za czesto zmieniac, bo get axis bedzie przychodzic co kazdego update
			return ;

		lastChangeTime = DateTime.Now.Ticks;
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
			string slotNameTmp = null;
			if (backpack[i] != null){
				slotNameTmp = (backpack[i] as GameObject).GetComponent<Item>().slotName;
			}

			if (slotNameTmp == slotName) {
				stillLooking = false;
				tmp2 = backpack[i] as GameObject;
			}
		}

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
