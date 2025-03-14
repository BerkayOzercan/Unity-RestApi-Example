using Assets.GameSystem.Scripts;
using Assets.LevelSystem.Scripts;
using Assets.SaveSystem.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            _startBtn.onClick.AddListener(() => LevelManager.Instance.Load("Level 1"));
            _settingsBtn.onClick.AddListener(() => Debug.Log("Settings"));
            _quitBtn.onClick.AddListener(() => Application.Quit());
        }
    }
}


