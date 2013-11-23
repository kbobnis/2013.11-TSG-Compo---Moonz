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
        sc.MoveForward(mp.speed);
        life -= Time.deltaTime;
        if (life < 0) {
            Destroy(gameObject);
        }
    }
}
