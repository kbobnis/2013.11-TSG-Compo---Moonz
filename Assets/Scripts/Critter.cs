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

    public float lastAttackTime;

    public Eq eq;
    public SC sc;

    void Start () {
        eq = GetComponent<Eq>();
        sc = GetComponent<SC>();
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

                        List<GameObject> enemies = World.GetOppositeCollection(team);
                        Vector3 attackDirection = target - sc.position;
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
