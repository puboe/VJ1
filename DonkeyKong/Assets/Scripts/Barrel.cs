using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {

	private Animator animator;
	private Vector2 movement;
	public CapsuleCollider collider;
	bool facingRight = true;
	bool down = false;
	bool right = true;
	public bool onPool = true;
	public bool move = true;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		collider = this.GetComponents<CapsuleCollider> ()[0];
		movement = Vector2.right;
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			transform.Translate (movement * Time.deltaTime);
		}
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
		if (coll.gameObject.name == "Barrel") {
			GameManager.instance.loose = true;
		}
		if (coll.gameObject.name == "OnLadder") {
			if(Random.value < 0.5){	
				down = true;
			}
		}
	}

	void OnCollisionEnter (Collision coll){
		string name = coll.gameObject.name;
		if (name == "Ground") {
			if (Random.value < 0.75) {	
				movement = Vector2.right;
				right = true;
			} else {
				movement = Vector2.left;
				right = false;
			}
			animator.SetInteger ("Roll", 0);
		} else if (name == "Barrel" || name == "DonkeyKong" || name == "EndLevel" || name == "Princess") {
			if (right) {	
				movement = Vector2.left;
				right = false;
			} else {
				movement = Vector2.right;
				right = true;
			} 
		}else if (name == "Player") {
			GameManager.instance.loose = true;
		} else if (name == "BarrelEnd") {
			move = false;
			transform.position = new Vector2(999, 999);
			onPool = true;
		}
	}
}
