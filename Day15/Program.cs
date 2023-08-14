using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");

            var list = input
                .Split('\n')
                .Select(x => x.Replace("\n", ""))
                .Select(x => x.Replace("\r", ""))
                .Select(row => row.ToCharArray().Select(ch => ch - '0').ToArray())
                .ToArray();

            var width = list[0].Length;
            var height = list.Length;

            Console.WriteLine($"Part one solution: {Part1(list, width, height)}");
            Console.WriteLine($"Part two solution: {Part2(list, width, height)}");
        }

        private static int Part1(int[][] list, int width, int height)
        {
            var minimum = new int[width * height];
            Array.Fill(minimum, int.MaxValue);

            minimum[0] = 0;

            VisitCells(list, width, height, minimum, (0, 0), out var toVisit, out var movements);

            while (toVisit.Any())
            {
                var (x, y) = toVisit.Dequeue();
                var current = x + y * width;

                foreach (var (dx, dy) in movements)
                {
                    var x2 = x + dx;
                    var y2 = y + dy;

                    if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height)
                    {
                        // in bounds
                        var current2 = x2 + y2 * width;
                        if (list[y2][x2] + minimum[current] < minimum[current2])
                        {
                            minimum[current2] = list[y2][x2] + minimum[current];
                            toVisit.Enqueue((x: x2, y: y2));
                        }
                    }
                }
            }

            return minimum[width - 1 + (height - 1) * width];
        }

        private static int Part2(int[][] list, int width, int height)
        {
            int[][] map = new int[height * 5][];
            for (var i = 0; i < height * 5; i++) map[i] = new int[width * 5];

            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    for (var x = 0; x < width; ++x)
                    {
                        for (var y = 0; y < height; ++y)
                        {
                            var n = list[y][x] + i + j;
                            if (n > 9) n -= 9;
                            map[y + j * height][x + i * width] = n;
                        }
                    }
                }
            }
            list = map;
            width = width * 5;
            height = height * 5;

            var minimums2 = new int[width * height];
            Array.Fill(minimums2, int.MaxValue);

            minimums2[0] = 0;

            VisitCells(list, width, height, minimums2, (0, 0), out var toVisit2, out var movements2);

            while (toVisit2.Any())
            {
                var (x, y) = toVisit2.Dequeue();
                var current = x + y * width;

                foreach (var (dx, dy) in movements2)
                {
                    var x2 = x + dx;
                    var y2 = y + dy;

                    if (x2 >= 0 && x2 < width && y2 >= 0 && y2 < height)
                    {
                        // in bounds
                        var current2 = x2 + y2 * width;
                        if (list[y2][x2] + minimums2[current] < minimums2[current2])
                        {
                            minimums2[current2] = list[y2][x2] + minimums2[current];
                            toVisit2.Enqueue((x: x2, y: y2));
                        }
                    }
                }
            }

            return minimums2[width - 1 + (height - 1) * width];
        }

        private static void VisitCells(int[][] list, int width, int height, int[] minimum, (int x, int y) start, out Queue<(int x, int y)> toVisit, out (int dx, int dy)[] movements)
        {
            toVisit = new Queue<(int x, int y)>();
            toVisit.Enqueue(start);

            movements = new[]
            {
                (dx: -1, dy: 0),
                (dx: 1, dy: 0),
                (dx: 0, dy: -1),
                (dx: 0, dy: 1),
            };
        }
    }
}
