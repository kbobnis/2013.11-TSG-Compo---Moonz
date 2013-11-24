using UnityEngine;
using System.Collections;

public class Critter : MonoBehaviour {
    public float speed;
    public float power;
    public float hp;
    public float armorValue;
    public float shield;
    public float maxHp;
    public string team;

    public float lastAttackTime;

    public Eq eq;

    void Start () {
        eq = GetComponent<Eq>();
        lastAttackTime = 0;
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

    public void Attack(Vector3 target) {
        if (eq != null) {
            Item weapon = eq.GetWeapon();
            if (weapon != null) {
                if (Time.time - lastAttackTime > weapon.cooldown) {
                    if (weapon.missilePrefab != null) {
                        GameObject missileObj = Instantiate(weapon.missilePrefab) as GameObject;
                        missileObj.GetComponent<Missile>().SetParamsFromWeapon(weapon);
                        missileObj.GetComponent<Missile>().team = team;
                        missileObj.GetComponent<SC>().SetPosition(GetComponent<SC>().position);
                        missileObj.GetComponent<SC>().SetDirectionTo(target);
                        World.AddMissile(missileObj);
                    } else {
                        GameObject slashFx = Instantiate(Resources.Load("Slash"), transform.localPosition, transform.localRotation) as GameObject;
                        slashFx.transform.parent = transform;
                    }
                    lastAttackTime = Time.time;
                }
            }
        }
    }
}
