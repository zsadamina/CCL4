using Classes;

namespace Services
{
    public static class RoomService
    {
        public static Room createRoom(RoomType type, int version, float spawnchance, int x, int y)
        {
            return new Room(type, version,x,y,spawnchance);
        }
        
        
    }
}