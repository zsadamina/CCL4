using System;
using System.Linq;
using Classes;

namespace Services
{
    public static class MazeService 
    {
        
        public static void GenerateMaze(Maze maze)
        {
            // suffle the walls to create a unique maze
            Random rand = new Random();
            var shuffledWalls = maze.walls.OrderBy(x => rand.Next()).ToList();

            // initialize the disjoint set with the cells
            var disjointSet = new DisjointSet<BaseCell>(maze.Cells.Cast<BaseCell>().ToList());

            // iterate through the walls, joining the cells if they are not already connected and removing the wall between them
            foreach (var wall in shuffledWalls)
            {
                // if the cells are not connected, we remove the wall and unite their sets
                if (!disjointSet.Connected(wall.Cell1, wall.Cell2))
                {
                    // remove the wall between the cells
                    maze.RemoveWall(wall);

                    // join the cells in the disjoint set
                    disjointSet.Union(wall.Cell1, wall.Cell2);
                }
            }
        }
    }
}