using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        private string filePath = "game";
        public ScoreData scoreData = new ScoreData();

        public void AddScore()
        {
            scoreData.levelDatas.Add(LevelManager.Instance.LevelData());
            SaveScores();
        }

        public void SaveScores()
        {
            string json = JsonUtility.ToJson(scoreData, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Scores saved: " + filePath);
        }

        public void LoadScores()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                scoreData = JsonUtility.FromJson<ScoreData>(json);
                Debug.Log("Scores loaded.");
            }
            else
            {
                Debug.Log("No saved scores found.");
            }
        }

        public List<LevelData> GetScores()
        {
            return scoreData.levelDatas;
        }
    }
}

