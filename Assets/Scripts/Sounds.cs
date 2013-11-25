using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	public AudioClip death;
	public AudioClip gotHit;
	public AudioClip spawn;
	public AudioClip move;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float play(GameObject source, AudioClip death) {

		if (death != null){

			AudioSource[] sources = source.GetComponents<AudioSource>();
			AudioSource goodSource = null;
			foreach(AudioSource audioSource in sources){
			 	if (!audioSource.isPlaying ) {
					goodSource = audioSource;
				}
			}
			if (goodSource == null){
				goodSource = source.AddComponent<AudioSource>();
			}

			goodSource.clip = death;
			goodSource.Play();
			return death.length;
		}
		return 0f;
	}

	public void dieSound(GameObject gameObject){
		GameObject obj = Instantiate(Resources.Load("DieSoundPrefab")) as GameObject;
		obj.transform.localPosition = gameObject.transform.localPosition;
		obj.transform.localRotation = gameObject.transform.localRotation;
		float time = play(obj, death);
		Destroy(obj, time);
	}

	public void Move(GameObject gameObject){

	}



}
