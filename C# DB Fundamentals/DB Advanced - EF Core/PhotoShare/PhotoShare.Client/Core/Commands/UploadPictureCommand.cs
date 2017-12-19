namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;
    using Models;
    using Contracts;
    using Services.Contracts;

    public class UploadPictureCommand : ICommand
    {
        private readonly IPhotoService photoService;

        public UploadPictureCommand(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(params string[] data)
        {
            var albumName = data[0];
            var pictureTitle = data[1];
            var pictureFilePath = data[2];

            if (Session.User is null || !photoService.IsOwner(albumName, Session.User))
            {
                throw new ArgumentException("Invalid credentials!");
            }

            photoService.UploadPicture(albumName, pictureTitle, pictureFilePath);

            return $"Picture {pictureTitle} added to {albumName}!";
        }

       
    }
}
