using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {
    SC sc;
    Critter crit;

	void Start () {
        sc = GetComponent<SC>();
        crit = GetComponent<Critter>();
	}
	
	void Update () {
        if (sc && crit) {
            GameObject target = World.GetNearestPlayer(sc);
            if (target != null) {
                Vector3 targetPosition =target.GetComponent<SC>().position;
                sc.SetDirectionTo(targetPosition);
				crit.Attack(target.GetComponent<SC>().position);
            }
        }
	}

	void LetMeDie() {
		World.RemoveEnemy(this.gameObject);
		ZombieSpawner.EnemyDead(this.gameObject);
	}
}


