﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	private CharacterController controller;
	private Vector2 movement;
	private float jumpSpeed = 11.0f;
	private float gravity = 10.0f;
	private bool ground = true;
	private bool bLadder = false;
	private bool tLadder = false;
	private bool climbing = false;
	private bool onLadder = false;
	private bool jumping = false;

	void Start () {
		animator = this.GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate () {
		movement = Vector2.zero;
		animator.SetInteger ("Move", 1);
		if (Input.GetKey (KeyCode.D) && ground) {
			animator.SetInteger ("Move", 1);
			movement.x += 1;
		}
		if (Input.GetKey (KeyCode.A) && ground) {
			animator.SetInteger ("Move", 1);
			movement.x -= 1;
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
			jumping = true;
			movement.y += 15;
		}else {
			animator.SetInteger ("Move", 0);
		}
		if (!bLadder && !ground && !tLadder && !climbing) {
			movement.y -= 1.5f * gravity * Time.deltaTime;
		}
		controller.Move(movement * Time.deltaTime);
	}

	void OnTriggerStay (Collider coll){
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

	void OnTriggerExit (Collider coll){
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

	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.name == "OnLadder") {
			onLadder = true;
		}
	}

	void OnControllerColliderHit (ControllerColliderHit hit){
		if (hit.gameObject.name == "Ground") {
			this.ground = true;
			this.climbing = false;
			this.jumping = false;
		}
	}


}
