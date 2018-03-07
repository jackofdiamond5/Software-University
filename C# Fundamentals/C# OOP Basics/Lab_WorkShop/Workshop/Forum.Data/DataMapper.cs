namespace Forum.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using Forum.Models;

    public class DataMapper
    {
        private const string DATA_PATH = "../data/";
        private const string CONFIG_PATH = "config.ini";
        private const string DEFAULT_CONFIG =
            "users=users.csv\r\ncategories=categories.csv\r\nposts=posts.csv\r\nreplies=replies.csv";
        private static readonly IDictionary<string, string> config;

        static DataMapper()
        {
            Directory.CreateDirectory(DATA_PATH);
            config = LoadConfig(DATA_PATH + CONFIG_PATH);
        }

        public static void SaveReplies(IReadOnlyCollection<Reply> replies)
        {
            var lines = new List<string>();
            const string replyFormat = "{0};{1};{2}";

            foreach (var reply in replies)
            {
                var line = string.Format(replyFormat,
                    reply.Id,
                    reply.Content,
                    reply.AuthorId,
                    reply.PostId);

                lines.Add(line);
            }

            WriteLines(config["replies"], lines);
        }

        public static List<Reply> LoadReplies()
        {
            var replies = new List<Reply>();
            var dataLines = ReadLines(config["replies"]);

            foreach (var line in dataLines)
            {
                var args = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(args[0]);
                var content = args[1];
                var authorId = int.Parse(args[2]);
                var postId = int.Parse(args[3]);

                var reply = new Reply(id, content, authorId, postId);
                replies.Add(reply);
            }

            return replies;
        }

        public static void SavePosts(IReadOnlyCollection<Post> posts)
        {
            var lines = new List<string>();
            const string postFormat = "{0};{1};{2}";

            foreach (var post in posts)
            {
                var line = string.Format(postFormat,
                    post.Id,
                    post.Title,
                    post.Content,
                    post.CategoryId,
                    post.AuthorId,
                    string.Join(",", post.ReplyIds));

                lines.Add(line);
            }

            WriteLines(config["posts"], lines);
        }

        public static List<Post> LoadPosts()
        {
            var posts = new List<Post>();
            var dataLines = ReadLines(config["posts"]);

            foreach (var line in dataLines)
            {
                var args = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(args[0]);
                var title = args[1];
                var content = args[2];
                var categoryId = int.Parse(args[3]);
                var authorId = int.Parse(args[4]);
                var replyIds = args[5]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var post = new Post(id, title, content, categoryId, authorId, replyIds);
                posts.Add(post);
            }

            return posts;
        }

        public static void SaveUsers(IReadOnlyCollection<User> users)
        {
            var lines = new List<string>();
            const string userFormat = "{0};{1};{2}";

            foreach (var user in users)
            {
                var line = string.Format(userFormat,
                    user.Id,
                    user.Username,
                    user.Password,
                    string.Join(",", user.PostIds));

                lines.Add(line);
            }

            WriteLines(config["users"], lines);
        }

        public static List<User> LoadUsers()
        {
            var users = new List<User>();
            var dataLines = ReadLines(config["users"]);

            foreach (var line in dataLines)
            {
                var args = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(args[0]);
                var username = args[1];
                var password = args[2];
                var postIds = args[2]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var user = new User(id, username, password, postIds);
                users.Add(user);
            }

            return users;
        }

        public static void SaveCategories(IReadOnlyCollection<Category> categories)
        {
            var lines = new List<string>();
            const string categoryFormat = "{0};{1};{2}";

            foreach (var category in categories)
            {
                var line = string.Format(categoryFormat,
                    category.Id,
                    category.Name,
                    string.Join(",", category.PostIds));

                lines.Add(line);
            }

            WriteLines(config["categories"], lines);
        }

        public static List<Category> LoadCategories()
        {
            var categories = new List<Category>();
            var dataLines = ReadLines(config["categories"]);

            foreach (var line in dataLines)
            {
                var args = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var id = int.Parse(args[0]);
                var name = args[1];
                var postIds = args[2]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var category = new Category(id, name, postIds);
                categories.Add(category);
            }

            return categories;
        }

        private static void WriteLines(string path, IEnumerable<string> lines)
        {
            File.WriteAllLines(path, lines);
        }

        private static string[] ReadLines(string path)
        {
            EnsureFile(path);

            var lines = File.ReadAllLines(path);
            return lines;
        }

        private static IDictionary<string, string> LoadConfig(string configPath)
        {
            EnsureConfigFile(configPath);

            var contents = ReadLines(configPath);

            var config = contents
                .Select(l => l.Split('='))
                .ToDictionary(t => t[0], t => DATA_PATH + t[1]);

            return config;
        }

        private static void EnsureFile(string path)
        {
            if (!File.Exists(path))
            {
                using (var fileStream = File.Create(path)) { }
            }
        }

        private static void EnsureConfigFile(string configPath)
        {
            if (!File.Exists(configPath))
            {
                File.WriteAllText(configPath, DEFAULT_CONFIG);
            }
        }
    }
}
