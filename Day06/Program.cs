using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the input from a file and split it by commas
            string[] fish = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt")).Split(",");
            // Convert the input string array to an integer array
            int[] inputs = (Array.ConvertAll(fish, s => Int32.Parse(s)));

            Console.WriteLine($"Part 1 solution: {Part1(inputs)}");

            // Convert the input string array to an integer array again (reusing the same 'inputs' variable)
            inputs = (Array.ConvertAll(fish, s => Int32.Parse(s)));
            Console.WriteLine($"Part 2 solution: {Part2(inputs)}");
        }

        private static object Part1(int[] fishArray)
        {
            // Perform the process for 80 days
            for (int i = 0; i < 80; i++)
            {
                // Create a new list to store the numbers for the new day
                List<int> newDay = new List<int>();
                // Iterate over each number in the input array
                for (int j = 0; j < fishArray.Count(); j++)
                {
                    // If the number is 0, replace it with 7 and add 8 to the new day list
                    if (fishArray[j] == 0)
                    {
                        fishArray[j] = 7;
                        newDay.Add(8);
                    }
                    // Decrement the number by 1
                    fishArray[j]--;
                }

                // Combine the input array with the new day list
                List<int> newFish = new List<int>();
                newFish.AddRange(fishArray);
                newFish.AddRange(newDay);
                // Convert the combined list to an array and update the input fish array
                fishArray = newFish.ToArray();
            }

            // Return the length of the input array as the solution
            return fishArray.Length;
        }

        private static object Part2(int[] fishArray)
        {
            // Create an array to keep track of the number of occurrences of each number (fish generation)
            long[] fishByAge = new long[9];
            // Iterate over each number in the input array and increment the corresponding count in the fish generation array
            foreach (int i in fishArray)
            {
                fishByAge[i]++;
            }

            // Perform 256 iterations
            for (int i = 0; i < 256; i++)
            {
                // Store the count of the first element (number 0)
                long newOnes = fishByAge[0];
                // Shift the counts in the fish generation array to the left (except for the last element)
                for (int j = 1; j < fishByAge.Length; j++)
                {
                    fishByAge[j - 1] = fishByAge[j];
                }

                // Set the last element (number 8) to the stored count of the first element
                fishByAge[8] = newOnes;
                // Add the stored count to the count of the sixth element (number 6)
                fishByAge[6] += newOnes;
            }

            // Return the sum of all counts in the fish generation array as the solution
            return fishByAge.Sum();
        }
    }
}
