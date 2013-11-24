using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	public AudioClip death;
	public AudioClip gotHit;
	public AudioClip spawn;


	public void play(AudioSource audioSource, AudioClip death) {
		if (death != null && audioSource != null) {
			audioSource.PlayOneShot(death);
		}
	}
}
