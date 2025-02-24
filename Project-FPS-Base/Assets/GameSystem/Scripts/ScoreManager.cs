using System;
using Assets.MenuSystem.Scripts;

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

        private CanvasManager _canvasManager = null;
        private Target.TargetType _currentTargetType = Target.TargetType.None;

        private void Update()
        {
            CountTime();
        }

        public float GetLevelScore()
        {
            var score = (LevelCurrency + LevelTime) * LevelBonus;
            return (float)Math.Round(score, 2);
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

        private void CountTime()
        {
            if (IsRunning)
            {
                _canvasManager = CanvasManager.Instance;
                Time += UnityEngine.Time.deltaTime;

                _canvasManager.SetCounterText(Math.Round(Time, 1).ToString());
                _canvasManager.SetBonusText(LevelBonus.ToString());
                _canvasManager.SetCurrencyText(LevelCurrency.ToString());
            }
        }

        void OnPlaying(bool value)
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
            GameManager.OnPlayState += OnPlaying;
        }

        void OnDisable()
        {
            GameManager.OnPlayState -= OnPlaying;
        }
    }
}

