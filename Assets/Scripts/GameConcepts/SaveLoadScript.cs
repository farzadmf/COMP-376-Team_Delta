using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

[System.Serializable]
public static class SaveLoadScript {
	public static CharacterStats playerStats;

	public static bool saved = false;
	public static void SaveGame(CharacterStats playerStatss) {

		SavePlayerStats (playerStatss);
	}
	public static void LoadGame() {
		LoadPlayerStats ();

	}
	private static void SavePlayerStats(CharacterStats stats) {
		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Create (Application.persistentDataPath + "/playerStats.gd");
		bf.Serialize (file, stats);
		file.Close ();
		saved = true;
	}
	private static void LoadPlayerStats() {
		if(File.Exists(Application.persistentDataPath + "/playerStats.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerStats.gd", FileMode.Open);

			playerStats = (CharacterStats)bf.Deserialize (file);
			playerStats.Health = playerStats.TotalHealth;
			playerStats.Stamina = playerStats.TotalStamina;
			file.Close();
		}
	}
}
