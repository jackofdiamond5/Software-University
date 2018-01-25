using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Stacks_And_Queues
{
    public class Exercises
    {
        public static void Main()
        {
        }

        /// <summary>
        /// Exercise 6
        /// </summary>
        /// <param name="passedCarsCount"></param>
        /// <param name="command"></param>
        public static void TrafficJam(int passedCarsCount, string command)
        {
            var carQueue = new Queue<string>();
            var totalCarsPassedCount = 0;
            while (command.ToLower() != "end")
            {
                if (!command.ToLower().Equals("green"))
                {
                    carQueue.Enqueue(command);
                }
                else
                {
                    for (var i = 0; i < passedCarsCount; i++)
                    {
                        if (carQueue.Count.Equals(0))
                        {
                            break;
                        }

                        Console.WriteLine($"{carQueue.Dequeue()} passed!");
                        totalCarsPassedCount++;
                    }
                }

                command = Console.ReadLine();
            }

            Console.WriteLine($"{totalCarsPassedCount} cars passed the crossroads.");
        }

        /// <summary>
        /// Exercise 5
        /// </summary>
        /// <param name="kidsString"></param>
        /// <param name="removeKidIndex"></param>
        /// <returns></returns>
        public static string HotPotato(string kidsString, int removeKidIndex)
        {
            var inputData = kidsString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var builder = new StringBuilder();

            var kidsQueue = new Queue<string>();
            foreach (var kid in inputData)
            {
                kidsQueue.Enqueue(kid);
            }

            var stepCounter = 1;
            while (kidsQueue.Count != 1)
            {
                if (stepCounter < removeKidIndex)
                {
                    var currentKid = kidsQueue.Dequeue();
                    kidsQueue.Enqueue(currentKid);
                    stepCounter++;
                }
                else
                {
                    stepCounter = 1;
                    builder.AppendLine($"Removed {kidsQueue.Dequeue()}");
                }
            }

            builder.Append($"Last is {kidsQueue.Peek()}");

            return builder.ToString();
        }

        /// <summary>
        /// Exercise 4
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MatchingBrackers(string input)
        {
            var stackIndexes = new Stack<int>();
            var builder = new StringBuilder();
            for (var charIndex = 0; charIndex < input.Length; charIndex++)
            {
                var symbol = input[charIndex];

                if (symbol.Equals('('))
                {
                    stackIndexes.Push(charIndex);
                }
                else if (symbol.Equals(')'))
                {
                    var openingBracketIndex = stackIndexes.Pop();
                    var closingBracketIndex = charIndex;

                    for (var i = openingBracketIndex; i <= closingBracketIndex; i++)
                    {
                        builder.Append(input[i]);
                    }

                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Exercise 3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecimalToBinaryConverter(int input)
        {
            if (input.Equals(0))
            {
                return "0";
            }

            var stack = new Stack<int>();
            while (input > 0)
            {
                var remainder = input % 2;
                input /= 2;

                stack.Push(remainder);
            }

            return string.Join("", stack);
        }

        /// <summary>
        /// Exercise 2
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>

        public static decimal SimpleCalculator(string inputString)
        {
            var elements = inputString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var stackOfSymbols = new Stack<string>();
            foreach (var symbol in elements)
            {
                stackOfSymbols.Push(symbol);
            }

            var reversedStack = new Stack<string>();
            while (stackOfSymbols.Count() != 0)
            {
                reversedStack.Push(stackOfSymbols.Pop());
            }

            var result = 0m;
            var lastDigit = 0m;
            var lastSymbol = "";
            while (reversedStack.Count() != 0)
            {
                var currentSymbol = reversedStack.Pop();
                var isNum = decimal.TryParse(currentSymbol, out decimal digit);
                var currentDigit = digit;
                decimal currentSum;

                if (lastSymbol.Equals("+"))
                {
                    currentSum = lastDigit + currentDigit;

                    if (currentSum.Equals(lastDigit))
                    {
                        lastDigit = currentDigit;
                        continue;
                    }

                    result = currentSum;
                    lastDigit = result;
                    lastSymbol = currentSymbol;
                    continue;
                }

                if (lastSymbol.Equals("-"))
                {
                    currentSum = lastDigit - currentDigit;

                    if (currentSum.Equals(lastDigit))
                    {
                        lastDigit = currentDigit;
                        continue;
                    }

                    result = currentSum;
                    lastDigit = result;
                    lastSymbol = currentSymbol;
                    continue;
                }

                if (isNum)
                {
                    lastDigit = currentDigit;
                }
                else
                {
                    lastSymbol = currentSymbol;
                }
            }

            return result;
        }

        /// <summary>
        /// Exercise 1
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string ReverseStrings(string inputString)
        {
            var lettersArray = inputString.ToCharArray();
            var stack = new Stack<char>();

            foreach (var letter in lettersArray)
            {
                stack.Push(letter);
            }

            return string.Join("", stack);
        }
    }
}