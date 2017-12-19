namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IAlbumService
    {
        Album CreateAlbum(string username, string albumTitle, string bgColor, params string[] tags);

        void ShareAlbum(int albumId, string username, string permission);

        bool IsOwner(User user, int albumId);
    }
}
