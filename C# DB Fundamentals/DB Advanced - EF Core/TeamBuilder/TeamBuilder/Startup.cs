using TeamBuilder.Data;

namespace TeamBuilder
{
    public class Startup
    {
        static void Main()
        {
            var context = new TeamBuilderContext();

            using (context)
            {
                //context.Database.EnsureDeleted();

                context.Database.EnsureCreated();
            }
        }
    }
}
