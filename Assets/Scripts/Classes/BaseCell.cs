namespace Classes
{
    public abstract class BaseCell
    {
        // 'coordinates' of the cell within the maze
        public int Row { get; }
        public int Column { get; }

        // the content of the cell, used for ASCII art
        public string CellContent = "   ";

        /*
            the cell walls, boolean values indicating if they exist

            used for the ASCII art and collision with player

            initially, a cell has all 4 walls
        */
        public bool TopWall { get; set; } = true;
        public bool BottomWall { get; set; } = true;
        public bool LeftWall { get; set; } = true;
        public bool RightWall { get; set; } = true;

        // constructor for cell
        public BaseCell(int row, int column)
        {
            Row = row;
            Column = column;
        }

    }
}