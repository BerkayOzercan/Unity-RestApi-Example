using System;

namespace Assets.GameSystem.Scripts
{
    [Serializable]
    public class LevelData
    {
        public string Name;
        public float Score;

        public LevelData(string name, float score)
        {
            Name = name;
            Score = score;
        }
    }
}


