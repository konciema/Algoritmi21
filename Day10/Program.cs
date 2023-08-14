using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10

{
    class Program
    {
        private static List<string> wrongLines;
        private static List<string> incompleteLines;
        private static Dictionary<char, int> partOne;
        private static Dictionary<char, int> partTwoValues;
        private static Dictionary<char, char> pairs;
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "input.txt"));
            PopulateDictionaries();
            SortLines(input);

            Console.WriteLine($"Part one solution: {Part1(input)}");
            Console.WriteLine($"Part two solution: {Part2(input)}");
        }

        private static long Part1(string[] input)
        {
            long result = 0;
            foreach (string line in wrongLines)
            {
                Stack<char> stack = new Stack<char>();
                foreach (char ch1 in line.ToCharArray())
                {
                    if (pairs.Values.Contains(ch1))
                    {
                        char ch2 = stack.Pop();
                        if (IfClosingIsInvalid(ch1, ch2))
                        {
                            result += partOne[ch1];
                        }
                    }
                    else
                    {
                        stack.Push(ch1);
                    }
                }
            }

            return result;
        }

        private static object Part2(string[] input)
        {
            long[] results = new long[incompleteLines.Count];
            for (int i = 0; i < incompleteLines.Count; i++)
            {
                string line = incompleteLines[i];
                Stack<char> stack = new Stack<char>();
                foreach (char t1 in line.ToCharArray())
                {
                    if (pairs.Values.Contains(t1))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(t1);
                    }
                }

                long tempResult = 0;
                foreach (var item in stack)
                {
                    tempResult *= 5;
                    tempResult += partTwoValues[pairs[item]];
                }

                results[i] = tempResult;
            }

            int middleIndex = (int)Math.Ceiling((double)results.Length / (double)2);
            Array.Sort(results);

            return results[middleIndex - 1];
        }

        private static void PopulateDictionaries()
        {
            partOne = new Dictionary<char, int>();
            partOne.Add(')', 3);
            partOne.Add(']', 57);
            partOne.Add('}', 1197);
            partOne.Add('>', 25137);

            partTwoValues = new Dictionary<char, int>();
            partTwoValues.Add(')', 1);
            partTwoValues.Add(']', 2);
            partTwoValues.Add('}', 3);
            partTwoValues.Add('>', 4);

            pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('[', ']');
            pairs.Add('{', '}');
            pairs.Add('<', '>');
        }

        private static void SortLines(string[] input)
        {
            wrongLines = new List<string>();
            incompleteLines = new List<string>();
            foreach (string line in input)
            {
                Stack<char> temp = new Stack<char>();
                bool isInvalidLine = false;
                foreach (char ch1 in line.ToCharArray())
                {
                    if (pairs.Values.Contains(ch1))
                    {
                        char ch2 = temp.Pop();
                        if (IfClosingIsInvalid(ch1, ch2))
                        {
                            wrongLines.Add(line);
                            isInvalidLine = true;
                            break;
                        }
                    }
                    else
                    {
                        temp.Push(ch1);
                    }
                }

                if (temp.Count != 0 && !isInvalidLine)
                {
                    incompleteLines.Add(line);
                }
            }
        }

        private static bool IfClosingIsInvalid(char ch1, char ch2)
        {
            return (ch1 == ')' && ch2 != '(') || ((ch1 == ']' && ch2 != '[')) || ((ch1 == '}' && ch2 != '{')) || ((ch1 == '>' && ch2 != '<'));
        }
    }
}