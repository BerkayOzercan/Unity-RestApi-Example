using Assets.PlayerSystem.Scripts;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class Collectable : MonoBehaviour
    {
        public CollectableTypes CollectableTypes = CollectableTypes.None;

        private ScoreManager _scoreManager = null;
        private bool _isCollect = false;

        private void Start()
        {
            _scoreManager = ScoreManager.Instance;
        }

        public void Collect()
        {
            if (_isCollect) return;
            if (CollectableTypes == CollectableTypes.Coin)
            {
                _isCollect = true;
                _scoreManager.AddCurrency(1);
                Destroy(gameObject);
            }
        }
    }

    public enum CollectableTypes
    {
        None,
        Coin
    }
}


