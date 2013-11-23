using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
    GameObject[] players;

    // Use this for initialization
    void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 up = new Vector3();
        Vector3 mean = new Vector3();
        for (int i=0; i<players.Length; ++i) {
            up += players[i].GetComponent<SC>().direction;
            mean += players[i].GetComponent<SC>().position;
        }
        up /= players.Length;
        up = up.normalized;
        mean /= players.Length;
        mean = mean.normalized;
        Camera.main.transform.localPosition = mean * World.radius * 3;
        Camera.main.transform.LookAt(mean, up);
    }
}
