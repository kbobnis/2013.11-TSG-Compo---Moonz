using UnityEngine;
using System.Collections;

public class SunRotator : MonoBehaviour {

    void Update () {
        transform.Rotate(0,3*Time.deltaTime,0);
    }
}
