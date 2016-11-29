using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

[System.Serializable]
public static class SaveLoadScript {
	public static CharacterStats playerStats;
	public static int playerLevel;
	public static int playerXP;
	public static bool saved = false;
	public static void SaveGame(CharacterStats playerStatss, int level, int xp) {

		SavePlayerStats (playerStatss);
		SavePlayerLevelAndXP (level, xp);
	}
	public static void LoadGame() {
		LoadPlayerStats ();
		LoadPlayerLevelAndXP ();
	}
	private static void SavePlayerLevelAndXP(int level, int xp) {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerLevel.gd");
		bf.Serialize (file, level);
		file.Close ();
		file = File.Create (Application.persistentDataPath + "/playerXP.gd");
		bf.Serialize (file, xp);
		file.Close ();
		saved = true;
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
	private static void LoadPlayerLevelAndXP() {
		if(File.Exists(Application.persistentDataPath + "/playerLevel.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerLevel.gd", FileMode.Open);

			playerLevel = (int)bf.Deserialize (file);
			file.Close();
		}
		if(File.Exists(Application.persistentDataPath + "/playerXP.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerXP.gd", FileMode.Open);

			playerXP = (int)bf.Deserialize (file);
			file.Close();
		}

	}
}
