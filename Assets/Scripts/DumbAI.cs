﻿using UnityEngine;
using System.Collections;

public class DumbAI : MonoBehaviour {
    SC sc;
    Critter crit;
    float speedModifier;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SC>();
        crit = GetComponent<Critter>();
        speedModifier = Random.value + 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (sc && crit) {
            GameObject target = World.GetNearestPlayer(sc);
            if (target != null) {
                sc.SetDirectionTo(target.GetComponent<SC>().position);
                sc.MoveForward(crit.speed * speedModifier * Time.deltaTime);
            }
        }
	}

	void letMeDie() {
		World.RemoveEnemy(this.gameObject);
		ZombieSpawner.EnemyDead(this.gameObject);

	}
}
