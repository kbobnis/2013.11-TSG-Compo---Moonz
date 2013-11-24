using UnityEngine;
using System.Collections;

public class SlashEffect : MonoBehaviour {

    float time;
    float totalTime;
    // Use this for initialization
    void Start () {
        totalTime = 0.3f;
        time = totalTime;
    }
    
    // Update is called once per frame
    void Update () {
        time -= Time.deltaTime;

        if (time < 0) {
            Destroy(gameObject);
        }

        float progress = 1.0f - time/totalTime;
        transform.localPosition = new Vector3(-Mathf.Cos(progress * Mathf.PI) * 0.6f, 0.3f, Mathf.Sin(progress * Mathf.PI) * 0.6f);
    }
}
