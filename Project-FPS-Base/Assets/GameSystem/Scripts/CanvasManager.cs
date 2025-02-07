using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class CanvasManager : Singleton<CanvasManager>
    {
        [Header("Canvases")]
        public GameObject ParentCanvas = null;
        public GameObject MenuCanvas = null;
        public GameObject PauseCanvas = null;
        public GameObject GameCanvas = null;
        public GameObject CrossHairCanvas = null;
    }
}


