using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional_Programming_Exer
{
    public class Exercises
    {
        public static void Main()
        {
            var number = int.Parse(Console.ReadLine());

            var dividers = Console.ReadLine().Split().Select(int.Parse).OrderByDescending(x => x % 2 != 0).ToArray();

            Predicate<int> checkDivider = n => dividers.All(d => n % d == 0);

            var resultList = new HashSet<int>();
            for (var i = 1; i <= number; i++)
            {
                if (checkDivider(i))
                {
                    resultList.Add(i);
                }
            }

            Console.WriteLine(string.Join(" ", resultList));
        }


        /// <summary>
        /// Exercise 8
        /// </summary>
        public static void CustomCompare()
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Array.Sort(numbers, new Comparer());

            Console.WriteLine(string.Join(" ", numbers));
        }
        /// <summary>
        /// Another possible solution for Exercise 8
        /// </summary>
        public static void CustomComparerTwo()
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();

            Func<List<int>, int[]> evensFirst = nums =>
            {
                var odds = new Queue<int>();
                var evens = new Queue<int>();

                foreach (var num in nums)
                {
                    if (num % 2 == 0)
                    {
                        evens.Enqueue(num);
                    }
                    else
                    {
                        odds.Enqueue(num);
                    }
                }

                var result = new int[odds.Count + evens.Count];

                var i = 0;
                while (odds.Count != 0 || evens.Count != 0)
                {
                    if (evens.Count != 0)
                    {
                        result[i] = evens.Dequeue();
                    }
                    else
                    {
                        result[i] = odds.Dequeue();
                    }

                    i++;
                }

                return result;
            };

            Console.WriteLine(string.Join(" ", evensFirst(numbers)));
        }

        /// <summary>
        /// Exercise 7
        /// </summary>
        public static void PredicateForNames()
        {
            var nameLength = int.Parse(Console.ReadLine());
            var names = Console.ReadLine().Split();

            Func<string[], List<string>> checkLength = f => f.Where(s => s.Length <= nameLength).ToList();

            checkLength(names).ForEach(Console.WriteLine);
        }

        /// <summary>
        /// Exercise 6
        /// </summary>
        public static void ReverseAndExclude()
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
            numbers.Reverse();
            var n = int.Parse(Console.ReadLine());

            Predicate<int> isDivisible = s => s % n != 0;
            Func<List<int>, List<int>> checkCol = col => col.Where(num => isDivisible(num)).ToList();

            Console.WriteLine(string.Join(" ", checkCol(numbers)));
        }

        /// <summary>
        /// Exercise 5
        /// </summary>
        public static void AppliedArithmetics()
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();

            Func<List<int>, List<int>> add = col =>
            {
                for (var i = 0; i < col.Count; i++)
                {
                    col[i] += 1;
                }

                return col;
            };

            Func<List<int>, List<int>> substract = col =>
            {

                for (var i = 0; i < col.Count; i++)
                {
                    col[i] -= 1;
                }

                return col;
            };

            Func<List<int>, List<int>> multiply = col =>
            {

                for (var i = 0; i < col.Count; i++)
                {
                    col[i] *= 2;
                }

                return col;
            };

            Func<List<int>, string> getCol = col => string.Join(" ", col);

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                switch (command)
                {
                    case "add":
                        numbers = add(numbers);
                        break;
                    case "subtract":
                        numbers = substract(numbers);
                        break;
                    case "multiply":
                        numbers = multiply(numbers);
                        break;
                    case "print":
                        Console.WriteLine(getCol(numbers));
                        break;
                }
            }
        }

        /// <summary>
        /// Exercise 4
        /// </summary>
        public static void FindOddsOrEvens()
        {
            var bound = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var lower = bound[0];
            var upper = bound[1];

            var command = Console.ReadLine();

            Predicate<int> isEven = x => x % 2 == 0;

            var resultList = new List<int>();

            for (var i = lower; i <= upper; i++)
            {
                switch (command)
                {
                    case "odd":
                        if (!isEven(i))
                        {
                            resultList.Add(i);
                        }
                        break;
                    case "even":
                        if (isEven(i))
                        {
                            resultList.Add(i);
                        }
                        break;
                }
            }

            Console.WriteLine(string.Join(" ", resultList));
        }

        /// <summary>
        /// Exercise 3
        /// </summary>
        public static void CustomMinFunction()
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToList();

            Func<List<int>, int> getSmallest = x =>
            {
                var smallest = int.MaxValue;

                foreach (var number in x)
                {
                    if (number < smallest)
                    {
                        smallest = number;
                    }
                }

                return smallest;
            };

            Console.WriteLine(getSmallest(numbers));
        }

        /// <summary>
        /// Exercise 2
        /// </summary>
        public static void KnightsOfHonor()
        {
            var names = Console.ReadLine().Split().ToList();

            Action<List<string>> print = x => x.ForEach(n => Console.WriteLine($"Sir {n}"));

            print(names);
        }

        /// <summary>
        /// Exercise 1
        /// </summary>
        public static void ActionPrint()
        {
            var names = Console.ReadLine().Split().ToList();

            Action<List<string>> print = x => x.ForEach(Console.WriteLine);

            print(names);
        }


        /// <summary>
        /// Class for Exercise 8
        /// </summary>
        internal class Comparer : IComparer<int>
        {
            public int Compare(int a, int b)
            {
                if (a % 2 == 0 && b % 2 != 0)
                {
                    return -1;
                }
                else if (a % 2 != 0 && b % 2 == 0)
                {
                    return 1;
                }
                else
                {
                    if (a < b)
                    {
                        return -1;
                    }
                    else if (a > b)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}