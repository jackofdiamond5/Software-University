using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Task_3___Crypto_Blockchain
{
    public class CryptoBlockchain
    {
        public static void Main()
        {
            var roomCount = int.Parse(Console.ReadLine());
            var cryptoBlock = new StringBuilder();

            for (var i = 0; i < roomCount; i++)
            {
                var curRow = Console.ReadLine();
                cryptoBlock.Append(curRow);
            }

            var regex = new Regex(@"(\[.*?\d{3,}.*?\])|(\{.*?\d{3,}.*?\})");

            var matchedDigits = new Queue<string>();
            var blockLengths = new Queue<int>();
            foreach (Match match in regex.Matches(cryptoBlock.ToString()))
            {
                var currentDigits = new StringBuilder();
                var allDigits = Regex.Matches(match.Value, @"\d+");
                foreach (Match digitMatch in allDigits)
                {
                    if (digitMatch.Length % 3 != 0) continue;

                    currentDigits.Append(digitMatch.Value);
                    blockLengths.Enqueue(match.Value.Length);
                }

                matchedDigits.Enqueue(currentDigits.ToString());
            }

            var numbers = new Queue<int>();
            foreach (var digits in matchedDigits)
            {
                var tripleDigits = Regex.Matches(digits, @"\d{3}");

                foreach (Match digit in tripleDigits)
                {
                    var number = int.Parse(digit.Value) - blockLengths.Peek();

                    numbers.Enqueue(number);
                }

                blockLengths.Dequeue();
            }

            foreach (var number in numbers)
            {
                Console.Write((char)number);
            }
        }
    }
}
