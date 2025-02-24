using System.Collections;
using System.Collections.Generic;
using Assets.GameSystem.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class ScoreBoardCanvas : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _playerDataText = null;
        [SerializeField]
        Transform _listParent = null;
        [SerializeField]
        TextMeshProUGUI _boardListItem = null;

        void Start()
        {
            SetPlayerData(ScoreBoard.Instance.Player());
            SetBoardList(ScoreBoard.Instance.Players());
        }

        void SetPlayerData(PlayerDto playerDto)
        {
            _playerDataText.text = $"{playerDto.Rank} {playerDto.Name} {playerDto.Score}";
        }

        void SetBoardList(List<PlayerDto> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                var player = Instantiate(_boardListItem, _listParent);
                player.text = $"{players[i].Rank} {players[i].Name} {players[i].Score}";
            }
            // Force layout update
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_listParent.GetComponent<RectTransform>());
        }
    }
}


