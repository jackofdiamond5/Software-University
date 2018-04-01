namespace P02_BlackBoxInteger
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            //var bbType = typeof(BlackBoxInteger);
            //var blackBoxCtors = bbType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            //var blackBoxInt = (BlackBoxInteger)blackBoxCtors
            //    .FirstOrDefault(ci => ci.GetParameters().Count() == 0)
            //    .Invoke(new object[] { });

            //var bbType = Type.GetType("P02_BlackBoxInteger.BlackBoxInteger");
            var blackBoxInt = (BlackBoxInteger)Activator.CreateInstance(typeof(BlackBoxInteger), true);

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Split('_');
                var command = args[0];
                var value = int.Parse(args[1]);

                Caller(command, value, blackBoxInt);
            }
        }

        private static void Caller(string command, int value, BlackBoxInteger blackBoxInt)
        {
            var method = blackBoxInt
                .GetType()
                .GetMethod(command, BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(blackBoxInt, new object[] { value });

            var innerValue = blackBoxInt
                .GetType()
                .GetField("innerValue", BindingFlags.Instance | BindingFlags.NonPublic);
            Console.WriteLine(innerValue.GetValue(blackBoxInt));
        }
    }
}
