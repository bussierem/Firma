using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	private float timeLeft = 1000;
	public GameObject timeText;

	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		timeText.gameObject.GetComponent<Text>().text = "Time Left: " + (int)timeLeft;
		if(timeLeft < 0.1f) {
			SceneManager.LoadScene("Prototype");
		}
	}

	void OnTriggerEnter2D(Collider2D trig) {
		if(trig.gameObject.CompareTag("Coin")) {
			Destroy(trig.gameObject);
			//DataManagement.dataManager.SaveData();
		}
	}
}
