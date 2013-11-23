using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public static float radius = 5;
    public static List<GameObject> players;
    public static List<GameObject> enemies;
    public static List<GameObject> missiles;

    public static Dictionary<string,GameObject> playerByInput; 
    public static Dictionary<string,bool> inputSuffixes;

    void Start() {
        enemies = new List<GameObject>();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        missiles = new List<GameObject>();
        playerByInput = new Dictionary<string,GameObject>();
        inputSuffixes = new Dictionary<string,bool>();
        inputSuffixes["1"] = false;
        inputSuffixes["2"] = false;
    }
    
    void Update () {

		if (players.Count <= 1) {
		string[] names = null; 
		bool pad2 = Input.GetKey(KeyCode.Joystick2Button0);
		if (pad2) {
			names = new string[] { "pad 1", "pad 2" };
		}
		else {
			names = new string[] { "pad 1"};
		}


        List<string> playersToRemove = new List<string>();
        foreach (string key in playerByInput.Keys) {
            playersToRemove.Add(key);
        }
        for (int i=0; i<names.Length; ++i) {
            if (!playerByInput.ContainsKey(names[i])) {
                foreach (string key in inputSuffixes.Keys) {
                    if (inputSuffixes[key] == false) {
                        GameObject newPlayer = Instantiate(Resources.Load("Player", typeof(GameObject))) as GameObject;
                        inputSuffixes[key] = true;
                        newPlayer.GetComponent<Player>().inputSuffix = key;
                        Debug.Log("Added player " + names[i] + " at pad "+ key);
                        players.Add(newPlayer);
                        playerByInput[names[i]] = newPlayer;
                        break;
                    }
                }
            }
            if (playersToRemove.Contains(names[i])) {
                playersToRemove.Remove(names[i]);
            }
        }
        for (int i=0; i<playersToRemove.Count; ++i) {
            Debug.Log("Removing player " + playersToRemove[i]);
            GameObject disconnectedPlayer = playerByInput[playersToRemove[i]];
            players.Remove(disconnectedPlayer);
            inputSuffixes[disconnectedPlayer.GetComponent<Player>().inputSuffix] = false;
            Destroy(disconnectedPlayer);
            playerByInput.Remove(playersToRemove[i]);
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
