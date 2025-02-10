using System;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        //Level Scores
        public float LevelTime { get; set; }
        public int LevelCurrency { get; set; }
        public int LevelBonus { get; set; } = 1;
        private float _levelScore = 0f;

        //Level Timer
        public bool IsRunning { get; set; }
        public float Time { get; set; }

        private CanvasManager _canvasManager = null;

        protected override void Awake()
        {
            base.Awake();
            _canvasManager = GetComponent<CanvasManager>();
        }

        private void Update()
        {
            CountTime();
        }

        /// <summary>
        /// Get total level score
        /// </summary>
        /// <returns></returns>
        public float TotalLevelScore()
        {
            _levelScore = (LevelCurrency + LevelTime) * LevelBonus;
            return _levelScore;
        }

        /// <summary>
        /// Add point for current score
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(float score)
        {
            _levelScore += score;
        }

        /// <summary>
        /// Add currency amount
        /// </summary>
        /// <param name="amount"></param>
        public void AddCurrency(int amount)
        {
            LevelCurrency += amount;
        }

        /// <summary>
        /// Add Bonus
        /// </summary>
        /// <param name="amount"></param>
        public void AddBonus(int amount)
        {
            LevelBonus = amount;
        }

        private void CountTime()
        {
            if (IsRunning)
            {
                Time += UnityEngine.Time.deltaTime;

                _canvasManager.SetCounterText(Math.Round(Time, 1).ToString());
                _canvasManager.SetBonusText(LevelBonus.ToString());
                _canvasManager.SetCurrencyText(LevelCurrency.ToString());
            }
        }

        public void StopTimer()
        {
            IsRunning = false;
            LevelTime = Time;
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

