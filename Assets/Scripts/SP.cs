using UnityEngine;
using System.Collections;

// Sphere positioner
public class SP : MonoBehaviour {
    SC sc;

    // Use this for initialization
    void Start () {
        sc = GetComponent<SC>();
    }
    
    // Update is called once per frame
    void Update () {
        if (sc != null) {
            transform.localPosition = sc.position * (World.radius + sc.height);
            transform.localRotation = Quaternion.LookRotation(sc.direction, sc.position);
        }
    }
}
