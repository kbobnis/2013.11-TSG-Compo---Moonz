using UnityEngine;
using System.Collections;

public class Critter : MonoBehaviour {
    public float speed;
    public float power;
    public float hp;
    public float armorValue;
    public float shield;
	public float maxHp;

    void Start () {
    }
    
    void Update () {
    }

	public void takeDamage(float dmg)
	{
		this.hp -= dmg;
		if (this.hp <= 0) {
			this.gameObject.SendMessage("LetMeDie");
		}

	}
}
