namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Data;
    using Models;
    using Contracts;
    using Services.Contracts;

    public class ShareAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;

        public ShareAlbumCommand(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(params string[] data)
        {
            var albumId = int.Parse(data[0]);
            var username = data[1];
            var permission = data[2];

            var isOwner = albumService.IsOwner(Session.User, albumId);

            if (!isOwner)
            {
                throw new ArgumentException("Invalid credentials!");
            }
            
            albumService.ShareAlbum(albumId, username, permission);

            var context = new PhotoShareContext();

            Album album;

            using (context)
            {
                album = context.Albums.Find(albumId);
            }

            return $"Username {username} added to album {album.Name} ({permission})";
        }
    }
}
