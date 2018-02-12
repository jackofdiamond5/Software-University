using System;
using System.Linq;
using System.Collections.Generic;

namespace Exam_11_02_2018
{
    public class KeyRevolver
    {
        public static void Main(string[] args)
        {
            var bulletPrice = int.Parse(Console.ReadLine());
            var barrelSize = int.Parse(Console.ReadLine());

            var bullets = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var locks = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var intelligenceValue = int.Parse(Console.ReadLine());

            var bulletsStack = new Stack<int>();
            foreach (var bulletVal in bullets)
            {
                bulletsStack.Push(bulletVal);
            }

            var locksQueue = new Queue<int>();
            foreach (var lockVal in locks)
            {
                locksQueue.Enqueue(lockVal);
            }

            var locksLeft = locksQueue.Count;
            var bulletsLeft = bulletsStack.Count;
            var bulletsUsed = 0;
            var breakTime = false;
            while (bulletsStack.Count > 0 || locksQueue.Count > 0)
            {
                if (breakTime) break;

                for (var magazine = barrelSize; magazine >= 0; magazine--)
                {
                    if (magazine == 0 && bulletsStack.Count > 0)
                    {
                        Console.WriteLine("Reloading!");
                        break;
                    }

                    if (bulletsStack.Count == 0 || locksQueue.Count == 0)
                    {
                        breakTime = true;
                        break;
                    }

                    var currentLock = locksQueue.Peek();
                    var currentBullet = bulletsStack.Pop();

                    if (currentBullet <= currentLock)
                    {
                        Console.WriteLine("Bang!");
                        locksQueue.Dequeue();
                        locksLeft--;
                    }
                    else
                    {
                        Console.WriteLine("Ping!");
                    }

                    bulletsUsed++;
                    bulletsLeft--;
                }
            }

            if (bulletsStack.Count == 0 && locksQueue.Count != 0)
            {
                Console.WriteLine($"Couldn't get through. Locks left: {locksLeft}");
            }
            else
            {
                var earnings = intelligenceValue - bulletsUsed * bulletPrice;

                Console.WriteLine($"{bulletsLeft} bullets left. Earned ${earnings}");
            }
        }
    }
}
