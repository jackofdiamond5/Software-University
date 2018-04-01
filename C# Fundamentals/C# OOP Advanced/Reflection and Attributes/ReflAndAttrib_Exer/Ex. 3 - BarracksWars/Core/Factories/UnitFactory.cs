namespace _03BarracksFactory.Core.Factories
{
    using System;
    using Contracts;

    public class UnitFactory : IUnitFactory
    {
        public IUnit CreateUnit(string unitTypeName)
        {
            var unitType = Type.GetType($"_03BarracksFactory.Models.Units.{unitTypeName}");
            var unit = (IUnit)Activator.CreateInstance(unitType, true);
            
            return unit;
        }
    }
}
