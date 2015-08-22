using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {
	
	private Animator animator;
	bool ladder;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		ladder = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ladder)
			transform.Translate (Vector2.down * 0.5f * Time.smoothDeltaTime);
		else
			transform.Translate (Vector2.right * 0.5f * Time.smoothDeltaTime);
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		BoxCollider2D collider = other.gameObject.GetComponent<BoxCollider2D> ();
		BoxCollider2D tCollider = this.gameObject.GetComponent<BoxCollider2D> ();
		if(other.gameObject.name == "TopLadder"){
			ladder = collider.bounds.min.x < tCollider.bounds.min.x && tCollider.bounds.max.x < collider.bounds.max.x;
			animator.SetBool ("Ladder", ladder);
		}
		
	}

	void OnCollisionEnter2D (Collision2D coll){
		ladder = false;
	}

	void OnTriggerEnter2D (Collider2D other){
		if(other.gameObject.name == "BottomLadder"){
			this.GetComponent<Rigidbody2D>().isKinematic = false;
		}
	}
	
}
