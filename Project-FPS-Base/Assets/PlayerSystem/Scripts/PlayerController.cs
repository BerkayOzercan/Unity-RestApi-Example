using Assets.GameSystem.Scripts;
using UnityEngine;

namespace Assets.PlayerSystem.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private GameManager _gameManager = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LevelPoint levelPoint))
                DetectLevelPoint(levelPoint);
        }

        private void DetectLevelPoint(LevelPoint point)
        {
            if (point.PointType == PointTypes.End)
            {
                _gameManager = GameManager.Instance;

                _gameManager.GameWin();
            }
        }
    }
}


