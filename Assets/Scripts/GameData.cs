using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public string PlayerName;
    public List<GameScore> ScoreList;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ScoreList = new List<GameScore>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        Load();
    }

    public void Save()
    {
        SaveData data = new SaveData()
        {
            PlayerName = PlayerName,
            GameScores = ScoreList
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "savefile.json"), json);
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "savefile.json");

        if (!File.Exists(filePath))
            return;

        string json = File.ReadAllText(filePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        PlayerName = data.PlayerName;
        ScoreList = data.GameScores;
    }


    [Serializable]
    public class SaveData
    {
        public string PlayerName;
        public List<GameScore> GameScores;
    }

    [Serializable]
    public class GameScore : IComparable<GameScore>
    {
        public string PlayerName;
        public int Score;

        public int CompareTo(GameScore other)
        {
            return other.Score.CompareTo(Score);
        }
    }
}
