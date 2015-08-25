using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	private CharacterController controller;
	private Vector2 movement;
	private int jumpSpeed = 20;
	private float gravity = 10.0f;
	private bool ground = true;
	private bool bLadder = false;
	private bool tLadder = false;
	private bool climbing = false;
	private bool onLadder = false;
	private bool jumping = false;
	public bool dead = false;
	private bool facingRight = true;
	private AudioSource audio;
	public AudioClip jumpingSound;

	void Start () {
		animator = this.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
		audio = GetComponent <AudioSource> ();
	}

	void Update () {
		int moveDirection = 0;
		movement = Vector2.zero;
		if (Input.GetKey (KeyCode.D) && (ground || jumping)) {
			if(!jumping)
				animator.SetInteger ("Move", 1);
			movement.x += 1;
			moveDirection = 1;
		}
		if (Input.GetKey (KeyCode.A) && (ground || jumping)) {
			if(!jumping)
				animator.SetInteger ("Move", 1);
			movement.x -= 1;
			moveDirection = -1;
		} 
		if (Input.GetKey (KeyCode.W) && (bLadder || climbing )) {
			animator.SetInteger ("Move", 2);
			movement.y += 1;
			this.ground = false;
			this.climbing = true;
		}
		if (Input.GetKey (KeyCode.S) && (tLadder || bLadder || onLadder)) {
			animator.SetInteger ("Move", 2);
			movement.y -= 1;
			ground = false;
		}
		if (Input.GetKey (KeyCode.Space) && !bLadder && ground && !jumping) {
			animator.SetInteger ("Move", 3);
			PlaySound(jumpingSound);
			jumping = true;
			ground = false;
			movement.y += jumpSpeed;
		}

		if (!Input.anyKey && !jumping) {
			animator.SetInteger ("Move", 0);
		}
		if (jumping || (!bLadder && !climbing && !tLadder)) {
			movement.y -= 1.5f * gravity * Time.deltaTime;
		}

		if (moveDirection < 0 && facingRight) {
			flip ();
		} else if (moveDirection > 0 && !facingRight) {
			flip ();
		}

		controller.Move(movement * Time.deltaTime);
	}

	void OnTriggerStay (Collider coll) {
		if ((coll.gameObject.name == "Bottom") && coll.bounds.Contains (controller.bounds.min)) {
			bLadder=true;
			tLadder=false;
		}
		if (coll.gameObject.name == "Top") {
			if (coll.bounds.min.y < controller.bounds.min.y) {
				bLadder = false;
				tLadder = true;
				ground = true;
			} else {
				bLadder = true;
				tLadder = false;
				ground = false;
			}
		}
	}

	void OnTriggerExit (Collider coll) {
		if (coll.gameObject.name == "Bottom") {
			bLadder = false;
		}
		if (coll.gameObject.name == "Top") {
			climbing = false;
			ground = true;
		}
		if (coll.gameObject.name == "OnLadder") {
			onLadder = false;
		}
	}

	void OnTriggerEnter (Collider coll) {
		if (coll.gameObject.name == "OnLadder") {
			onLadder = true;
			ground = true;
		}
	}

	void OnControllerColliderHit (ControllerColliderHit hit){
		if (hit.gameObject.name == "Ground") {
			this.ground = true;
			this.climbing = false;
			this.jumping = false;
		}
		if (hit.gameObject.name == "Barrel") {
			dead = true;
			Debug.Log("Perdiste AMEO");
		}
		if (hit.gameObject.name == "Princess") {
			dead = false;
			Application.LoadLevel ("Win");
			Debug.Log("Ganaste AMEO");
		}
	}

	private void flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void PlaySound(AudioClip clip) {
		
		audio.PlayOneShot (clip, 0.75f);
	}

	public void LoadScene(int level) {
		Application.LoadLevel (level);
	}
}
