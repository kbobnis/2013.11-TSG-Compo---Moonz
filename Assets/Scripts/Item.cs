using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public string _name;
	public Texture texture;

	public const string SLOT_UP = "up";
	public const string SLOT_DOWN = "down";
	public const string SLOT_LEFT = "left";
	public const string SLOT_RIGHT = "right";

	public string slotName;

	public float maxDist;
	public float angle;
	public float damage;
	public bool dualHand;
	public float cooldown;

    public float armorValue;

	public float shieldProtection;
	public float shieldRestoreTime;
    public float shieldTotalHp;
    public float shieldHp;

	public float speedChange;
    public float buffDuration;

    public GameObject missilePrefab;
    public float missileSpeed;

    public float healing;

    void Start () {
    
    }
    
    void Update () {
    
    }
}
