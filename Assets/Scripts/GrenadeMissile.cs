using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeMissile : Missile {

    float totalLife;

    void Start () {
        sc = GetComponent<SC>();
    }

    void Update () {
        sc.MoveForward(speed * Time.deltaTime);
        float progress = 1.0f - life/totalLife;
        sc.height = Mathf.Sin(progress * Mathf.PI);

        if (progress >= 1) {
            List<GameObject> enemies = World.GetOppositeCollection(team);
            for (int i=enemies.Count-1; i>=0; --i) {
                GameObject enemy = enemies[i];
                if (sc.IsColliding(enemy)) {
                    enemy.GetComponent<Critter>().TakeDamage(dmg);
                }
            }
            World.RemoveMissile(gameObject);
            GameObject bigBoom = Instantiate(Resources.Load("BigBoom")) as GameObject;
            bigBoom.transform.localPosition = gameObject.transform.localPosition;
            bigBoom.transform.localRotation = gameObject.transform.localRotation;
            Destroy(bigBoom, 1);
        } else {
            life -= Time.deltaTime;
        }
    }

    override public void SetParamsFromWeapon(Item weapon) {
        speed = weapon.missileSpeed;
        dmg = weapon.damage;
        life = weapon.maxDist / weapon.missileSpeed;
        totalLife = life;
    }
}

