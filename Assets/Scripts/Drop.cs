using UnityEngine;
using System.Collections;

public class Drop : MonoBehaviour {

    public GameObject itemPrefab;
    float timeout;

    void Start () {
        timeout = 60;
    }
    
    void Update () {
        timeout -= Time.deltaTime;
        if (timeout < 0) {
            World.RemoveDrop(gameObject);
        }
    }

    public void TriggerTakeEffect() {
    }
}
