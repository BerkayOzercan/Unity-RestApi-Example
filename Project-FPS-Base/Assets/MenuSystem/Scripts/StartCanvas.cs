using System;
using Assets.GameSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class StartCanvas : BaseCanvas
    {
        [SerializeField]
        private Button _startBtn = null;
        [SerializeField]
        private Button _settingsBtn = null;
        [SerializeField]
        private Button _quitBtn = null;

        void Start()
        {
            _startBtn.onClick.AddListener(() => _gameManager.ChangeState(GameStates.Play));
            _settingsBtn.onClick.AddListener(() => Debug.Log("Settings"));
            _quitBtn.onClick.AddListener(() => Application.Quit());
        }
    }
}


