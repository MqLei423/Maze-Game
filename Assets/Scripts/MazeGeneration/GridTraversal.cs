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
            visited = new bool[grid.NumberOfRows, grid.NumberOfColumns];
            return PrimsTraverse((startRow, startColumn));
            
        }

        private IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> PrimsTraverse((int Row, int Column) cell)
        {
            var nextStepNominees = new List<(int Row, int Column)>();
            if (!visited[cell.Row, cell.Column])
            {
                visited[cell.Row, cell.Column] = true;
                foreach (var neighbor in grid.Neighbors(cell.Row, cell.Column))
                {
                    if (!visited[neighbor.Row, neighbor.Column])
                    {
                        nextStepNominees.Add(neighbor);
                    }
                }
                var random = new Random();

                while (nextStepNominees.Count > 0)
                {
                    int rndIndex = random.Next(nextStepNominees.Count);
                    var next = nextStepNominees[rndIndex];
                    nextStepNominees.Remove(next);

                    yield return ((cell.Row, cell.Column), next);
                }
            }
        }

    }
}