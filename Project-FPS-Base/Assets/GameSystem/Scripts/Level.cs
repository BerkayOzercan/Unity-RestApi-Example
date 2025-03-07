using System;

namespace Assets.GameSystem.Scripts
{
    [Serializable]
    public class Level
    {
        public string Name;
        public int Score;

        public Level(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}


