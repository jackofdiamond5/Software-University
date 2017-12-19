using System;
using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    class StartUp
    {
        static void Main()
        {
            var context = new FootballBettingContext();

            using(context)
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
