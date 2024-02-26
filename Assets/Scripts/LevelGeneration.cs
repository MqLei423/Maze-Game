using System.Collections.Generic;
using UnityEngine;

namespace ShareefSoftware
{
    public class LevelGeneration : MonoBehaviour
    {
        [SerializeField] TileStyle tileStyle;
        [SerializeField] int numberOfRows = 10;
        [SerializeField] int numberOfColumns = 10;
        [SerializeField] List<GameObject> barrierPrefabs;
        [SerializeField] List<GameObject> pathPrefabs;
        [SerializeField] List<GameObject> powerUps;
        [SerializeField] private float cellWidth;
        [SerializeField] private float cellHeight;
        [SerializeField] private Transform parentForNewObjects;
        [SerializeField] int randomSeed = 0;
        [SerializeField] GameObject DestSign;
        [SerializeField] GameObject Player;
        private List<(int Row, int Column)> deadEnds;

        private void Awake()
        {
            System.Random random = CreateRandom();
            var maze = new Maze(numberOfRows, numberOfColumns, random);
            deadEnds = findDeadEnds(maze);
            IGridGraph<bool> occupancyGrid = ConvertMazeToOccupancyGraph(maze);
            CreatePrefabs(random, occupancyGrid);
            MakeExit();
            spawnPlayer();
            CreateCoins(random, occupancyGrid, deadEnds);
        }

        private void spawnPlayer()
        {
            Vector3 startPos = new Vector3(cellWidth , 2f, 0f);
            Player.transform.position = startPos;
        }

        private void MakeExit()
        {
            Vector3 exitPos = new Vector3(numberOfRows * cellWidth * 2 - cellWidth, 0, numberOfColumns * cellWidth * 2);
            DestSign.transform.position = exitPos;
            //Instantiate(DestSign, exitPos, Quaternion.identity);
        }

        // Create a list containing all the deadends in the maze
        private List<(int Row, int Column)> findDeadEnds(Maze maze)
        {
            List<(int Row, int Column)> result = new List<(int Row, int Column)>();
            foreach ((int row, int column) in MazeQuery.DeadEnds(maze))
            {
                result.Add((row, column));
            }

            return result;
        }

        private void CreateCoins(System.Random random, IGridGraph<bool> occupancyGrid, List<(int Row, int Column)> deadEnds)
        {
            var pathFactory = new GameObjectFactoryRandomFromList(pathPrefabs, random) { Parent = parentForNewObjects };
            var wallFactory = new GameObjectFactoryRandomFromList(barrierPrefabs, random) { Parent = parentForNewObjects };
            var powerUpFactory = new GameObjectFactoryRandomFromList(powerUps, random) { Parent = parentForNewObjects };
            var coinFactory = new CoinFactory(cellWidth, cellHeight)
            {
                PrefabFactoryDeadEnd = powerUpFactory
            };
            coinFactory.CreatePrefabs(occupancyGrid, deadEnds);
        }

        private void CreatePrefabs(System.Random random, IGridGraph<bool> occupancyGrid)
        {
            var pathFactory = new GameObjectFactoryRandomFromList(pathPrefabs, random) { Parent = parentForNewObjects };
            var wallFactory = new GameObjectFactoryRandomFromList(barrierPrefabs, random) { Parent = parentForNewObjects };
            var gridFactory = new GridGameObjectFactory(cellWidth, cellHeight)
            {
                PrefabFactoryIfTrue = pathFactory,
                PrefabFactoryIfFalse = wallFactory,
            };
            gridFactory.CreatePrefabs(occupancyGrid);
        }

        private System.Random CreateRandom()
        {
            if (randomSeed == 0)
            {
                randomSeed = (int)System.DateTime.Now.Ticks & 0x0000FFFF;
            }
            Debug.Log("Random Seed: " + randomSeed);
            System.Random random = new System.Random(randomSeed);
            return random;
        }

        private IGridGraph<bool> ConvertMazeToOccupancyGraph(Maze maze)
        {
            IGridGraph<bool> occupancyGrid;
            if (tileStyle == TileStyle.Small2x2)
                occupancyGrid = MazeUtility.Create2x2OccupancyGridFromMaze(maze);
            else
                occupancyGrid = MazeUtility.Create3x3OccupancyGridFromMaze(maze, tileStyle);
            PrintOccupancyGrid(occupancyGrid);
            return occupancyGrid;
        }

        private static void PrintOccupancyGrid(IGridGraph<bool> occupancyGrid)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for (int row = occupancyGrid.NumberOfRows - 1; row >= 0; row--)
            {
                for (int column = 0; column < occupancyGrid.NumberOfColumns; column++)
                {
                    char symbol = (occupancyGrid.GetCellValue(row, column) == true) ? ' ' : '█';
                    stringBuilder.Append(symbol);
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine();
            Debug.Log(stringBuilder.ToString());
        }
    }
}