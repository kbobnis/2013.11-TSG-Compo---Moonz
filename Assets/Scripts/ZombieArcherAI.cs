using UnityEngine;
using System.Collections;

public class ZombieArcherAI : MonoBehaviour {
    SC sc;
    Critter crit;
    float speedModifier;
    float lovedSideDirection;
    float minDist;

	void Start () {
        sc = GetComponent<SC>();
        crit = GetComponent<Critter>();
        speedModifier = Random.value + 0.5f;
        lovedSideDirection = Random.value < 0.5f ? 1 : -1;
        minDist = 0.8f + Random.value * 0.2f;
	}
	
	void Update () {
        if (sc && crit) {
            GameObject target = World.GetNearestPlayer(sc);
            if (target != null) {
                Vector3 targetPosition =target.GetComponent<SC>().position;
                sc.SetDirectionTo(targetPosition);
                float dist = Vector3.Distance(targetPosition, sc.position);
                if ( dist > 1) {
                    sc.MoveForward(crit.speed * speedModifier * Time.deltaTime);
                } else {
                    if (dist < minDist) {
                        sc.MoveForward(-crit.speed * speedModifier * Time.deltaTime);
                    }
                    sc.MoveSide(lovedSideDirection * crit.speed * speedModifier * Time.deltaTime);
                }
				crit.Attack(target.GetComponent<SC>().position);
            }
        }
	}

	void LetMeDie() {
		World.RemoveEnemy(this.gameObject);
		ZombieSpawner.EnemyDead(this.gameObject);

	}
}

