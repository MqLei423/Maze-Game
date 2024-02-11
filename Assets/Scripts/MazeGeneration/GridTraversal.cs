using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
        private readonly System.Random random;
        private List<(int Row, int Column)> cellsVisited;

        /// Constructor
        public GridTraversal(IGridGraph<T> grid)
        {
            this.grid = grid;
            random = new System.Random();
            cellsVisited = new List<(int Row, int Column)>();
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
            return PrimsEdgeSelect((startRow, startColumn));
            
        }

        private IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> PrimsEdgeSelect((int Row, int Column) cell)
        {
            var vertexNominees = new List<(int Row, int Column)>();
            var allNominees = new List<(int Row, int Column)>();

            if (!visited[cell.Row, cell.Column])
            {
                visited[cell.Row, cell.Column] = true;
                cellsVisited.Add((cell.Row, cell.Column));
                Debug.Log("Row " + cell.Row + " Column " + cell.Column);

                foreach (var frontierCell in cellsVisited)
                {
                    vertexNominees = createCutList(frontierCell);
                    if (vertexNominees.Count != 0)
                    {
                        foreach (var vertex in vertexNominees)
                            allNominees.Add(vertex);
                    }
                }

                // Randomly select an edge on the cut
                // Handle the situation when not encountering deadend
                if (vertexNominees.Count > 0)
                {
                    int rndIndex = random.Next(vertexNominees.Count);
                    var next = vertexNominees[rndIndex];
                    
                    foreach (var edge in PrimsEdgeSelect(next))
                    {
                        yield return edge;
                    }

                    vertexNominees.Remove(next);
                    //allNominees.Remove(next);
                    yield return ((cell.Row, cell.Column), next);
                    
                }
                else if (allNominees.Count > 0)
                {
                    int rndIndex = random.Next(allNominees.Count);
                    var next = allNominees[rndIndex];

                    foreach (var edge in PrimsEdgeSelect(next))
                    {
                        yield return edge;
                    }

                    //vertexNominees.Remove(next);
                    allNominees.Remove(next);

                    int splitingCellIndex = 0;
                    foreach(var nextNeighbor in grid.Neighbors(next.Row, next.Column))
                    {
                        for (int i = 0; i < cellsVisited.Count; i++)
                        {
                            if ((nextNeighbor.Row == cellsVisited[i].Row) && (nextNeighbor.Column == cellsVisited[i].Column))
                            {
                                splitingCellIndex = i;
                            }
                        }
                    }

                    yield return (cellsVisited[splitingCellIndex], next);
                }
            }
        }

        private List<(int Row, int Column)> createCutList((int Row, int Column) cell)
        {
            var nextStepNominees = new List<(int Row, int Column)>();

            foreach (var neighbor in grid.Neighbors(cell.Row, cell.Column))
            {
                if (!visited[neighbor.Row, neighbor.Column])
                {
                    nextStepNominees.Add(neighbor);
                }
            }

            return nextStepNominees;
        }
    }
}