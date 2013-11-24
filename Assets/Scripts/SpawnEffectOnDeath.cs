using UnityEngine;
using System.Collections;

public class SpawnEffectOnDeath : MonoBehaviour {

    public GameObject prefab;
    public float destroyTimeout;

    void Start () {
    
    }
    
    void Update () {
    
    }

    void LetMeDie() {
        GameObject obj = Instantiate(prefab) as GameObject;
        obj.transform.localPosition = transform.localPosition;
        obj.transform.localRotation = transform.localRotation;
        Destroy(obj, destroyTimeout);
    }
}
