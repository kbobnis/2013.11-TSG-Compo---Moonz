using UnityEngine;
using System.Collections;

public class Missiler : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
    
    }

    public static void FireMissile(string prefab, GameObject from, Vector3 to) {
        GameObject obj = Instantiate(Resources.Load(prefab, typeof(GameObject))) as GameObject;
        obj.GetComponent<SC>().SetPosition(from.GetComponent<SC>().position);
        obj.GetComponent<SC>().SetDirectionTo(to);
    }

    public static void FireMissile(string prefab, GameObject from, GameObject to) {
        FireMissile(prefab, from, to.GetComponent<SC>().position - from.GetComponent<SC>().position);
    }
}
