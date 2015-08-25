using UnityEngine;
using System.Collections;

public class DonkeyKong : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		StartCoroutine (BarrelRoll ());
	}

	IEnumerator BarrelRoll(){
		while (true) {
			animator.SetInteger ("Throw", 1);
			yield return new WaitForSeconds(3.0f);
			GameManager.instance.Roll();
			animator.SetInteger ("Throw", 0);
		}
	}

}
