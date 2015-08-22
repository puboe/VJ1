using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	private CharacterController controller;
	private Vector2 movement;
	private float jumpSpeed = 11.0f;
	private float gravity = 10.0f;
	private bool ground = true;

	void Start () {
		animator = this.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		movement = Vector2.zero;
		if (Input.GetKey (KeyCode.D)) {
			animator.SetInteger ("Move", 1);
			movement.x += 1;
		} 
		if (Input.GetKey (KeyCode.W)) {
			animator.SetInteger ("Move", 2);
		}
		if (Input.GetKey (KeyCode.Space)) {
			animator.SetInteger ("Move", 3);
			ground = false;
			movement.y += 1;
		}else {
			animator.SetInteger ("Move", 0);
		}
		if (!ground) {
			movement.y -= gravity * Time.deltaTime;
		}
		controller.Move(movement * Time.deltaTime);
	
	}
	

}
