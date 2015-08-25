using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	//public GameObject loadingImage;

	public void LoadScene(int level) {
		// Descomentar si tarda mucho en cargar el nivel.
		//loadingImage.SetActive (true);
		Application.LoadLevel (level);
	}

	public void Exit() {
		Application.Quit ();
	}
}
