using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public static float radius = 5;
    public static List<GameObject> players;
    public static List<GameObject> enemies;
    public static List<GameObject> missiles;

    void Start() {
        enemies = new List<GameObject>();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        missiles = new List<GameObject>();
    }
    
    void Update () {
		var names = Input.GetJoystickNames().Length;
		
		if (names != players.Count)
		{
			if (names == 1)
			{
				GameObject tmp = Resources.Load("Player", typeof(GameObject)) as GameObject;
				GameObject tmp2 = Instantiate(tmp) as GameObject;
				
				tmp2.GetComponent<Player>().inputSuffix = "1";
				players.Add(tmp2);
			}
			if (names == 2)
			{
				GameObject tmp = Resources.Load("Player", typeof(GameObject)) as GameObject;
				GameObject tmp2 = Instantiate(tmp) as GameObject;

				tmp2.GetComponent<Player>().inputSuffix = "2";
				players.Add(tmp2);
			}
		}

    }

    public static GameObject GetNearestInArray(SC sc, List<GameObject> array) {
        GameObject near = null;
        float nearDist = 0;
        for (int i=0; i<array.Count; ++i) {
            GameObject current = array[i];
            SC currSc = current.GetComponent<SC>();
            if (sc != currSc) {
                float currDist = Vector3.Distance(sc.position, currSc.position);
                if (near == null || currDist < nearDist) {
                    nearDist = currDist;
                    near = current;
                }
            }
        }
        return near;
    }

    public static GameObject GetNearestPlayer(SC sc) {
        return GetNearestInArray(sc, players);
    }

    public static void AddEnemy(GameObject e) {
		enemies.Add(e);
    }

    public static GameObject GetNearestEnemy(SC sc) {
        return GetNearestInArray(sc, enemies);
    }

    public static void AddMissile(GameObject m) {
        missiles.Add(m);
    }

	public static void RemoveEnemy(GameObject e) {
			Destroy(e);
			enemies.Remove(e);
	}
}
