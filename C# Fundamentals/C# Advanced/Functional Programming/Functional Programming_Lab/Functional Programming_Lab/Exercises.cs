using System;
using System.Linq;
using System.Collections.Generic;

namespace Functional_Programming_Lab
{
    public class Exercises
    {
        public static void Main()
        {

        }


        /// <summary>
        /// Exercise 5
        /// </summary>
        private static void FilterByAge()
        {
            var n = int.Parse(Console.ReadLine());

            var pairs = new Dictionary<string, int>();

            for (var i = 0; i < n; i++)
            {
                var data = Console.ReadLine().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var personName = data[0];
                var personAge = int.Parse(data[1]);

                if (!pairs.ContainsKey(personName))
                {
                    pairs.Add(personName, personAge);
                }
            }

            var condition = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());
            var format = Console.ReadLine();

            var filter = GetFilter(condition, age);
            var printer = GetPrinter(format);

            foreach (var kvp in pairs)
            {
                if (filter(kvp.Value))
                {
                    printer(kvp);
                }
            }
        }
        private static Action<KeyValuePair<string, int>> GetPrinter(string format)
        {
            switch (format)
            {
                case "name":
                    return f => Console.WriteLine(f.Key);

                case "age":
                    return f => Console.WriteLine(f.Value);

                case "name age":
                    return f => Console.WriteLine($"{f.Key} - {f.Value}");
            }

            return null;
        }
        private static Func<int, bool> GetFilter(string condition, int age)
        {
            if (condition == "younger")
            {
                return x => x < age;
            }

            return x => x >= age;
        }
        ///

        /// <summary>
        /// Exercise 4
        /// </summary>
        public static void AddVat()
        {
            var line = Console.ReadLine()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToList();

            IEnumerable<double> CalcVat(IEnumerable<double> prices) => prices.Select(p => p + p * 0.2).ToList();

            var result = CalcVat(line);
            foreach (var price in result)
            {
                Console.WriteLine($"{price:F2}");
            }
        }

        /// <summary>
        /// Exercise 3
        /// </summary>
        public static void CountUppercaseWords()
        {
            var text = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            void Uppers(IEnumerable<string> word) => word.Where(w => char.IsUpper(w[0])).ToList()
                .ForEach(Console.WriteLine);

            Uppers(text);
        }

        /// <summary>
        /// Exercise 2
        /// </summary>
        public static void SumNumbers()
        {
            var line = Console.ReadLine().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            int Sum(IEnumerable<string> nums) => nums.Select(int.Parse).ToList().Sum();

            Console.WriteLine(line.Count);
            Console.WriteLine(Sum(line));
        }

        /// <summary>
        /// Exercise 1
        /// </summary>
        public static void SortEvenNumbers()
        {
            var line = Console.ReadLine()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            List<int> EvenNumbers(IEnumerable<int> l) => l
                .Where(m => m % 2 == 0)
                .OrderBy(n => n)
                .ToList();

            Console.WriteLine(string.Join(", ", EvenNumbers(line)));
        }
    }
}
