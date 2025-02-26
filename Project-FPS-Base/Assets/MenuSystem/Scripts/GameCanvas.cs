using Assets.GameSystem.Scripts;
using TMPro;
using UnityEngine;

namespace Assets.MenuSystem.Scripts
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _counterTimeText = null;
        [SerializeField]
        TextMeshProUGUI _currencyText = null;
        [SerializeField]
        TextMeshProUGUI _bonusText = null;


        void GetScoreData(ScoreDto scoreDto)
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


