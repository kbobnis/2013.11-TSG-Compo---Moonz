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

			foreach(var tmp in World.enemies.ToArray())
			{
				GameObject enemy = tmp as GameObject;
				if (sc.IsColliding(enemy))
				{
					enemy.GetComponent<Critter>().takeDamage(mp.dmg);
				}
			}

        life -= Time.deltaTime;
        if (life < 0) {
            Destroy(gameObject);
        }
    }
}
