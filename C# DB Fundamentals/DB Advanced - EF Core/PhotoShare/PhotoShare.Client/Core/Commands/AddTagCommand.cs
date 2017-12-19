namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Utilities;
    using Contracts;
    using Services.Contracts;

    public class AddTagCommand : ICommand
    {
        private readonly ITagService tagService;

        public AddTagCommand(ITagService tagService)
        {
            this.tagService = tagService;
        }

        // AddTag <tag>
        public string Execute(params string[] data)
        {
            if (Session.User is null)
            {
                throw new ArgumentException("Invalid credentials!");
            }

            var tag = data[0].ValidateOrTransform();

            tagService.AddTag(tag);

            return $"{tag} was added successfully to database!";
        }
    }
}
