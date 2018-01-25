using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Stacks_And_Queues_Exercises
{
    public class Exercises
    {
        public static void Main()
        {
        }

        /// <summary>
        /// Exercise 10
        /// </summary>
        public static void SimpleTextEditor()
        {
            var n = int.Parse(Console.ReadLine());

            var text = new StringBuilder();
            var ctrlZ = new Stack<string>();

            for (var i = 0; i < n; i++)
            {
                var data = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var command = int.Parse(data[0]);

                switch (command)
                {
                    case 1:
                        ctrlZ.Push(text.ToString());
                        text.Append(data[1]);
                        break;
                    case 2:
                        ctrlZ.Push(text.ToString());
                        var length = int.Parse(data[1]);
                        text.Remove(text.Length - length, length);
                        break;
                    case 3:
                        var index = int.Parse(data[1]);
                        Console.WriteLine(text[index - 1]);
                        break;
                    case 4:
                        text.Clear();
                        text.Append(ctrlZ.Pop());
                        break;
                }
            }
        }

        /// <summary>
        /// Exercise 9
        /// </summary>
        public static void StackFibonacci()
        {
            var n = int.Parse(Console.ReadLine());

            var fibStack = new Stack<long>();
            fibStack.Push(0);
            fibStack.Push(1);

            if (n == 1 || n == 2)
            {
                Console.WriteLine(1);
                return;
            }

            for (var i = 0; i < n - 1; i++)
            {
                var topNum = fibStack.Pop();
                var bottomNum = fibStack.Pop();

                var sum = topNum + bottomNum;

                fibStack.Push(topNum);
                fibStack.Push(sum);
            }

            Console.WriteLine(fibStack.Pop());
        }

        /// <summary>
        /// Exercise 8
        /// </summary>
        public static void FibonacciRecursion()
        {
            var n = int.Parse(Console.ReadLine());
            var lookUp = new long[n + 1];

            for (var i = 2; i <= n; i++)
            {
                lookUp[i] = -1;
            }

            Console.WriteLine(GetFibonacci(n, lookUp));
        }
        private static long GetFibonacci(int n, IList<long> lookUp)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }

            if (lookUp[n] == -1)
            {
                lookUp[n] = GetFibonacci(n - 1, lookUp) + GetFibonacci(n - 2, lookUp);
            }

            return lookUp[n];
        }

        /// <summary>
        /// Exercise 7
        /// </summary>
        public static void BalancedParentheses()
        {
            var parentheses = Console.ReadLine().ToCharArray();

            if (parentheses.Length % 2 != 0)
            {
                Console.WriteLine("NO");
                return;
            }

            var opening = new[] { '(', '{', '[' };
            var closing = new[] { ')', '}', ']' };

            var extra = new Stack<char>();

            foreach (var character in parentheses)
            {
                if (opening.Contains(character))
                {
                    extra.Push(character);
                }
                else
                {
                    var lastCharacter = extra.Pop();
                    var openingElement = Array.IndexOf(opening, lastCharacter);
                    var closingElement = Array.IndexOf(closing, character);

                    if (openingElement.Equals(closingElement))
                        continue;

                    Console.WriteLine("NO");
                    return;
                }
            }

            Console.WriteLine(extra.Any() ? "NO" : "YES");
        }

        /// <summary>
        /// Exercise 6
        /// </summary>
        public static void TruckTour()
        {
            var n = int.Parse(Console.ReadLine());

            var pumps = new Queue<int[]>();

            for (var i = 0; i < n; i++)
            {
                var pump = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                pumps.Enqueue(pump);
            }

            for (var currentStart = 0; currentStart < n - 1; currentStart++)
            {
                var fuelAmount = 0;
                var finishesTrip = true;

                for (var pumpsPassed = 0; pumpsPassed < n; pumpsPassed++)
                {
                    var currentPump = pumps.Dequeue();
                    var currentPumpFuel = currentPump[0];
                    var distanceToNextPump = currentPump[1];

                    pumps.Enqueue(currentPump);

                    fuelAmount += currentPumpFuel - distanceToNextPump;

                    if (fuelAmount >= 0)
                        continue;

                    currentStart += pumpsPassed;
                    finishesTrip = false;
                    break;
                }

                if (!finishesTrip)
                    continue;

                Console.WriteLine(currentStart);
                break;
            }
        }

        /// <summary>
        /// Exercise 5
        /// </summary>
        public static void CalculateSequenceWithQueue()
        {

            var s1 = long.Parse(Console.ReadLine());
            var sequence = new Queue<long>();
            sequence.Enqueue(s1);

            var sElements = new Queue<long>();
            sElements.Enqueue(s1);

            var sCounter = 1;
            while (sequence.Count < 50)
            {
                var currentS = sElements.Peek();

                switch (sCounter)
                {
                    case 1:
                        currentS += 1;
                        sequence.Enqueue(currentS);
                        sElements.Enqueue(currentS);
                        break;
                    case 2:
                        currentS = 2 * currentS + 1;
                        sequence.Enqueue(currentS);
                        sElements.Enqueue(currentS);
                        break;
                    case 3:
                        currentS += 2;
                        sequence.Enqueue(currentS);
                        sElements.Enqueue(currentS);
                        break;
                }

                sCounter++;
                if (sCounter < 4)
                    continue;

                sElements.Dequeue();
                sCounter = 1;
            }

            Console.WriteLine(string.Join(" ", sequence));
        }

        /// <summary>
        /// Exercise 4
        /// </summary>
        public static void BasicQueueOperations()
        {
            var commandLine = Console.ReadLine().Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var elementsToEnqueueCount = int.Parse(commandLine[0]);
            var elementsToDequeueCount = int.Parse(commandLine[1]);
            var elementToCheck = commandLine[2];

            var dataLine = Console.ReadLine().Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var queue = new Queue<string>();
            for (var i = 0; i < elementsToEnqueueCount; i++)
            {
                if (i >= dataLine.Length) break;

                queue.Enqueue(dataLine[i]);
            }

            for (var i = 0; i < elementsToDequeueCount; i++)
            {
                if (queue.Count.Equals(0)) break;

                queue.Dequeue();
            }

            if (queue.Count.Equals(0))
            {
                Console.WriteLine(0);
            }
            else if (queue.Contains(elementToCheck))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine(queue.Min());
            }
        }

        /// <summary>
        /// Exercise 3
        /// </summary>
        public static void MaximumElement()
        {
            var n = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();
            var maxElements = new Stack<int>();

            for (var i = 1; i <= n; i++)
            {
                var dataParts = Console.ReadLine().Split();
                var command = int.Parse(dataParts[0]);

                switch (command)
                {
                    case 1:
                        var currentElement = int.Parse(dataParts[1]);
                        stack.Push(currentElement);

                        if (maxElements.Count.Equals(0) || currentElement > maxElements.Peek())
                        {
                            maxElements.Push(currentElement);
                        }
                        break;
                    case 2:
                        var removedElement = stack.Pop();

                        if (removedElement.Equals(maxElements.Peek()))
                        {
                            maxElements.Pop();
                        }
                        break;
                    case 3:
                        Console.WriteLine(maxElements.Peek());
                        break;
                }
            }
        }

        /// <summary>
        /// Exercise 2
        /// </summary>
        public static void BasicStackOperations()
        {
            var commandData = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var elementsToPushCount = int.Parse(commandData[0]);
            var elementsToPopCount = int.Parse(commandData[1]);
            var elementToCheck = commandData[2];

            var numbersData = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var stack = new Stack<string>();

            for (var i = 0; i < elementsToPushCount; i++)
            {
                if (i >= numbersData.Length)
                    break;

                stack.Push(numbersData[i]);
            }

            for (var i = 0; i < elementsToPopCount; i++)
            {
                if (i >= numbersData.Length || stack.Count.Equals(0))
                    break;

                stack.Pop();
            }

            if (stack.Contains(elementToCheck))
            {
                Console.WriteLine("true");
            }
            else
            {
                var min = stack.Min() is null ? "0" : stack.Min();
                Console.WriteLine(min);
            }
        }

        /// <summary>
        /// Exercise 1
        /// </summary>
        /// <param name="inputNumbers"></param>
        /// <returns></returns>
        public static string ReverseStack(string inputNumbers)
        {
            var inputNumbersArr = inputNumbers.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var stack = new Stack<string>();

            foreach (var number in inputNumbersArr)
            {
                stack.Push(number);
            }

            return string.Join(" ", stack);
        }
    }
}




