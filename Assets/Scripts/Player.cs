using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	bool climbing;
	bool ground;
	bool topLadder;
	bool jumping;
	float maxJumpHeight = 0.5f;
	float jumpSpeed = 7.0f;
	float fallSpeed = 12.0f;
	public Vector2 groundPos;
	
	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
		climbing = false;
		ground = true;
		topLadder = false;
		jumping = false;
		groundPos = transform.position;
		maxJumpHeight = transform.position.y + maxJumpHeight;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey (KeyCode.D) && ground) {
			transform.Translate (Vector2.right * 0.5f * Time.smoothDeltaTime);
			animator.SetInteger ("Move", 1);
		} else if (Input.GetKey (KeyCode.A) && ground) {
			animator.SetInteger ("Move", 2);
			transform.Translate (Vector2.left * 0.5f * Time.smoothDeltaTime);
		} else if (Input.GetKey (KeyCode.W) && climbing) {
			animator.SetInteger ("Move", 0);
			ground = false;
			transform.Translate (Vector2.up * 0.5f * Time.smoothDeltaTime);
		} else if (Input.GetKey (KeyCode.S) && (!ground || topLadder)) {
			animator.SetInteger ("Move", 0);
			transform.Translate (Vector2.down * 0.5f * Time.smoothDeltaTime);
		} else if (Input.GetKey (KeyCode.X)&& ground  && !jumping) {
			jumping = true;
			StartCoroutine("Jump");
		} 

	}

	void OnTriggerStay2D(Collider2D other)
	{
		BoxCollider2D collider = other.gameObject.GetComponent<BoxCollider2D> ();
		BoxCollider2D tCollider = this.gameObject.GetComponent<BoxCollider2D> ();
		if(other.gameObject.name == "BottomLadder")
			climbing = collider.bounds.min.x < tCollider.bounds.min.x && tCollider.bounds.max.x < collider.bounds.max.x;
		if(other.gameObject.name == "TopLadder"){
			ground = collider.bounds.min.y < tCollider.bounds.min.y && tCollider.bounds.max.y < collider.bounds.max.y;
			climbing = collider.bounds.min.x < tCollider.bounds.min.x && tCollider.bounds.max.x < collider.bounds.max.x;
			topLadder = true;
		}

	}

	void OnTriggerEnter2D (Collider2D coll){
		if(coll.gameObject.name == "Ground"){
			ground = true;
		}
	}

	void OnTriggerExit2D (Collider2D coll){
		if(coll.gameObject.name == "TopLadder"){
			topLadder = false;
		}
	}

	IEnumerator Jump()
	{
		while(true)
		{
			if(transform.position.y >= maxJumpHeight)
				jumping = false;
			if(jumping){
				transform.Translate(Vector2.up * jumpSpeed * Time.smoothDeltaTime);
			}
			else 
			{
				transform.position = Vector2.Lerp(transform.position, groundPos, fallSpeed * Time.smoothDeltaTime);
				if((Vector2)transform.position == (Vector2)groundPos)
					StopAllCoroutines();
			}
			
			yield return new WaitForEndOfFrame();
		}
	}

}
