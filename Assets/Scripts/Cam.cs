using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        if (World.players.Count > 0) {
            Vector3 up = new Vector3();
            Vector3 mean = new Vector3();
            for (int i=0; i<World.players.Count; ++i) {
                up += World.players[i].GetComponent<SC>().direction;
                mean += World.players[i].GetComponent<SC>().position;
            }
            up /= World.players.Count;
            up = up.normalized;
            mean /= World.players.Count;
            mean = mean.normalized;
            Camera.main.transform.localPosition = mean * World.radius * 3;
            Camera.main.transform.LookAt(mean, up);
        } else {
            Camera.main.transform.localPosition = Vector3.forward * World.radius * 3;
            Camera.main.transform.LookAt(Vector3.forward, Vector3.up);
        }
    }
}
