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
            _resumeBtn.onClick.AddListener(() => GameManager.Instance.ChangeState(GameStates.Play));
            _restartBtn.onClick.AddListener(() => Debug.Log("restart Level"));
            _settingsBtn.onClick.AddListener(() => Debug.Log("Settings"));
            _startMenuBtn.onClick.AddListener(() => LevelManager.Instance.LoadStartMenu());
        }
    }
}


