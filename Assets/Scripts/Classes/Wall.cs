namespace Classes
{
    public class Wall
    {
        // cells connected by the wall
        public BaseCell Cell1 { get; }
        public BaseCell Cell2 { get; }

        // constructor for wall
        public Wall(BaseCell cell1, BaseCell cell2)
        {
            Cell1 = cell1;
            Cell2 = cell2;
        }
    }
}