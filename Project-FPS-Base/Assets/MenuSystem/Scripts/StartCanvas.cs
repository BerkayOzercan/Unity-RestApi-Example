using Assets.GameSystem.Scripts;
using Assets.GameSceneManager.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class StartCanvas : MonoBehaviour
    {
        [SerializeField]
        private Button _startBtn = null;
        [SerializeField]
        private Button _settingsBtn = null;
        [SerializeField]
        private Button _quitBtn = null;

        void Start()
        {
            _startBtn.onClick.AddListener(() => GameSceneManager.Scripts.GameSceneManager.Instance.Load());
            _settingsBtn.onClick.AddListener(() => Debug.Log("Settings"));
            _quitBtn.onClick.AddListener(() => Application.Quit());
        }
    }
}


