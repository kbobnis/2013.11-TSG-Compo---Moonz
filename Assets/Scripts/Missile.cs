using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Missile : MonoBehaviour {

    protected SC sc;
    protected SP sp;
	protected Sounds sounds; 
	public float speed;
    public float dmg;
    public float life;
    public string team;
    public float startingSpeed;
	public bool isPiercing;


    void Start () {
        sc = GetComponent<SC>();
        sp = GetComponent<SP>();
    }

    void Update () {
        sc.MoveForward(speed * Time.deltaTime);

		if (dmg < 0) { // zbijacz pocisków
			List<GameObject> missiles = World.missiles;
			for (int i=missiles.Count-1; i>=0; --i) {
				GameObject mis = missiles[i];
				if (mis != gameObject && sc.IsColliding(mis)) {
					if (mis.GetComponent<Missile>().team != team) {
						World.RemoveMissile(mis);
					}
				}
			}
		} else {
			List<GameObject> enemies = World.GetOppositeCollection(team);
			for (int i=enemies.Count-1; i>=0; --i) {
				GameObject enemy = enemies[i];
				if (sc.IsColliding(enemy)) {
					float overkill = enemy.GetComponent<Critter>().TakeDamage(dmg);
					if (overkill <= 0) {
						if (!isPiercing) {
							World.RemoveMissile(gameObject);
							return;
						}
					}
				}
			}
		}

        life -= Time.deltaTime;
        if (life < 0) {

            World.RemoveMissile(gameObject);
        }
    }

    public virtual void SetParamsFromWeapon(GameObject gameObjectWeapon) {
		Item weapon = gameObjectWeapon.GetComponent<Item>() as Item;
        speed = weapon.missileSpeed + startingSpeed + Random.Range(-weapon.missileSpeedVariance, weapon.missileSpeedVariance) ;
        dmg = weapon.damage;
        life = weapon.maxDist / weapon.missileSpeed;
		isPiercing = weapon.isMissilePiercing;
		sounds = gameObjectWeapon.GetComponent<Sounds>();
    }
}
