using System;

namespace PhotoShare.Services
{
    using Data;
    using Models;
    using Contracts;

    using System.Linq;

    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Town AddTown(string townName, string countryName)
        {
            var town = new Town
            {
                Name = townName,
                Country = countryName
            };

            var townExists = context.Towns
                .ToArray()
                .SingleOrDefault(t => t.Name.Equals(townName));

            if (townExists != null)
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            context.Towns.Add(town);
            context.SaveChanges();

            return town;
        }
    }
}