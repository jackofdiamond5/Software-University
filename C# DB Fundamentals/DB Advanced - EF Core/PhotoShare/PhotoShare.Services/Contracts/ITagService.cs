namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface ITagService
    {
        Tag AddTag(string tag);

        void AddTagTo(string albumName, string tag);

        bool IsOwner(string albumName, User user);
    }
}
