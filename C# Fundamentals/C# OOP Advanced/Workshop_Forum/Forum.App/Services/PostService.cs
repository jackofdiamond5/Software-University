namespace Forum.App.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Forum.Data;
    using Forum.DataModels;
    using Forum.App.Contracts;
    using Forum.App.Contracts.ViewModels;

    public class PostService : IPostService
    {
        private ForumData forumData;
        private ISession session;
        private IUserService userService;


        public PostService(ForumData forumData, ISession session, IUserService userService)
        {
            this.forumData = forumData;
            this.session = session;
            this.userService = userService;
        }

        public int AddPost(int userId, string postTitle, string postCategory, string postContent)
        {
            var emptyCategory = string.IsNullOrWhiteSpace(postCategory);
            var emptyTitle = string.IsNullOrWhiteSpace(postTitle);
            var emptyContent = string.IsNullOrWhiteSpace(postContent);

            if(emptyCategory || emptyTitle || emptyContent)
            {
                throw new ArgumentException("All fields mus be filled!");
            }

            var category = this.EnsureCategory(postCategory);
            var postId = forumData.Posts.Any() ? forumData.Posts.Last()?.Id + 1 : 1;
            var author = this.userService.GetUserById(userId);
            var post = new Post(postId ?? 1, postTitle, postContent, category.Id, author.Id, new List<int>());

            this.forumData.Posts.Add(post);
            author.Posts.Add(post.Id);
            category.Posts.Add(post.Id);
            this.forumData.SaveChanges();

            return post.Id;
        }

        public void AddReplyToPost(int postId, string replyContents, int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICategoryInfoViewModel> GetAllCategories()
        {
            var categories = this.forumData.Categories.Select(c => new CategoryInfoViewModel(c.Id, c.Name, c.Posts.Count));

            return categories;
        }

        public string GetCategoryName(int categoryId)
        {
            var categoryName = this.forumData.Categories.Find(c => c.Id == categoryId)?.Name;

            if(categoryName == null)
            {
                throw new ArgumentException($"Category with id {categoryId} not found!");
            }

            return categoryName;
        }

        public IEnumerable<IPostInfoViewModel> GetCategoryPostsInfo(int categoryId)
        {
            var posts = (IEnumerable<IPostInfoViewModel>)this.forumData.Posts.Where(c => c.Id == categoryId);

            return posts;
        }

        public IPostViewModel GetPostViewModel(int postId)
        {
            var post = this.forumData.Posts.FirstOrDefault(p => p.Id == postId);

            var postPreview = new PostViewModel(post.Content,
                this.userService.GetUserName(post.AuthorId), this.GetPostReplies(postId));

            return postPreview;
        }

        public User GetUserById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public string GetUserName(int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool TryLogInUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool TrySignUpUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        private Category EnsureCategory(string postCategory)
        {
            var category = this.forumData.Categories.FirstOrDefault(c => c.Name == postCategory);

            if (category == null)
            {
                throw new InvalidOperationException($"Category with name: \"{postCategory}\" does not exist!");
            }

            return category;
        }

        private IEnumerable<IReplyViewModel> GetPostReplies(int postId)
        {
            var replies = this.forumData.Replies
                .Where(r => r.PostId == postId)
                .Select(r => new ReplyViewModel(this.userService.GetUserName(r.AuthorId), r.Content));

            return replies;
        }
    }
}
