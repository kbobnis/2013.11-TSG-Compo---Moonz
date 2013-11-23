using UnityEngine;
using System.Collections;

public class DumbAI : MonoBehaviour {
    SC sc;
    Critter crit;

	// Use this for initialization
	void Start () {
        sc = GetComponent<SC>();
        crit = GetComponent<Critter>();
	}
	
	// Update is called once per frame
	void Update () {
        if (sc && crit) {
            GameObject target = World.GetNearestPlayer(sc);
            if (target != null) {
                sc.SetDirectionTo(target.GetComponent<SC>().position);
                GameObject nearest = World.GetNearestEnemy(sc);
                bool canMove = true;
                if (nearest != null && nearest != gameObject ) {
                    Vector3 nearestPos = nearest.GetComponent<SC>().position;
                    Vector3 nextPos = sc.GetForwardPosition(crit.speed);
                    if ( Vector3.Distance(nextPos, nearestPos) > Vector3.Distance(sc.position, nearestPos)) {
                        canMove = false;
                    }
                }
                if (canMove) {
                    sc.MoveForward(crit.speed);
                }
            }
        }
	}
}
