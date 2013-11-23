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
            sc.SetDirectionTo(target.GetComponent<SC>().position);
            sc.MoveForward(crit.speed);
        }
	}
}
