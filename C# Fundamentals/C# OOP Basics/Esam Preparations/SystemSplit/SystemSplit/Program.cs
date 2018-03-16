using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var hardware = new List<Hardware>();
        var software = new List<Software>();
        
        string input;
        while ((input = Console.ReadLine()) != "System Split")
        {
            var inputArgs = input
                .Split(new[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim())
                .ToArray();

            var command = inputArgs[0];
            switch (command)
            {
                case "RegisterPowerHardware":
                    RegisterPowerHardwareCommand(inputArgs, hardware);
                    break;
                case "RegisterHeavyHardware":
                    RegisterHeavyHardwareCommand(inputArgs, hardware);
                    break;
                case "RegisterExpressSoftware":
                    RegisterExpressSoftwareCommand(inputArgs, software, hardware);
                    break;
                case "RegisterLightSoftware":
                    RegisterLightSoftwareCommand(inputArgs, software, hardware);
                    break;
                case "ReleaseSoftwareComponent":
                    ReleaseSoftwareComponentCommand(inputArgs, hardware, software);
                    break;
                case "Analyze":
                    Console.Write(AnalyzeCommand(hardware, software));
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        Console.Write(SplitSystem(hardware, software));
    }

    private static string SplitSystem(List<Hardware> hardware, List<Software> software)
    {
        var builder = new StringBuilder();

        foreach (var component in hardware.OrderByDescending(c => c.GetType().Name))
        {
            builder.AppendLine($"Hardware Component - {component.Name}");

            var expressSoftwareComponents =
                component.RegisteredSoftware.Where(s => s.GetType().Name == "ExpressSoftware").Count();
            var lightSoftwareComponents = 
                component.RegisteredSoftware.Where(s => s.GetType().Name == "LightSoftware").Count();
            builder.AppendLine($"Express Software Components - {expressSoftwareComponents}");
            builder.AppendLine($"Light Software Components - {lightSoftwareComponents}");

            builder.AppendLine(
                $"Memory Usage: {component.RegisteredSoftware.Select(s => s.MemoryConsumption).Sum()} / {component.MaximumMemory}");
            builder.AppendLine(
                $"Capacity Usage: {component.RegisteredSoftware.Select(s => s.CapacityConsumption).Sum()} / {component.MaximumCapacity}");

            var type = string.Join("", component.GetType().Name.Reverse().Skip(8).Reverse());

            builder.AppendLine($"Type: {type}");

            if (component.RegisteredSoftware.Count == 0)
            {
                builder.AppendLine("Software Components: None");
            }
            else
            {
                builder.AppendLine($"Software Components: {string.Join(", ", component.RegisteredSoftware)}");
            }
        }

        return builder.ToString();
    }

    private static string AnalyzeCommand(List<Hardware> hardware, List<Software> software)
    {
        var builder = new StringBuilder();

        builder.AppendLine("System Analysis");
        builder.AppendLine($"Hardware Components: {hardware.Count}");
        builder.AppendLine($"Software Components: {software.Count}");

        var totalMemoryInUse = GetMemoryInUse(hardware);
        var maximumMemory = GetMaximumMemory(hardware);
        builder.AppendLine($"Total Operational Memory: {totalMemoryInUse} / {maximumMemory}");

        var totalCapacityInUse = GetCapacityInUse(hardware);
        var maximumCapacity = GetMaximumCapacity(hardware);
        builder.AppendLine($"Total Capacity Taken: {totalCapacityInUse} / {maximumCapacity}");

        return builder.ToString();
    }

    private static int GetMaximumCapacity(List<Hardware> hardware)
    {
        var maximumCapacity = hardware.Select(h => h.MaximumCapacity).Sum();

        return maximumCapacity;
    }
    private static int GetMaximumMemory(List<Hardware> hardware)
    {
        return hardware.Select(h => h.MaximumMemory).Sum();
    }
    private static int GetCapacityInUse(List<Hardware> hardware)
    {
        var totalCapacityInUse = hardware.Select(h => new
        {
            SoftwareCapacity = h.RegisteredSoftware.Select(s => s.CapacityConsumption).Sum()
        }).Sum(h => h.SoftwareCapacity);

        return totalCapacityInUse;
    }
    private static int GetMemoryInUse(List<Hardware> hardware)
    {
        var totalMemoryInUse = hardware.Select(h => new
        {
            SoftwareMemory = h.RegisteredSoftware.Select(s => s.MemoryConsumption).Sum()
        }).Sum(h => h.SoftwareMemory);

        return totalMemoryInUse;
    }

    private static void ReleaseSoftwareComponentCommand(
        string[] inputArgs, List<Hardware> hardware, List<Software> software)
    {
        var hardwareComponentName = inputArgs[1];
        var softwareComponentName = inputArgs[2];

        var targetHardware = hardware.SingleOrDefault(h => h.Name == hardwareComponentName);
        if (targetHardware != null)
        {
            var targetSoftware = targetHardware.ReleaseSoftware(softwareComponentName);
            software.Remove(targetSoftware);
        }
    }

    private static void RegisterLightSoftwareCommand(
        string[] inputArgs, List<Software> software, List<Hardware> hardware)
    {
        var hardwareComponentName = inputArgs[1];
        var lsName = inputArgs[2];
        var lsCapacityConsumption = int.Parse(inputArgs[3]);
        var lsMemoryConsumption = int.Parse(inputArgs[4]);

        var lightSoftware = new LightSoftware(lsName, lsCapacityConsumption, lsMemoryConsumption);
        var targetHardware = hardware.FirstOrDefault(h => h.Name == hardwareComponentName);

        if (targetHardware != null && targetHardware.CanRunSoftware(lightSoftware))
        {
            targetHardware.RegisterSoftwareComponent(lightSoftware);
            software.Add(lightSoftware);
        }
    }

    private static void RegisterExpressSoftwareCommand(
        string[] inputArgs, List<Software> software, List<Hardware> hardware)
    {
        var hardwareComponentName = inputArgs[1];
        var esName = inputArgs[2];
        var esCapacityConsumption = int.Parse(inputArgs[3]);
        var esMemoryConsumption = int.Parse(inputArgs[4]);

        var expressSoftware = new ExpressSoftware(esName, esCapacityConsumption, esMemoryConsumption);
        var targetHardware = hardware.FirstOrDefault(h => h.Name == hardwareComponentName);

        if (targetHardware != null && targetHardware.CanRunSoftware(expressSoftware))
        {
            targetHardware.RegisterSoftwareComponent(expressSoftware);
            software.Add(expressSoftware);
        }
    }

    private static void RegisterHeavyHardwareCommand(string[] inputArgs, List<Hardware> hardware)
    {
        var hhName = inputArgs[1];
        var hhCapacity = int.Parse(inputArgs[2]);
        var hhMemory = int.Parse(inputArgs[3]);

        var heavyHardware = new HeavyHardware(hhName, hhCapacity, hhMemory);
        hardware.Add(heavyHardware);
    }

    private static void RegisterPowerHardwareCommand(string[] inputArgs, List<Hardware> hardware)
    {
        var phName = inputArgs[1];
        var phCapacity = int.Parse(inputArgs[2]);
        var phMemory = int.Parse(inputArgs[3]);

        var powerHardware = new PowerHardware(phName, phCapacity, phMemory);
        hardware.Add(powerHardware);
    }
}
