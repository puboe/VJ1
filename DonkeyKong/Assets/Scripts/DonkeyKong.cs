using UnityEngine;
using System.Collections;

public class DonkeyKong : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		// Ejecutar cada un intervalo de tiempo (depende de cuando se tiren los barriles).
		if (Input.GetKey (KeyCode.X)) {
			animator.SetInteger ("Throw", 1);
		} else {
			animator.SetInteger ("Throw", 0);
		} 
	}
}
