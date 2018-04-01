namespace P01_HarvestingFields
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            string input;
            var harvestFields = typeof(HarvestingFields);
            while ((input = Console.ReadLine()) != "HARVEST")
            {
                switch (input)
                {
                    case "private":
                        PrintFieldInfo("private", harvestFields, f => f.IsPrivate);
                        break;
                    case "protected":
                        PrintFieldInfo("protected", harvestFields, f => f.IsFamily);
                        break;
                    case "public":
                        PrintFieldInfo("public", harvestFields, f => f.IsPublic);
                        break;
                    case "all":
                        PrintFieldInfo("all", harvestFields, f => f.IsPublic
                        || f.IsFamily || f.IsPrivate || f.IsAssembly ||
                        f.IsFamilyAndAssembly || f.IsFamilyOrAssembly || f.IsStatic);
                        break;
                }
            }
        }

        private static void PrintFieldInfo(string modifier, Type type, Func<FieldInfo, bool> condition)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (modifier == "all")
            {
                foreach (var field in fields)
                {
                    string accessModifier = "";
                    if (field.IsPrivate) accessModifier = "private";
                    else if (field.IsFamily) accessModifier = "protected";
                    else if (field.IsPublic) accessModifier = "public";

                    Console.WriteLine($"{accessModifier} {field.FieldType.Name} {field.Name}");
                }

                return;
            }

            fields
                .Where(condition)
                .ToList()
                .ForEach(f => Console.WriteLine($"{modifier} {f.FieldType.Name} {f.Name}"));
        }
    }
}
