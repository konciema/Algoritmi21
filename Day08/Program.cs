using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day08
{
    class Program
    {
        // Reges.Matches method to find all matches
        private static string wholeWord = @"(\w+)";

        static void Main(string[] args)
        {
            // Read the input from a file
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));

            // Print the solution for Part 1
            Console.WriteLine($"Part one solution: {Part1(input)}");
            // Print the solution for Part 2
            Console.WriteLine($"Part two solution: {Part2(input)}");
        }

        private static object Part1(string[] input)
        {
            // Initialize the result variable
            int result = 0;

            // Iterate over each line in the input
            foreach (string line in input)
            {
                // Extract the numbers portion from the line
                string numbers = line.Split("|")[1];
                // Match each number
                MatchCollection matches = Regex.Matches(numbers, wholeWord);

                // Check the length of each matched number
                for (int i = 0; i < matches.Count; i++)
                {
                    var match = matches[i].Value;
                    if (match.Length == 2 || match.Length == 4 || match.Length == 3 || match.Length == 7)
                    {
                        result++;
                    }
                }
            }

            // Return the final result
            return result;
        }

        /// <summary>
        /// length == 6: 0, 6, 9
        /// length == 5: 2, 3, 5
        /// length == 2: 1
        /// length == 4: 4
        /// length == 7: 8
        /// length == 3: 7
        /// 4 is a part of 9
        /// 1 is a part of 3,
        /// 5 is a part of 6 etc
        /// </summary>
        
        private static object Part2(string[] input)
        {
            // Initialize the result variable
            int result = 0;

            // Iterate over each line in the input
            foreach (string line in input)
            {
                // Extract the decode portion from the line
                string firstLine = line.Split("|")[0];
                // Finds words that are separated
                MatchCollection matches = Regex.Matches(firstLine, wholeWord);

                // Initialization of variables
                string one = "";
                string four = "";
                string seven = "";
                string eight = "";

                // Determine the patterns based on the lengths of the matched characters in the decode portion
                for (int i = 0; i < matches.Count; i++)
                {
                    var match = matches[i].Value;
                    if (match.Length == 2)
                    {
                        one = match;
                    }
                    else if (match.Length == 3)
                    {
                        seven = match;
                    }
                    else if (match.Length == 4)
                    {
                        four = match;
                    }
                    else if (match.Length == 7)
                    {
                        eight = match;
                    }
                }

                // Find the differences between the characters in four and eight
                char[] fourEightDifference = eight.ToCharArray().Where(x => !four.Contains(x)).ToArray();

                // Extract the numbers portion from the line
                string numbers = line.Split("|")[1];
                StringBuilder sb = new StringBuilder();
                // Use regular expression to match each number in the numbers portion
                MatchCollection matchesNumbers = Regex.Matches(numbers, wholeWord);

                // Process each matched number and append the corresponding digit to the StringBuilder
                for (int i = 0; i < matchesNumbers.Count; i++)
                {
                    var match = matchesNumbers[i].Value;
                    switch (match.Length)
                    {
                        case 2:
                            sb.Append("1");
                            break;
                        case 3:
                            sb.Append("7");
                            break;
                        case 4:
                            sb.Append("4");
                            break;
                        case 5:
                            sb.Append(lengthFive(one, fourEightDifference, match));
                            break;
                        case 6:
                            sb.Append(lengthSix(four, seven, match));
                            break;
                        case 7:
                            sb.Append("8");
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }

                // Parse the resulting string and add it to the overall result
                result += Int32.Parse(sb.ToString());
            }

            // Return the final result
            return result;
        }
        
        private static string lengthFive(string containsOne, char[] fourAndEightDiff, string number)
        {
            // If the numberSequence contains all items from the one, the numberSequence must be 3
            if (containsOne.ToCharArray().All(x => number.Contains(x)))
            {
                return "3";
            }
            // If the number is not 3 and contains the difference of eight and four, the number must be 2
            else if (fourAndEightDiff.All(x => number.ToCharArray().Contains(x)))
            {
                return "2";
            }
            // Otherwise, the number must be 5
            else
            {
                return "5";
            }
        }

        private static string lengthSix(string containsFour, string containsSeven, string number)
        {
            // If the number contains all items from the fourPattern, the number must be 9
            if (containsFour.ToCharArray().All(x => number.Contains(x)))
            {
                return "9";
            }
            // If the number is not 9 and contains all items from the sevenPattern, the number must be 0
            else if (containsSeven.ToCharArray().All(x => number.Contains(x)))
            {
                return "0";
            }
            // Otherwise, the number must be 6
            else
            {
                return "6";
            }
        }
    }
}
