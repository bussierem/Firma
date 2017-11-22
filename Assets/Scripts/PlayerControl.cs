using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private int playerSpeed = 5;
    private int jumpPower = 70;
    public float moveX;
    public float moveY;
    private bool falling = false;

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
        PlayerMove();
		PlayerRaycast();
        falling = (moveY > 0.0f);
		if(moveY > 0.0f) {
			GetComponent<Animator>().SetBool("isJumping", true);
			GetComponent<Animator>().SetBool("isFalling", false);
			GetComponent<Animator>().SetBool("isMoving", false);
		} else if (moveY < 0.0f) {
			GetComponent<Animator>().SetBool("isJumping", false);
			GetComponent<Animator>().SetBool("isFalling", true);
			GetComponent<Animator>().SetBool("isMoving", false);
		} else {
			if(moveX != 0.0f) {
				GetComponent<Animator>().SetBool("isJumping", false);
				GetComponent<Animator>().SetBool("isFalling", false);
				GetComponent<Animator>().SetBool("isMoving", true);
			} else {
				GetComponent<Animator>().SetBool("isJumping", false);
				GetComponent<Animator>().SetBool("isFalling", false);
				GetComponent<Animator>().SetBool("isMoving", false);
			}
		}
		// Y Movement:
		//		UP:  isJumping, cancel isFalling/isMoving
		//		DOWN: isFalling, cancel isJumping/isMoving
		//		NONE:
		// 			X Movement:  isMoving
		//			ELSE:  cancel ALL
    }

    void PlayerMove() {
        // CONTROLS
        moveX = Input.GetAxis("Horizontal");
        moveY = gameObject.GetComponent<Rigidbody2D>().velocity.y;
        if(Input.GetButtonDown("Jump") && !falling) {
            Jump();
        }
        // ANIMATIONS
		//GetComponent<Animator>().SetBool("isMoving", (moveX != 0));
		// PLAYER DIRECTION
        if(moveX < 0.0f) {
			GetComponent<SpriteRenderer>().flipX = true;
		} else if (moveX > 0.0f) {
			GetComponent<SpriteRenderer>().flipX = false;
		}
        // PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump() {
        // JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
		//GetComponent<Animator>().SetBool("isJumping", true);
    }

	void PlayerRaycast() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
		if(hit != null && hit.collider != null && 
		   hit.distance < 0.5f && hit.collider.tag == "Enemy") {
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * ((int)-moveY + 50)); // "Bounce" the player in a direction (up)
			GameObject hitThing = hit.collider.gameObject;
			hitThing.GetComponent<BoxCollider2D>().enabled = false;
			hitThing.GetComponent<EnemyControl>().enabled = false;
			hitThing.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30);
			hitThing.GetComponent<Rigidbody2D>().gravityScale = 5;
		}
	}
}
