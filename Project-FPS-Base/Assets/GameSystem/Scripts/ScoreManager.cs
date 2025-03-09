using System;
using UnityEngine.SceneManagement;

namespace Assets.GameSystem.Scripts
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int LevelBonus { get; set; } = 0;

        public float LevelTime { get; set; }
        public int LevelCurrency { get; set; }
        public bool IsRunning { get; set; }
        public float Time { get; set; }
        public static Action<ScoreEntities> GetScoreAction;

        int _bonus = 0;
        Target.TargetType _currentTargetType = Target.TargetType.None;

        void Update()
        {
            UpdateTime();
            UpdateEntities();
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

        void UpdateEntities()
        {
            if (IsRunning)
                GetScoreAction?.Invoke(GetEntitiesData());
        }

        public ScoreEntities GetEntitiesData()
        {
            ScoreEntities entities = new ScoreEntities
            {
                Time = Time,
                Bonus = _bonus,
                Currency = LevelCurrency,
            };
            return entities;
        }

        public float GetScore()
        {
            return LevelCurrency + LevelBonus - (Time / 10f);
        }

        public GameData GameData()
        {
            return new GameData(SceneManager.GetActiveScene().name, GetScore());
        }

        void UpdateTime()
        {
            if (IsRunning)
                Time += UnityEngine.Time.deltaTime;

        }

        void OnPlayState(bool value)
        {
            if (value)
                StartTimer();
            else
                StopTimer();
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
        }

        void OnDisable()
        {
            GameManager.OnPlayState -= OnPlayState;
        }
    }
}


