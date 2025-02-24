using System;
using Assets.PlayerSystem.Scripts;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class LevelPoint : MonoBehaviour
    {
        [SerializeField]
        public PointTypes PointType = PointTypes.Start;

        private GameManager _gameManager = null;


        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (PointType == PointTypes.End)
                {
                    _gameManager = GameManager.Instance;

                    _gameManager.GameWin();
                }
            }
        }
    }

    public enum PointTypes
    {
        Start,
        End
    }
}


