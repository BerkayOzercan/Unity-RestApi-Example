using GameData.Models;

namespace GameData.Services
{
    public static class PlayerService
    {
        static List<Player> Players { get; }

        static int nextId = 3;
        static PlayerService()
        {
            Players = new List<Player>
            {
                new Player { Id = 1, Name = "Craysen", Score = 999 },
                new Player { Id = 2, Name = "Trasqual", Score = 666 }
            };
        }

        public static List<Player> GetAll() => Players;

        public static Player? Get(int id) => Players.FirstOrDefault(p => p.Id == id);

        public static void Add(Player player)
        {
            player.Id = nextId++;
            Players.Add(player);
        }

        public static void Delete(int id)
        {
            var player = Get(id);
            if(player is null)
                return;

            Players.Remove(player);
        }

        public static void Update(Player player)
        {
            var index = Players.FindIndex(p => p.Id == player.Id);
            if(index == -1)
                return;

            Players[index] = player;
        }
    }
}