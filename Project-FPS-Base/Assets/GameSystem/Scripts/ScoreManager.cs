using Unity.VisualScripting;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        //Level Scores
        public float LevelScore { get; set; }
        public float LevelTime { get; set; }
        public int LevelCurreny { get; set; }

        //Level Timer
        public bool IsRunning { get; set; }
        public float Time { get; set; }

        private void Update()
        {
            CountTime();
        }

        /// <summary>
        /// Add point for current score
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(float score)
        {
            LevelScore += score;
            Debug.Log($"Score: {LevelScore}");
        }

        private void CountTime()
        {
            if (IsRunning)
            {
                Time += UnityEngine.Time.deltaTime;
                Debug.Log("Time: " + Time);
            }
        }

        public void StopTimer()
        {
            IsRunning = false;
        }

        public void StartTimer()
        {
            IsRunning = true;
        }

        public void ResetTimer()
        {
            Time = 0f;
        }
    }
}

