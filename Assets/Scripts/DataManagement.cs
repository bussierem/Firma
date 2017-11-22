using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagement : MonoBehaviour {
	public static DataManagement dataManager;
	public int data;

	void Awake() {
		if(dataManager == null) { 
			DontDestroyOnLoad(gameObject);
			dataManager = this;
		} else if (dataManager != this) {
			Destroy(gameObject);
		}
	}

	public void SaveData() {
		BinaryFormatter binForm = new BinaryFormatter();
		FileStream f = File.Create(Application.persistentDataPath + "/game.dat");
		GameData gd = new GameData();
		gd.data = data;
		binForm.Serialize(f, gd);
		f.Close();
	}

	public void LoadData() {
		// Loaded
		if(File.Exists(Application.persistentDataPath + "/game.dat")) {
			BinaryFormatter binForm = new BinaryFormatter();
			FileStream f = File.Create(Application.persistentDataPath + "/game.dat");
			GameData gd = (GameData)binForm.Deserialize(f);
			f.Close();
			data = gd.data;
		}
	}
}

[Serializable]
class GameData {
	public int data;
}