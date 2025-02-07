using System;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class LevelPoint : MonoBehaviour
    {
        [SerializeField]
        public PointTypes PointType = PointTypes.Start;
    }

    public enum PointTypes
    {
        Start,
        End
    }
}


