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

            float fh = Input.GetAxis("FH1");
            float fv = Input.GetAxis("FV1");

            if (Mathf.Abs(fh) + Mathf.Abs(fv) > 0.5) {
                Vector3 shootDirection = Camera.main.transform.up * fv + Camera.main.transform.right * fh;
                Missiler.FireMissile("Missile1", gameObject, shootDirection);
            }
        }
    }
}
