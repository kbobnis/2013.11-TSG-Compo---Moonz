using UnityEngine;
using System.Collections;

// Sphere positioner
public class SP : MonoBehaviour {
    SC sc;
    public Quaternion rotation;

    void Start () {
        sc = GetComponent<SC>();
    }
    
    void Update () {
        if (sc != null) {
            transform.localPosition = sc.position * (World.radius + sc.height);
            transform.localRotation = Quaternion.LookRotation(sc.direction, sc.position) * rotation;
        }
    }
}
