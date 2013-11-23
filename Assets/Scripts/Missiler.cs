using UnityEngine;
using System.Collections;

public class Missiler : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
        CheckHits();
    }

    void CheckHits() {
        for (int i=World.missiles.Count-1; i>=0; --i) {
        }
    }

    public static void FireMissile(string prefab, GameObject from, Vector3 to) {
        GameObject obj = Instantiate(Resources.Load(prefab, typeof(GameObject))) as GameObject;
        obj.GetComponent<SC>().SetPosition(from.GetComponent<SC>().position);
        obj.GetComponent<SC>().SetDirectionTo(to);
        World.AddMissile(obj);
    }

    public static void FireMissile(string prefab, GameObject from, GameObject to) {
        FireMissile(prefab, from, to.GetComponent<SC>().position - from.GetComponent<SC>().position);
    }
}
