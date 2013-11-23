using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    SC sc;
    Critter critter;

    // Use this for initialization
    void Start () {
        sc = GetComponent<SC>();
        critter = GetComponent<Critter>();
    }
    
    // Update is called once per frame
    void Update () {
        if (sc && critter) {
            float h = Input.GetAxis("H1");
            float v = Input.GetAxis("V1");
            
            sc.MoveForward(v * critter.speed);
            sc.MoveSide(h * critter.speed);
        }
    }
}
