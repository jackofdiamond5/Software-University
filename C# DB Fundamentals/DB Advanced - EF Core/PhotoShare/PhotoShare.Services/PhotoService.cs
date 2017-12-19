namespace PhotoShare.Services
{
    using Data;
    using Models;
    using Contracts;


    using System;
    using System.Linq;

    public class PhotoService : IPhotoService
    {
        private readonly PhotoShareContext context;

        public PhotoService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void UploadPicture(string albumName, string pictureTitle, string pictureFilePath)
        {
            var album = context.Albums
                .SingleOrDefault(a => a.Name == albumName);

            album.Pictures.Add(new Picture
            {
                AlbumId = album.Id,
                Title = pictureTitle,
                Path = pictureFilePath
            });

            context.SaveChanges();
        }

        public bool IsOwner(string albumName, User user)
        {
            var curUser = context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));

            var curAlbum = context.Albums.SingleOrDefault(a => a.Name.Equals(albumName));

            if (curAlbum is null)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            var userRole = context.AlbumRoles
                .SingleOrDefault(ar => ar.UserId.Equals(curUser.Id) && ar.Album.Id.Equals(curAlbum.Id));

            if (userRole is null)
            {
                throw new ArgumentException("Invalid credentials!");
            }

            return userRole.Role.Equals(Role.Owner);
        }
    }
}
