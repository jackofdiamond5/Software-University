using System;
using System.Linq;
using System.Xml.Linq;
using AutoMapper.QueryableExtensions;
using Instagraph.Data;
using Newtonsoft.Json;
using Instagraph.DataProcessor.SerializeTemplates;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var dbPosts = context.Posts.Select(p => new
            {
                p.Id,
                Picture = p.Picture.Path,
                User = p.User.Username,
                CommentsCount = p.Comments.Count
            })
            .OrderBy(p => p.Id)
            .Where(p => p.CommentsCount.Equals(0))
            .ToArray();

            var json = JsonConvert.SerializeObject(dbPosts);

            var jsonParsed = JsonConvert.DeserializeObject<PostTemplate[]>(json);

            var jsonOutput = JsonConvert.SerializeObject(jsonParsed, Formatting.Indented);

            return jsonOutput;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var dbUsers = context.Users
                .Where(u => u.Posts.
                        Any(p => p.Comments
                            .Any(c => u.Followers.Any(f => f.FollowerId.Equals(c.UserId)
                            )
                        )
                    )
                )
                .OrderBy(u => u.Id)
                .ProjectTo<UserTemplate>()
                .ToArray();

            var json = JsonConvert.SerializeObject(dbUsers);

            //var jsonParsed = JsonConvert.DeserializeObject<UserTemplate[]>(json);

            //var jsonOutput = JsonConvert.SerializeObject(jsonParsed, Formatting.Indented);

            return json;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var dbUsers = context.Users.Select(u => new
            {
                u.Username,
                MostComments = u.Posts.Count.Equals(0) ? 0 : u.Posts
                .Select(p => p.Comments.Count)
                .OrderByDescending(c => c)
                .First()
            })
            .OrderByDescending(u => u.MostComments)
            .ThenBy(u => u.Username)
            .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));

            foreach (var dbUser in dbUsers)
            {
                xDoc.Element("users")
                    .Add(new XElement("user",
                    new XElement("Username", dbUser.Username),
                    new XElement("MostComments", dbUser.MostComments)
                    )
                );
            }

            return xDoc.ToString();
        }
    }
}
