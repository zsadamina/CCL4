using System.Collections.Generic;
using System.Linq;
using Services;
using UnityEngine;

namespace Classes
{
    public class Maze
    {
        // maze dimensions
        public int Rows { get; }
        public int Columns { get; }

        // 2D array of cells, representing the maze
        public BaseCell[,] Cells { get; }

        // list of walls in the maze
        public List<Wall> walls { get; } = new List<Wall>();

        // constructor for maze
        public Maze(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            // initialize the cells array
            Cells = new BaseCell[Rows, Columns];

            // initialize the cells, one for each [row, column] pair
            // this is the code if only the base cell class provided is used
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Cells[i, j] = new Room(RoomType.Hallway, 1, j,i);
                }
            }

            // this is the code if more cell types are introduced

            /*
            var rand = new Random();

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        double chance = rand.NextDouble();

                        if (chance < 0.15)
                        {
                            // 15% chance this cell is desired cell type
                            Cells[i, j] = new OtherCell(i, j);
                        }
                        else
                        {
                            // Fallback to the default BasicCell.
                            Cells[i, j] = new Cell(i, j);
                        }
                    }
                }
             */
            Debug.Log(Rows + " " + Columns);

            // initialize the walls list
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    // horizontal walls, going from each cell to the right (unless it's the last column)
                    if (i < Rows - 1) walls.Add(new Wall(Cells[i, j], Cells[i + 1, j]));
                    // vertical walls, going from each cell down (unless it's the last row)
                    if (j < Columns - 1) walls.Add(new Wall(Cells[i, j], Cells[i, j + 1]));
                }
            }
        }


        public void GenerateMaze()
        {
            MazeService.GenerateMaze(this);
        }

        // helper function to remove the wall between two cells
        public void RemoveWall(Wall wall)
        {
            // get the two cells connected by the wall
            var cell1 = wall.Cell1;
            var cell2 = wall.Cell2;

            // if the cells are horizontal neighbors
            if (cell1.Row == cell2.Row)
            {
                //determine which cell is on the left and which is on right
                if (cell1.Column < cell2.Column)
                {
                    cell1.LeftWall = false;
                    cell2.RightWall = false;
                }
                else
                {
                    cell1.RightWall = false;
                    cell2.LeftWall = false;
                }
            }
            // if the cells are vertical neighbors
            else if (cell1.Column == cell2.Column)
            {
                // determine which cell is on top and which is on bottom
                if (cell1.Row < cell2.Row)
                {
                    cell1.BottomWall = false;
                    cell2.TopWall = false;
                }
                else
                {
                    cell1.TopWall = false;
                    cell2.BottomWall = false;
                }
            }
        }
            // method to print the maze in ASCII art
    public string PrintMaze()
    {
        // create a string builder to hold the output
        var output = new System.Text.StringBuilder();

        // add the top boundary of the maze
        output.AppendLine("+" + string.Concat(Enumerable.Repeat("---+", Columns)));

        // iterating through all the rows in the maze
        for (int i = 0; i < Rows; i++)
        {
            // create the lines for the current row
            var line1 = "|"; // the first line of the cell, containing the left wall and the cell content
            var line2 = "+"; // the second line of the cell, containing the bottom wall

            // iterating through all the columns in the maze
            for (int j = 0; j < Columns; j++)
            {
                // get the current cell
                var cell = Cells[i, j];

                // determine if the cell is the player's position or the finish point
                // isFinish also checks if there is a player in the maze, by checking if playerCol is not null, to not render when generating

                bool isFinish = i == Rows - 1 && j == Columns - 1;

                // Decide what goes in the cell.
                string cellContent = isFinish ? " X " : cell.CellContent;

                line1 += cellContent;

                // if the cell is the last column, we add the right wall
                if (j == Columns - 1)
                {
                    line1 += "|";
                }
                // if the cell is not in the last column, we add the left wall if it exists
                else
                {
                    line1 += cell.LeftWall ? "|" : " ";
                }
                // if the cell has a bottom wall, we add it to the second line, otherwise we add a +
                line2 += cell.BottomWall ? "---+" : "   +";
            }

            // add the lines to the output
            output.AppendLine(line1);
            output.AppendLine(line2);
        }
        return output.ToString();
    }
    }
}