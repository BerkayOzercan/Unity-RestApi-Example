using System;
using Assets.GameSystem.Scripts;
using TMPro;
using UnityEngine;

namespace Assets.MenuSystem.Scripts
{
    public class GameCanvas : BaseCanvas
    {
        [SerializeField]
        private TextMeshProUGUI _counterTimeText = null;
        [SerializeField]
        private TextMeshProUGUI _currencyText = null;
        [SerializeField]
        private TextMeshProUGUI _bonusText = null;


        private void GetScoreData(ScoreDto scoreDto)
        {
            _bonusText.text = $"Bonus: {scoreDto.Bonus}";
            _counterTimeText.text = $"Time: {scoreDto.Time}";
            _currencyText.text = $"Collects: {scoreDto.Currency}";
        }

        void OnEnable()
        {
            ScoreManager.GetScoreAction += GetScoreData;
        }

        void OnDisable()
        {
            ScoreManager.GetScoreAction -= GetScoreData;
        }
    }
}


