using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        private static int BasinSize = 0; // Represents the size of a basin
        private static HashSet<string> visited = new HashSet<string>(); // Stores the visited coordinates
        private static int rowLength = 0; // Represents the number of rows in the map
        private static int columnLength = 0; // Represents the number of columns in the map

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            Console.WriteLine($"Part one solution: {Part1(input)}"); 
            Console.WriteLine($"Part two solution: {Part2(input)}"); 
        }

        private static int Part1(string[] input)
        {
            int result = 0; 
            int[][] map = GetArray(input); // Convert the input to a 2D array
            for (int row = 0; row < map.Length; row++) 
            {
                for (int column = 0; column < map[0].Length; column++) 
                {
                    int current = map[row][column]; 
                    if (IsLowPoint(map, row, column, current)) 
                    {
                        result += (current + 1); 
                    }
                }
            }

            return result; 
        }

        private static bool IsLowPoint(int[][] map, int row, int column, int current)
        {
            // Check if the current point is surrounded by higher or equal  points
            if (row != 0 && current >= map[row - 1][column])
            {
                return false;
            }
            if (row + 1 != map.Length && current >= map[row + 1][column])
            {
                return false;
            }
            if (column != 0 && current >= map[row][column - 1])
            {
                return false;
            }
            if (column + 1 != map[0].Length && current >= map[row][column + 1])
            {
                return false;
            }

            return true; // Return true if it is a low point
        }

        private static int[][] GetArray(string[] input)
        {
            List<int[]> listOfRowTiles = new List<int[]>();
            foreach (string line in input) 
            {
                int[] intInput = (Array.ConvertAll(line.ToCharArray(), s => Int32.Parse(s.ToString()))); 
                listOfRowTiles.Add(intInput); 
            }

            return listOfRowTiles.ToArray(); 
        }

        private static object Part2(string[] input)
        {
            List<int> basin = new List<int>(); 
            int[][] map = GetArray(input); 
            rowLength = map.Length; 
            columnLength = map[0].Length; 
            for (int row = 0; row < rowLength; row++) 
            {
                for (int column = 0; column < map[0].Length; column++) 
                {
                    int current = map[row][column]; 
                    if (IsLowPoint(map, row, column, current)) 
                    {
                        BasinSize = 0; 
                        visited = new HashSet<String>(); 
                        RecursiveFunction(map, row, column); 
                        basin.Add(BasinSize); 
                    }
                }
            }

            return basin.OrderByDescending(x => x).Take(3).Aggregate(1, (x, y) => x * y); // Return the product of the three largest basin sizes
        }

        private static void RecursiveFunction(int[][] map, int row, int column)
        {
            // Check if the current coordinate is within the valid range and hasn't been visited before
            if (!IsVisited(map, row, column) && row != -1 && column != -1 && row < rowLength && column < columnLength)
            {
                visited.Add($"{row}:{column}"); 
                int current = map[row][column]; 
                if (current != 9) 
                {
                    BasinSize++; 
                    RecursiveFunction(map, row - 1, column); 
                    RecursiveFunction(map, row + 1, column);
                    RecursiveFunction(map, row, column - 1); 
                    RecursiveFunction(map, row, column + 1); 
                }
            }
        }

        private static bool IsVisited(int[][] map, int row, int collumn)
        {
            return visited.Contains($"{row}:{collumn}"); // Check if the coordinate has been visited before
        }
    }
}
