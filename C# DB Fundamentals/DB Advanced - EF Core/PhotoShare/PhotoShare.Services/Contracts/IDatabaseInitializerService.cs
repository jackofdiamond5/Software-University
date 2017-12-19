namespace PhotoShare.Services.Contracts
{
    public interface IDatabaseInitializerService
    {
        void InitializeDatabase();

        void ResetDatabase();
    }
}
