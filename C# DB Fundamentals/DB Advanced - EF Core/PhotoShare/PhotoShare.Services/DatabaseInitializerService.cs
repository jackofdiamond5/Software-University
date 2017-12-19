using Microsoft.EntityFrameworkCore;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    using PhotoShare.Data;

    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly PhotoShareContext context;

        public DatabaseInitializerService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            context.Database.EnsureCreated();
        }

        public void ResetDatabase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
