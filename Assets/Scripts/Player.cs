using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public string inputSuffix;

    protected SC sc;
    protected Critter critter;

    // Use this for initialization
    void Start () {
        sc = GetComponent<SC>();
        critter = GetComponent<Critter>();
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        if (sc && critter) {
            float h = Input.GetAxis("H"+inputSuffix);
            float v = Input.GetAxis("V"+inputSuffix);

            sc.MoveForward(  v * critter.speed);
            sc.MoveSide( h * critter.speed);

            float fh = Input.GetAxis("FH"+inputSuffix);
            float fv = Input.GetAxis("FV"+inputSuffix);

            if (Mathf.Abs(fh) + Mathf.Abs(fv) > 0.5) {
                Vector3 shootDirection = Camera.main.transform.up * fv + Camera.main.transform.right * fh;
                Missiler.FireMissile("Missile1", gameObject, shootDirection);
            }
        }
    }
}
