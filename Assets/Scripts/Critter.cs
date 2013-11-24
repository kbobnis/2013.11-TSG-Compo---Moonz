using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Critter : MonoBehaviour {
    public float speed;
    public float power;
    public float hp;
    public float armorValue;
    public float shield;
    public float maxHp;
    public string team;
    public float points;
    public bool shieldActive;

	private float lastAttackTime;

    Eq eq;
    SC sc;

    void Start () {
        eq = GetComponent<Eq>();
        sc = GetComponent<SC>();
        lastAttackTime = 0;
    }
    
    void Update () {
    }

    public void Heal(float a) {
        hp = Mathf.Min(hp + a, maxHp);
    }

    public float TakeDamage(float dmg)
    {
		Sounds sounds = GetComponent<Sounds>();
		if (sounds != null){
			sounds.play(gameObject, sounds.gotHit);
		}

        if (eq != null) {
            float totalArmor = armorValue;
            Item armor = eq.GetArmor();
            if (armor != null) {
                totalArmor += armor.armorValue;
            }
            dmg = Mathf.Max(0, dmg - totalArmor * dmg);

            Item shield = eq.GetShield();
            if (shield != null && shieldActive) {
                float shieldedDmg = Mathf.Min(this.shield, shield.shieldValue * dmg);
                dmg = Mathf.Max(0, dmg - shieldedDmg);
                this.shield = Mathf.Max(0, this.shield - shieldedDmg);
            }
        }

        if (dmg > 0) {
            GameObject blood = Instantiate(Resources.Load("SmallBloodSplash")) as GameObject;
            blood.transform.localPosition = transform.localPosition;
            blood.transform.localRotation = transform.localRotation;
            Destroy(blood, 1);
        }

        float overkill = dmg - hp;
        hp -= dmg;
        if (hp <= 0) {
			Sounds _sounds = GetComponent<Sounds>();
			if (_sounds != null){
				_sounds.dieSound(gameObject);
			}
			gameObject.SendMessage("LetMeDie");
        }
        return overkill;
    }


    public void Attack(Vector3 target) {
        if (eq != null) {
            Item weapon = eq.GetWeapon();

            if (weapon != null) {
                if (Time.time - lastAttackTime > weapon.cooldown) {
					GameObject leftSlot = eq.leftSlot;
					Sounds sounds = leftSlot.GetComponent<Sounds>();
					if (leftSlot != null && sounds != null ){
						sounds.play(gameObject, sounds.spawn);
					}

                    if (weapon.missilePrefab != null) {
                        GameObject missileObj = Instantiate(weapon.missilePrefab) as GameObject;
                        missileObj.GetComponent<Missile>().SetParamsFromWeapon(weapon);
                        missileObj.GetComponent<Missile>().team = team;
                        missileObj.GetComponent<SC>().SetPosition(GetComponent<SC>().position);
                        missileObj.GetComponent<SC>().SetDirectionTo(target);
                        World.AddMissile(missileObj);
                    } else {
                        Vector3 attackDirection = target - sc.position;
                        Quaternion postRotation = Quaternion.FromToRotation(sc.direction, attackDirection.normalized);
                        GameObject slashFx = Instantiate(Resources.Load("Slash"), transform.localPosition, postRotation) as GameObject;
                        slashFx.transform.parent = transform;

                        List<GameObject> enemies = World.GetOppositeCollection(team);
                        for (int i=enemies.Count - 1; i>=0; --i) {
                            SC enemySc = enemies[i].GetComponent<SC>();
                            if (sc.GetAngleFromTo(attackDirection, enemySc.position) < weapon.angle && Vector3.Distance(sc.position, enemySc.position) < weapon.maxDist) {
                                enemies[i].GetComponent<Critter>().TakeDamage(weapon.damage);
                            }
                        }
                    }
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}
