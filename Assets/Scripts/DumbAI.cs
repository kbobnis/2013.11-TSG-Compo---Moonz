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
            GameObject target = World.GetNearestPlayer(sc.position);
            if (target != null) {
                sc.SetDirectionTo(target.GetComponent<SC>().position);
                GameObject nearest = World.GetNearestEnemy(sc.position);
                bool canMove = true;
                if (nearest != null && nearest != gameObject ) {
                    Vector3 nearestPos = nearest.GetComponent<SC>().position;
                    Vector3 nextPos = sc.GetForwardPosition(crit.speed);
Debug.Log(Vector3.Distance(nextPos, nearestPos));
                    if ( Vector3.Distance(nextPos, nearestPos) < 1) {
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
