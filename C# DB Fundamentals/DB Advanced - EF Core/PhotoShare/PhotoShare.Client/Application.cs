namespace PhotoShare.Client
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Core;
    using Data;
    using Services;
    using Services.Contracts;

    public class Application
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();

            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<PhotoShareContext>(options =>
                options.UseSqlServer(ServerConfig.ConnectionString));

            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<ITownService, TownService>();
            serviceCollection.AddTransient<ITagService, TagService>();
            serviceCollection.AddTransient<IAlbumService, AlbumService>();
            serviceCollection.AddTransient<IPhotoService, PhotoService>();
            serviceCollection.AddTransient<IApplicationInterfaceService, ApplicationInterfaceService>();


            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
