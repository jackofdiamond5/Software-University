using System.Linq;
using PhotoShare.Client.Core.Commands.Contracts;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;

        public CreateAlbumCommand(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(params string[] data)
        {
            var username = data[0];
            var albumTitle = data[1];
            var bgColor = data[2];
            var tags = data.Skip(3).ToArray();

            if (Session.User is null || !Session.User.Username.Equals(username))
            {
                throw new ArgumentException("Invalid credentials!");
            }

            albumService.CreateAlbum(username, albumTitle, bgColor, tags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
