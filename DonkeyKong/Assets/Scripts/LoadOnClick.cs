using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource> ();	
	}

	public void LoadScene(int level) {
		Application.LoadLevel (level);
	}

	public void Exit() {
		Application.Quit ();
	}

	public void PlaySound(AudioClip clip) {
		audio.PlayOneShot (clip, 0.75f);
	}
}
