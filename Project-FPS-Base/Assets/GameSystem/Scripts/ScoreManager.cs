using System;

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

        protected override void Awake()
        {
            base.Awake();
            _canvasManager = GetComponent<CanvasManager>();
        }

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

