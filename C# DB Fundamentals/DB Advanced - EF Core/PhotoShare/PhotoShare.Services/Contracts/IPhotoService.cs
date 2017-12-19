namespace PhotoShare.Services.Contracts
{

    using Models;

    public interface IPhotoService
    {
        void UploadPicture(string albumName, string pictureTitle, string pictureFilePath);

        bool IsOwner(string albumName, User user);
    }
}
