using System;
using System.Collections.Generic;
using Assets.PlayerSystem.Scripts;
using UnityEngine;

namespace Assets.GameSystem.Scripts
{
    public class ScoreBoard : Singleton<ScoreBoard>
    {
        PlayerDto _player = new PlayerDto { Rank = 34590, Name = "Berkay", Score = 2025412.0f };

        List<PlayerDto> _players = new List<PlayerDto>
        {
            new PlayerDto { Rank = 1, Name = "Charlie", Score = 1200.5f },
            new PlayerDto { Rank = 2, Name = "Alice", Score = 950.0f },
            new PlayerDto { Rank = 3, Name = "Charlie", Score = 1100.75f },
            new PlayerDto { Rank = 4, Name = "Diana", Score = 800.3f },
            new PlayerDto { Rank = 5, Name = "Ethan", Score = 1020.0f }
        };

        public List<PlayerDto> Players()
        {
            return _players;
        }

        public PlayerDto Player()
        {
            return _player;
        }
    }
}


