using UnityEngine;
using System.Collections;

public class TaranAI : MonoBehaviour {
    SC sc;
    Critter crit;
    float speedModifier;
    Vector3 currentTarget;
    float coolDown;

	void Start () {
        sc = GetComponent<SC>();
        crit = GetComponent<Critter>();
        speedModifier = Random.value + 0.5f;
        coolDown = 0;
	}
	
	void Update () {
        if (sc && crit) {
            GameObject target = World.GetNearestPlayer(sc);
            if (target != null) {
                if (coolDown <= 0) {
                    if (currentTarget.magnitude == 0) {
                        currentTarget = target.GetComponent<SC>().position;
                    }
                    float dist = Vector3.Distance(currentTarget, sc.position);
                    if (dist > 0.1f) {
                        sc.SetDirectionTo(currentTarget);
                        sc.MoveForward(crit.speed * speedModifier * Time.deltaTime);
                        crit.Attack(target.GetComponent<SC>().position);
                    } else {
                        currentTarget.Set(0,0,0);
                        coolDown = 1.5f;
                    }
                } else {
                    coolDown -= Time.deltaTime;
                }
            }
        }
	}

	void LetMeDie() {
		World.RemoveEnemy(this.gameObject);
		ZombieSpawner.EnemyDead(this.gameObject);

	}
}

