namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Data;
    using Models;
    using Contracts;

    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Album CreateAlbum(string username, string albumTitle, string bgColor, params string[] tags)
        {
            var user = context.Users
                .ToArray()
                .SingleOrDefault(u => u.Username.Equals(username));

            if (user is null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var album = context.Albums
                .ToArray()
                .SingleOrDefault(a => a.Name.Equals(albumTitle));

            if (album != null)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            var albumColor = typeof(Color)
                .GetFields()
                .SingleOrDefault(fi => fi.Name.Equals(bgColor));

            if (albumColor is null)
            {
                throw new ArgumentException($"Color {bgColor} not found!");
            }

            var dbColor = albumColor.GetRawConstantValue();

            var dbTags = context.Tags
                .Select(t => t.Name)
                .ToArray();

            if (tags.Any(ct => !dbTags.Contains(ct)))
            {
                throw new ArgumentException("Invalid tags!");
            }

            // CreateAlbum pesho PeshoAlbum White #CoolPhotos

            var albumTags = GetAlbumTags(dbTags);

            var dbAlbum = new Album(albumTitle, (Color)dbColor, albumTags);

            context.Albums.Add(dbAlbum);

            context.SaveChanges();

            var createdAlbum = context.Albums.SingleOrDefault(a => a.Name.Equals(albumTitle));

            ShareAlbum(createdAlbum.Id, username, "Owner");

            return dbAlbum;
        }

        public void ShareAlbum(int albumId, string username, string permission)
        {
            var user = context.Users
                .SingleOrDefault(u => u.Username.Equals(username));

            if (user is null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var album = context.Albums
                .SingleOrDefault(a => a.Id.Equals(albumId));

            if (album is null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            var roleType = typeof(Role)
                .GetFields()
                .SingleOrDefault(fi => fi.Name.Equals(permission));

            if (roleType is null)
            {
                throw new ArgumentException("Permission must be eithekr \"Owner\" or \"Viewer\"!");
            }

            var roleValue = roleType.GetRawConstantValue();

            var sharedAlbum = context.AlbumRoles
                .SingleOrDefault(ar => ar.UserId == user.Id && ar.AlbumId == albumId);

            if (sharedAlbum != null)
            {
                throw new ArgumentException($"{album.Name} is already shared with {user.Username}!");
            }

            user.AlbumRoles.Add(new AlbumRole
                {
                    User = user,
                    AlbumId = albumId,
                    Role = (Role)roleValue
                });

            context.SaveChanges();
        }

        public bool IsOwner(User user, int albumId)
        {
            if (user is null)
            {
                throw new ArgumentException("Invalid credentials!");
            }
            
            var userRole = context.AlbumRoles
                .SingleOrDefault(ar => ar.UserId.Equals(user.Id) && ar.AlbumId.Equals(albumId));

            if (userRole is null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }

            return userRole.Role.Equals(Role.Owner);
        }

        private ICollection<AlbumTag> GetAlbumTags(IEnumerable<string> tags)
        {
            var curAlbumTags = new List<AlbumTag>();

            var dbTags = context.Tags.Where(t => tags.Contains(t.Name));

            foreach (var tag in dbTags)
            {
                var albumTag = new AlbumTag
                {
                    TagId = tag.Id,
                    Tag = tag
                };

                curAlbumTags.Add(albumTag);
            }

            return curAlbumTags;
        }
    }
}
