using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    SC sc;
    public float speed;
    public float dmg;
    public float life;
    public string team;

    void Start () {
        sc = GetComponent<SC>();
    }

    void Update () {
        sc.MoveForward(speed * Time.deltaTime);

        for (int i=World.enemies.Count-1; i>=0; --i) {
            GameObject enemy = World.enemies[i];
            if (sc.IsColliding(enemy)) {
                float overkill = enemy.GetComponent<Critter>().TakeDamage(dmg);
                if (overkill <= 0) {
                    World.RemoveMissile(gameObject);
                    return;
                }
            }
        }

        life -= Time.deltaTime;
        if (life < 0) {
            World.RemoveMissile(gameObject);
        }
    }

    public void SetParamsFromWeapon(Item weapon) {
        speed = weapon.missileSpeed;
        dmg = weapon.damage;
        life = weapon.maxDist / weapon.missileSpeed;
    }
}
