using System;

namespace Assets.GameSystem.Scripts
{
    [Serializable]
    public class GameData
    {
        public string Name;
        public float Score;

        public GameData(string name, float score)
        {
            Name = name;
            Score = score;
        }
    }
}


