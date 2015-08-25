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
			GameManager.instance.Roll();
			yield return new WaitForSeconds(1.4f);
			animator.SetInteger ("Throw", 0);
		}
	}

}
