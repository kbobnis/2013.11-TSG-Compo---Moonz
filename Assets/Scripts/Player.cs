using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

        public string hname;
        public string vname;

    protected SC sc;
    protected Critter critter;

    // Use this for initialization
    void Start () {
        sc = GetComponent<SC>();
        critter = GetComponent<Critter>();
    }
    
    // Update is called once per frame
    void Update () {
        if (sc && critter) {
                        float h = Input.GetAxis(hname);
			float v = Input.GetAxis(vname);

            sc.MoveForward(  v * critter.speed);
            sc.MoveSide( h * critter.speed);
        }
    }
}
