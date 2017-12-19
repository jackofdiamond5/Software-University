namespace PhotoShare.Services
{
    using System;
    using System.Linq;

    using Data;
    using Models;
    using Contracts;

    public class TagService : ITagService
    {
        private readonly PhotoShareContext context;

        public TagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Tag AddTag(string tag)
        {
            var tagExists = context.Tags
                .ToArray()
                .SingleOrDefault(t => t.Name.Equals(tag));

            if (tagExists != null)
            {
                throw new ArgumentException($"Tag {tag} exists!");
            }

            var newTag = new Tag(tag);

            context.Tags.Add(newTag);

            context.SaveChanges();

            return newTag;
        }

        public void AddTagTo(string album, string tag)
        {
            var dbAlbum = context.Albums
                .SingleOrDefault(a => a.Name.Equals(album));

            var dbTag = context.Tags
                .SingleOrDefault(t => t.Name.Equals(tag));

            if (dbTag is null)
            {
                throw new ArgumentException("Either tag or album does not exist!");
            }

            var albumTag = new AlbumTag
            {
                TagId = dbTag.Id,
                Tag = dbTag
            };

            dbAlbum.AlbumTags.Add(albumTag);

            context.SaveChanges();
        }

        public bool IsOwner(string albumName, User user)
        {
            var curUser = context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));

            var curAlbum = context.Albums.SingleOrDefault(a => a.Name.Equals(albumName));

            if (curAlbum is null)
            {
                throw new ArgumentException("Either tag or album does not exist!");
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
