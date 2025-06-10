using System;

namespace Classes
{
    [Serializable]
    public class Room
    {
        
        public RoomType Type { get; set; }

        public int Version { get; set; }

        public float Spawnchance { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public Room(RoomType type = RoomType.LivingRoom, int version = 1,int x = 0, int y = 0, float spawnchance = 0)
        {
            this.Type = type;
            this.Version = version;
            this.x = x;
            this.y = y;
            this.Spawnchance = spawnchance;
        }
    }
}