using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour {

    public static float radius = 5;
    public static List<GameObject> players;
    public static List<GameObject> enemies;
    public static List<GameObject> missiles;
    public static List<GameObject> drops;

    public static Dictionary<string,bool> inputSuffixes;
    public static AudioSource music;

    public static float score;

    void Start() {
        enemies = new List<GameObject>();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        missiles = new List<GameObject>();
        drops = new List<GameObject>();
        inputSuffixes = new Dictionary<string,bool>();
        inputSuffixes["1"] = false;
        inputSuffixes["2"] = false;
        score = 0;
        music = GameObject.Find("muzyczka").GetComponent<AudioSource>();
    }

    static void ExterminateTheWorld() {
        for (int i=0; i<players.Count; ++i) {
            Destroy(players[i]);
        }
        players.Clear();
        for (int i=0; i<enemies.Count; ++i) {
            Destroy(enemies[i]);
        }
        enemies.Clear();
        for (int i=0; i<missiles.Count; ++i) {
            Destroy(missiles[i]);
        }
        missiles.Clear();
        for (int i=0; i<drops.Count; ++i) {
            Destroy(drops[i]);
        }
        drops.Clear();
        inputSuffixes["1"] = false;
        inputSuffixes["2"] = false;
        score = 0;
    }
    
    void Update () {
        for (int k=1; k <=2; ++k) {
            string key = k.ToString();
            bool padActive = false;
            for (int i=0; i < 10; ++i) {
                padActive = padActive || Input.GetKey("joystick " + key + " button "+i);
            }
            if (inputSuffixes[key] == false && padActive) {
                GameObject newPlayer = Instantiate(Resources.Load("Player", typeof(GameObject))) as GameObject;
                newPlayer.GetComponent<Player>().inputSuffix = key;
                players.Add(newPlayer);
                if (key == "2") {
                    foreach (var renderer in newPlayer.GetComponentsInChildren<SkinnedMeshRenderer>()) {
                        renderer.material= Resources.Load("tankYellow") as Material;
                    }
                }
                inputSuffixes[key] = true;
                Debug.Log("Added player " + key);
            }
        }


    }

	void OnGUI(){
		if (players.Count == 0)
		{
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Resources.Load("logo", typeof(Texture))as Texture, ScaleMode.ScaleAndCrop);
			GUI.Label(new Rect(50, 50, 300, 70), "Press button 'A' to start");
            music.volume = 0;
		} else {
            music.volume = 0.15f;
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

    public static List<GameObject> GetOppositeCollection(string myTeam) {
        if (myTeam == "a") {
            return enemies;
        } else {
            return players;
        }
    }

    public static void RemovePlayer(GameObject g) {
        Destroy(g);
        players.Remove(g);

		if (players.Count == 0){
            World.ExterminateTheWorld();
            ZombieSpawner.ExterminateTheWorld();
		}
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

    public static GameObject GetNearestDrop(SC sc) {
        return GetNearestInArray(sc, drops);
    }

    public static void AddMissile(GameObject m) {
        missiles.Add(m);
    }

    public static void RemoveMissile(GameObject m) {
        Destroy(m);
        missiles.Remove(m);
    }

	public static void RemoveEnemy(GameObject e) {
        Destroy(e);
        enemies.Remove(e);
	}

    public static void AddDrop(GameObject d) {
        drops.Add(d);
    }

    public static void RemoveDrop(GameObject d) {
        Destroy(d);
        drops.Remove(d);
    }
}
