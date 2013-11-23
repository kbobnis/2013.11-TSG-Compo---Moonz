using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public static float radius = 5;
    public static List<GameObject> players;
    public static List<GameObject> enemies;

    void Start() {
        enemies = new List<GameObject>();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }
    
    void Update () {
    }

    public static GameObject GetNearestInArray(Vector3 position, List<GameObject> array) {
        GameObject near = null;
        float nearDist = 0;
        for (int i=0; i<array.Count; ++i) {
            GameObject current = array[i];
            float currDist = Vector3.Distance(position, current.GetComponent<SC>().position);
            if (near == null || currDist < nearDist) {
                nearDist = currDist;
                near = current;
            }
        }
        return near;
    }

    public static GameObject GetNearestPlayer(Vector3 position) {
        return GetNearestInArray(position, players);
    }

    public static void AddEnemy(GameObject e) {
        enemies.Add(e);
    }

    public static GameObject GetNearestEnemy(Vector3 position) {
        return GetNearestInArray(position, enemies);
    }
}
