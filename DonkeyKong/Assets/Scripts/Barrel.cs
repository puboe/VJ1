using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	private Animator animator;
	private Vector2 movement;
	bool facingRight = true;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.G)) {
			animator.SetInteger ("Roll", 1);
			//this.transform.Translate(Vector2.down);
		} 
		if (Input.GetKey(KeyCode.H)) {
			animator.SetInteger("Roll", 0);
			//this.transform.Translate(Vector2.right);
		}
	}
}
