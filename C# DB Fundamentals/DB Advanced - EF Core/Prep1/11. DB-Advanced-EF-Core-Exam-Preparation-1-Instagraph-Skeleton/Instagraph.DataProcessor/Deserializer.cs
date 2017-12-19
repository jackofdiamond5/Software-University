using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Newtonsoft.Json.Linq;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var jPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var pictures = new List<Picture>();

            var builder = new StringBuilder();

            foreach (var picture in jPictures)
            {
                if (picture.Path is null || picture.Path.Equals(string.Empty) || pictures.Any(p => p.Path.Equals(picture.Path)))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (picture.Size <= 0)
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                pictures.Add(new Picture
                {
                    Path = picture.Path,
                    Size = picture.Size
                });

                builder.AppendLine($"Successfully imported Picture {picture.Path}.");
            }

            context.Pictures.AddRange(pictures);

            context.SaveChanges();

            return builder.ToString();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var dbPictures = context.Pictures
                .Select(p => p)
                .ToArray();

            dynamic jUsers = JsonConvert.DeserializeObject(jsonString);

            var users = new List<User>();

            var builder = new StringBuilder();

            foreach (var user in jUsers)
            {
                if (user.Username is null || user.Password is null || user.ProfilePicture is null)
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var userPicture = dbPictures.SingleOrDefault(p => p.Path.Equals(user.ProfilePicture.ToString()));

                if (userPicture is null)
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                users.Add(new User
                {
                    Username = user.Username,
                    Password = user.Password,
                    ProfilePicture = userPicture
                });

                builder.AppendLine($"Successfully imported User {user.Username}.");
            }

            context.AddRange(users);

            context.SaveChanges();

            return builder.ToString();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            // SeedDatabase(context);

            dynamic usersFollowersJson = JsonConvert.DeserializeObject(jsonString);

            var dbUsers = context.Users
                .Select(u => u)
                .ToList();

            var dbUsersFollowers = context.UsersFollowers
                .Select(uf => new
                {
                    uf.UserId,
                    uf.FollowerId,
                    uf.User.Username,
                    FollowerUsername = uf.Follower.Username
                })
                .ToArray();

            var userFollowers = new List<UserFollower>();

            var builder = new StringBuilder();

            foreach (var ufp in usersFollowersJson)
            {
                string curUserName = ufp.User.ToString();
                string curFollowerName = ufp.Follower.ToString();

                if (!dbUsers.Any(u => u.Username.Equals(curUserName)) ||
                    !dbUsers.Any(u => u.Username.Equals(curFollowerName)))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (dbUsersFollowers.Any(uf => uf.Username.Equals(curUserName)) &&
                    dbUsersFollowers.Any(uf => uf.FollowerUsername.Equals(curFollowerName)))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                var curUser = dbUsers.SingleOrDefault(u => u.Username.Equals(curUserName));
                var curFollower = dbUsers.SingleOrDefault(f => f.Username.Equals(curFollowerName));

                if (curUser is null || curFollower is null)
                {
                    builder.AppendLine("Error: Invaldi data.");
                    continue;
                }

                if (userFollowers.Count > 0 && userFollowers.Any(
                    u => u.UserId.Equals(curUser.Id) && u.FollowerId.Equals(curFollower.Id)))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                userFollowers.Add(new UserFollower
                {
                    UserId = curUser.Id,
                    FollowerId = curFollower.Id
                });

                builder.AppendLine($"Successfully imported Follower {curFollowerName} to User {curUserName}.");
            }

            //var temp = builder.ToString();

            context.AddRange(userFollowers);

            context.SaveChanges();

            return builder.ToString();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var dbUsers = context.Users
                .Select(u => u);

            var dbPictures = context.Pictures
                .Select(p => p);

            var posts = xDoc.Root.Elements();

            var madePosts = new List<Post>();

            var builder = new StringBuilder();

            foreach (var post in posts)
            {
                var caption = post.Element("caption")?.Value ?? "";
                var username = post.Element("user")?.Value;
                var picturePath = post.Element("picture")?.Value;

                var postMaker = dbUsers.SingleOrDefault(u => u.Username.Equals(username));
                var postPicture = dbPictures.SingleOrDefault(p => p.Path.Equals(picturePath));

                if (postMaker is null || postPicture is null || caption.Equals(string.Empty))
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                madePosts.Add(new Post
                {
                    UserId = postMaker.Id,
                    PictureId = postPicture.Id,
                    Caption = caption
                });

                builder.AppendLine($"Successfully imported Post {caption}.");
            }

            context.Posts.AddRange(madePosts);

            context.SaveChanges();

            return builder.ToString();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var users = context.Users
                .Select(u => u);

            var posts = context.Posts
                .Select(p => p);

            var comments = xDoc.Root.Elements();

            var madeComments = new List<Comment>();

            var builder = new StringBuilder();

            foreach (var comment in comments)
            {
                var content = comment.Element("content")?.Value ?? "";
                var username = comment.Element("user")?.Value ?? "";
                var postId = int.Parse(comment.Element("post")?.Attribute("id")?.Value ?? "0");

                var commentMaker = users.SingleOrDefault(u => u.Username.Equals(username));
                var post = posts.SingleOrDefault(p => p.Id.Equals(postId));

                if (commentMaker is null || post is null)
                {
                    builder.AppendLine("Error: Invalid data.");
                    continue;
                }

                madeComments.Add(new Comment
                {
                    Content = content,
                    UserId = commentMaker.Id,
                    PostId = post.Id
                });

                builder.AppendLine($"Successfully imported Comment {content}.");
            }

            context.Comments.AddRange(madeComments);

            context.SaveChanges();

            return builder.ToString();
        }

        private static void SeedDatabase(InstagraphContext context)
        {
            var picture = new Picture()
            {
                Path = "src/folders/resources/images/story/browsed/jpg/p27t303Lra.jpg",
                Size = 44273.27m
            };

            var users = new User[]
            {
                new User()
                {
                    Username = "User1",
                    Password = "AdKs>q]u7P`C",
                    ProfilePicture = picture
                },
                new User()
                {
                    Username = "User2",
                    Password = "AdKs>q]u7P`C",
                    ProfilePicture = picture
                },
                new User()
                {
                    Username = "User3",
                    Password = "AdKs>q]u7P`C",
                    ProfilePicture = picture
                },
            };

            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }
}
