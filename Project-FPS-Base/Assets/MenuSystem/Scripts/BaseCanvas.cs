using System.Collections;
using Assets.GameSystem.Scripts;
using Assets.LevelSystem.Scripts;
using UnityEngine;

namespace Assets.MenuSystem.Scripts
{
    public class BaseCanvas : MonoBehaviour
    {
        protected private GameManager _gameManager;
        protected private LevelManager _levelManager;
        protected private ScoreManager _scoreManager;

        protected virtual void Awake()
        {
            StartCoroutine(Initial());
        }

        IEnumerator Initial()
        {
            yield return new WaitUntil(HasManager);
            _gameManager = GameManager.Instance;
            _levelManager = LevelManager.Instance;
            _scoreManager = ScoreManager.Instance;
        }

        bool HasManager()
        {
            return _gameManager == null &&
                    _levelManager == null &&
                    _scoreManager == null;
        }
    }
}


