using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y < -15) {
			Destroy(gameObject);
		}
	}
}
