using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public static float radius = 5;
    public static GameObject[] players;

    void Awake() {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    
    void Update () {
    }

    public static GameObject GetNearestInArray(Vector3 position, GameObject[] array) {
        GameObject near = null;
        float nearDist = 0;
        for (int i=0; i<array.Length; ++i) {
            GameObject current = array[i];
            float currDist = Vector3.Distance(position, current.GetComponent<SC>().position);
            if (current == null || currDist < nearDist) {
                nearDist = currDist;
                near = current;
            }
        }
        return near;
    }

    public static GameObject GetNearestPlayer(Vector3 position) {
        return GetNearestInArray(position, players);
    }
}
