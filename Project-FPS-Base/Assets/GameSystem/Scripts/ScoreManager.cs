using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GameSystem.Scripts
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        //Level Scores
        public float LevelTime { get; set; }
        public int LevelCurrency { get; set; }
        public int LevelBonus { get; set; } = 0;
        private int _bonus = 0;

        //Level Timer
        public bool IsRunning { get; set; }
        public float Time { get; set; }

        public static Action<ScoreDto> GetScoreAction;
        Target.TargetType _currentTargetType = Target.TargetType.None;

        void Update()
        {
            UpdateData();
        }

        /// <summary>
        /// Add point for current score
        /// </summary>
        /// <param name="score"></param>
        public void AddBonus(Target.TargetType targetType)
        {
            if (_currentTargetType != targetType)
            {
                _currentTargetType = targetType;
                _bonus = 1;
            }
            else if (_currentTargetType == targetType)
            {
                _bonus++;
            }

            if (_bonus > LevelBonus)
            {
                LevelBonus = _bonus;
            }
        }

        /// <summary>
        /// Add currency amount
        /// </summary>
        /// <param name="amount"></param>
        public void AddCurrency(int amount)
        {
            LevelCurrency += amount;
        }

        void UpdateData()
        {
            if (IsRunning)
            {
                Time += UnityEngine.Time.deltaTime;

                GetScoreAction?.Invoke(GetScoreData());
            }
        }

        public ScoreDto GetScoreData()
        {
            ScoreDto Score = new ScoreDto
            {
                Time = Time,
                Bonus = _bonus,
                Currency = LevelCurrency,
                LevelScore = LevelScore()
            };
            return Score;
        }

        float LevelScore()
        {
            var score = (LevelCurrency - LevelTime) * LevelBonus;
            return (float)Math.Round(score, 2);
        }

        void SaveLevelScore(string level, float levelScore)
        {
            if (PlayerPrefs.HasKey(level))
            {
                if (PlayerPrefs.GetFloat(level) < levelScore)
                {
                    PlayerPrefs.SetFloat(level, levelScore);
                }
            }
        }


        void OnPlayState(bool value)
        {
            if (value)
                StartTimer();
            else
                StopTimer();
        }

        void OnWinState(bool value)
        {
            if (value)
                SaveLevelScore(SceneManager.GetActiveScene().name, LevelScore());
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

        void OnEnable()
        {
            GameManager.OnPlayState += OnPlayState;
            GameManager.OnWinState += OnWinState;
        }

        void OnDisable()
        {
            GameManager.OnPlayState -= OnPlayState;
            GameManager.OnWinState -= OnWinState;
        }
    }
}

