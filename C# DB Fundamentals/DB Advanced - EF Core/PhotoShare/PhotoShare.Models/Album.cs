using System.Reflection.Metadata;

namespace PhotoShare.Models
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;


    public class Album
    {
        private ICollection<Picture> pictures;
        private ICollection<AlbumTag> albumTags;
        private ICollection<AlbumRole> albumRoles;

        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.AlbumTags = new HashSet<AlbumTag>();
            this.AlbumRoles = new HashSet<AlbumRole>();
        }

        public Album(string albumTitle, Color? bgColor, ICollection<AlbumTag> tags)
        {
            this.Name = albumTitle;
            this.BackgroundColor = bgColor;
            this.albumTags = tags;
            this.Pictures = new HashSet<Picture>();
            this.AlbumRoles = new HashSet<AlbumRole>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public Color? BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<AlbumRole> AlbumRoles
        {
            get { return this.albumRoles; }
            set { this.albumRoles = value; }
        }

        public ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        public ICollection<AlbumTag> AlbumTags
        {
            get { return this.albumTags; }
            set { this.albumTags = value; }
        }

        public override string ToString()
        {
            return $"{this.Name} has {this.Pictures.Count} pictures";
        }
    }
}
