using UnityEngine;
using System.Collections;

public class NeutralizerMissileAnim : MonoBehaviour {
	float bornTime;

	// Use this for initialization
	void Start () {

		bornTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale= new Vector3(1,1,1) * (0.75f + Mathf.Sin((Time.time - bornTime) * 15) * 0.25f);
	}
}
