namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class AddTagToCommand : ICommand
    {
        private readonly ITagService tagService;

        public AddTagToCommand(ITagService tagService)
        {
            this.tagService = tagService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(params string[] data)
        {
            var albumName = data[0];
            var tagName = data[1];

            if (Session.User is null || !tagService.IsOwner(albumName, Session.User))
            {
                throw new ArgumentException("Invalid credentials!");
            }

            tagService.AddTagTo(albumName, tagName);

            return $"Tag {tagName} added to {albumName}";
        }
    }
}
