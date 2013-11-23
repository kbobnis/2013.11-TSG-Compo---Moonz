using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

    SC sc;
    MissileParams mp;

    void Start () {
        sc = GetComponent<SC>();
        mp = GetComponent<MissileParams>();
    }
    
    void Update () {
        sc.MoveForward(mp.speed);
    }
}
