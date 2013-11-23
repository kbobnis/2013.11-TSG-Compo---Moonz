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

	public float TakeDamage(float dmg)
	{
        float overkill = dmg - hp;
		hp -= dmg;
		if (hp <= 0) {
			gameObject.SendMessage("LetMeDie");
		}
        return overkill;
	}
}
