using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	private Animator animator;
	private Vector2 movement;
	private CapsuleCollider collider;
	bool facingRight = true;
	bool down = false;
	bool right = true;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		collider = this.GetComponents<CapsuleCollider> ()[0];
		movement = Vector2.right;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (movement * Time.deltaTime);
	}

	void OnTriggerStay (Collider coll){
		if (coll.gameObject.name == "OnLadder" && coll.bounds.min.x < collider.bounds.min.x) {
			if(down){	
				down = false;
				movement = Vector2.down;
				animator.SetInteger("Roll",1);
			}
		}
	}

	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.name == "OnLadder") {
			if(Random.value < 0.5){	
				down = true;
			}
		}
	}

	void OnCollisionEnter (Collision coll){
		if (coll.gameObject.name == "Ground") {
			if (Random.value < 0.5) {	
				movement = Vector2.right;
				right = true;
			} else {
				movement = Vector2.left;
				right = false;
			}
			animator.SetInteger ("Roll", 0);
		} else if (coll.gameObject.name == "EndLevel") {
			if (right) {	
				movement = Vector2.left;
				right = false;
			} else {
				movement = Vector2.right;
				right = true;
			}
			animator.SetInteger ("Roll", 0);
		} else if (coll.gameObject.name == "Player") {
			Debug.Log("Perdiste AMEO");
		}
	}
}
