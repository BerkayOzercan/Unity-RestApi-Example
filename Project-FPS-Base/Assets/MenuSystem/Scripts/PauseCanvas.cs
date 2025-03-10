using Assets.GameSystem.Scripts;
using Assets.LevelSystem.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MenuSystem.Scripts
{
    public class PauseCanvas : MonoBehaviour
    {
        [SerializeField]
        Button _resumeBtn = null;
        [SerializeField]
        Button _restartBtn = null;
        [SerializeField]
        Button _settingsBtn = null;
        [SerializeField]
        Button _startMenuBtn = null;

        void Start()
        {
            LevelManager levelManager = LevelManager.Instance;
            _resumeBtn.onClick.AddListener(() => GameManager.Instance.IsPause = false);
            _restartBtn.onClick.AddListener(() => levelManager.Restart());
            _settingsBtn.onClick.AddListener(() => Debug.Log("Settings"));
            _startMenuBtn.onClick.AddListener(() => levelManager.LoadStartMenu());
        }
    }
}


