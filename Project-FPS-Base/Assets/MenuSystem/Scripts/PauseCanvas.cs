using System;
using Assets.GameSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class PauseCanvas : BaseCanvas
    {
        [SerializeField]
        private Button _resumeBtn = null;
        [SerializeField]
        private Button _restartBtn = null;
        [SerializeField]
        private Button _settingsBtn = null;
        [SerializeField]
        private Button _startMenuBtn = null;

        void Start()
        {
            _resumeBtn.onClick.AddListener(() => _gameManager.ChangeState(GameStates.Play));
            _restartBtn.onClick.AddListener(() => Debug.Log("restart Level"));
            _settingsBtn.onClick.AddListener(() => Debug.Log("Settings"));
            _startMenuBtn.onClick.AddListener(() => _gameManager.ChangeState(GameStates.Start));
        }
    }
}


