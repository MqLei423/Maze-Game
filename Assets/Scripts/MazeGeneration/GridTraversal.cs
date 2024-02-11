using System;
using System.Collections.Generic;
using System.Linq;

namespace ShareefSoftware
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;

        /*
         * Replace this with your documentation
         * 
         * Define your instance variables here
         */
        private bool[,] visited;

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
        }

        /*
         * Replace this with your documentation
         * 
         * DO NOT change the method signature
         * Define helper methods as 'private'
         */
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            /*
             * Implement your maze generation algorithm here
             * Use helper methods as needed
             */
            visited = new bool[grid.NumberOfRows, grid.NumberOfColumns];
            var frontierCells = new List<(int Row, int Column)>();
            frontierCells.Add((startRow, startColumn));

            if (frontierCells.Count > 0)
            {
                var randomIndex = new Random().Next(frontierCells.Count);
                var currentCell = frontierCells[randomIndex];
                frontierCells.RemoveAt(randomIndex);
                frontierCells.Distinct();

                if (!visited[currentCell.Row, currentCell.Column])
                {
                    visited[currentCell.Row, currentCell.Column] = true;

                    var neighbors = grid.Neighbors(currentCell.Row, currentCell.Column);

                    // Shuffle the neighbors to introduce randomness
                    //var shuffledNeighbors = neighbors.OrderBy(_ => Guid.NewGuid());

                    foreach (var neighbor in neighbors)
                    {
                        if (!visited[neighbor.Row, neighbor.Column])
                        {
                            frontierCells.Add(neighbor);

                            yield return ((currentCell.Row, currentCell.Column), neighbor);
                        }
                    }
                }
            }
        }
    }
}