using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    SC sc;
    MissileParams mp;
    float life;

    void Start () {
        sc = GetComponent<SC>();
        mp = GetComponent<MissileParams>();
        life = mp.life;
    }

    void Update () {
        sc.MoveForward(mp.speed * Time.deltaTime);

        for (int i=World.enemies.Count-1; i>=0; --i) {
            GameObject enemy = World.enemies[i];
            if (sc.IsColliding(enemy)) {
                enemy.GetComponent<Critter>().takeDamage(mp.dmg);
            }
        }

        life -= Time.deltaTime;
        if (life < 0) {
            Destroy(gameObject);
        }
    }
}
