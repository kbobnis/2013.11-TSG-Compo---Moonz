﻿using UnityEngine;
using System.Collections;

public class DumbAI : MonoBehaviour {
    SC sc;
    Critter crit;
    float speedModifier;

	void Start () {
        sc = GetComponent<SC>();
        crit = GetComponent<Critter>();
        speedModifier = Random.value + 0.5f;
        GetComponent<Animator>().speed = speedModifier * 0.75f;
	}
	
	void Update () {
        if (sc && crit) {
            GameObject target = World.GetNearestPlayer(sc);
            if (target != null) {
                sc.SetDirectionTo(target.GetComponent<SC>().position);
                sc.MoveForward(crit.speed * speedModifier * Time.deltaTime);
				crit.Attack(target.GetComponent<SC>().position);
            }
        }
	}

	void LetMeDie() {

		ZombieSpawner.EnemyDead(this.gameObject);
		World.RemoveEnemy(this.gameObject);

	}
}
