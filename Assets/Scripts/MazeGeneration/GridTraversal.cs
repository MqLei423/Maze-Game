using System.Collections.Generic;

namespace ShareefSoftware
{
    /// Utility class to traverse a grid.
    public class GridTraversal<T>
    {

        private readonly IGridGraph<T> grid;

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
         * Prim's algorithm is implemented to traverse the grid. Rather than doing it iteratively, the algorithm is
         * implemented in a recursive manner, in which it goes on and handle the next vertex by calling itself.
         */
        public IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> GenerateMaze(int startRow, int startColumn)
        {
            visited = new bool[grid.NumberOfRows, grid.NumberOfColumns];
            return PrimsEdgeSelect((startRow, startColumn));
            
        }

        private IEnumerable<((int Row, int Column) From, (int Row, int Column) To)> PrimsEdgeSelect((int Row, int Column) cell)
        {
            var vertexNominees = new List<(int Row, int Column)>();
            var allNominees = new List<(int Row, int Column)>(); // This contains neighbor vertices on the other side of the cut

            if (!visited[cell.Row, cell.Column])
            {
                // Mark current cell as visited, then identify the cut.
                visited[cell.Row, cell.Column] = true;
                cellsVisited.Add((cell.Row, cell.Column));

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
                if (allNominees.Count > 0)
                {
                    // Select an edge randomly
                    int rndIndex = random.Next(allNominees.Count);
                    var next = allNominees[rndIndex];
                    bool[,] copy = visited;

                    // Find the visited vertex the edge extend from
                    int splitingCellRow = -1;
                    int splitingCellColumn = -1;
                    foreach(var nextNeighbor in grid.Neighbors(next.Row, next.Column))
                    {
                        if (copy[nextNeighbor.Row, nextNeighbor.Column])
                        {
                            splitingCellRow = nextNeighbor.Row;
                            splitingCellColumn = nextNeighbor.Column;
                            break;
                        }
                    }

                    //Continue on to next vertex
                    foreach (var edge in PrimsEdgeSelect(next))
                    {
                        yield return edge;
                    }

                    vertexNominees.Remove(next);
                    allNominees.Remove(next);

                    yield return ((splitingCellRow, splitingCellColumn), next);
                }
            }
        }

        /*
         * This method finds the part of cut around a given vertex,
         * this part forms the complete cut with other seperated cuts.
         */
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