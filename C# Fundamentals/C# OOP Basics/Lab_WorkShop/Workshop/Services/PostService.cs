namespace Forum.App.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using Forum.Data;
    using Forum.Models;
    using Forum.App.UserInterface.ViewModels;

    public static class PostService
    {
        public static bool TrySavePost(PostViewModel postView)
        {
            var emptyCategory = string.IsNullOrWhiteSpace(postView.Category);
            var emptyTitle = string.IsNullOrWhiteSpace(postView.Title);
            var emptyContent = !postView.Content.Any();

            if (emptyCategory || emptyTitle || emptyContent)
            {
                return false;
            }

            var forumData = new ForumData();
            var postId = forumData.Posts.Any() ? forumData.Posts.Last().Id + 1 : 1;
            var category = EnsureCategory(postView, forumData);
            var author = UserService.GetUser(postView.Author);
            var authorId = author.Id;
            var content = string.Join("", postView.Content);

            var post =
                new Post(postId, postView.Title, content, category.Id, authorId, new List<int>());

            forumData.Posts.Add(post);
            author.PostIds.Add(post.Id);
            category.PostIds.Add(post.Id);
            forumData.SaveChanges();

            return true;
        }

        public static PostViewModel GetPostViewModel(int postId)
        {
            var forumData = new ForumData();
            var post = forumData.Posts.Find(p => p.Id == postId);
            var pvm = new PostViewModel(post);

            return pvm;
        }

        public static IEnumerable<Post> GetPostByCategory(int categoryId)
        {
            var forumData = new ForumData();
            var postIds = forumData.Categories.First(c => c.Id == categoryId).PostIds;

            var posts = forumData.Posts.Where(p => postIds.Contains(p.Id));
            return posts;
        }

        internal static Category GetCategory(int categoryId)
        {
            var forumData = new ForumData();
            var category = forumData.Categories.Find(c => c.Id == categoryId);

            return category;
        }

        internal static IList<ReplyViewModel> GetReplies(int postId)
        {
            var forumData = new ForumData();
            var post = forumData.Posts.Find(p => p.Id == postId);

            var replies = new List<ReplyViewModel>();
            foreach (var replyId in post.ReplyIds)
            {
                var reply = forumData.Replies.Find(r => r.Id == replyId);
                replies.Add(new ReplyViewModel(reply));
            }

            return replies;
        }

        internal static string[] GetAllCategoryNames()
        {
            var forumData = new ForumData();
            var allCategories = forumData.Categories.Select(c => c.Name).ToArray();

            return allCategories;
        }

        private static Category EnsureCategory(PostViewModel postView, ForumData forumData)
        {
            var categoryName = postView.Category;
            var category = forumData.Categories.FirstOrDefault(i => i.Name == categoryName);

            if (category == null)
            {
                var categories = forumData.Categories;
                var categoryId = categories.Any() ? categories.Last().Id + 1 : 1;
                category = new Category(categoryId, categoryName, new List<int>());
                forumData.Categories.Add(category);
            }

            return category;
        }
    }
}
