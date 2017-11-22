using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
	private int enemySpeed = 1;
	private int moveXDir = -1;
	private bool facingRight = false;

	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(moveXDir, 0));
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveXDir, 0) * enemySpeed;
		if(hit.distance < 0.25f) {
			Flip();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
		moveXDir *= -1;
	}
}
