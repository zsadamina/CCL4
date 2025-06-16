using System;

namespace Classes
{
    [Serializable]
    public class Room : BaseCell
    {
        public RoomType Type { get; set; }

        public int Version { get; set; }

        public float Spawnchance { get; set; }

        public float Rotation { get; set; }
        
        public Room(RoomType type = RoomType.LivingRoom, int version = 1,int x = 0, int y = 0,float rotation = 0, float spawnchance = 0):base(x,y)
        {
            this.Type = type;
            this.Version = version;
            this.Rotation = rotation;
            this.Spawnchance = spawnchance;
        }
    }
}