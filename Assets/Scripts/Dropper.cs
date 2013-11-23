using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour {

    public float dropChance;
    public GameObject[] dropsPrefabs;
    public float[] dropWeights;

    float weightsSum;

    void Start () {
        weightsSum = 0;
        for (int i=0; i<dropWeights.Length; ++i) {
            weightsSum += dropWeights[i];
        }
    }
    
    void Update () {
    
    }

    void LetMeDie() {
        if (Random.value < dropChance) {
            float pick = Random.value * weightsSum;
            for (int i=0; i<dropWeights.Length; ++i) {
                if (pick > dropWeights[i]) {
                    pick -= dropWeights[i];
                } else {
                    GameObject drop = Instantiate(dropsPrefabs[i]) as GameObject;
                    drop.GetComponent<SC>().SetPosition(GetComponent<SC>().position);
                    World.AddDrop(drop);
                    break;
                }
            }
        }
    }


}
