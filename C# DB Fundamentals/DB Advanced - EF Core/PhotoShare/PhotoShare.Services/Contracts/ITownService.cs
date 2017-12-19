namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface ITownService
    {
        Town AddTown(string name, string countryName);
    }
}
