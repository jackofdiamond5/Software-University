namespace Forum.App.Contracts.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    public class PostViewModel : ContentViewModel, IPostViewModel
    {
        public PostViewModel(string content, string author, IEnumerable<IReplyViewModel> replies)
            : base(content)
        {
            this.Title = Title;
            this.Author = author;
            this.Replies = replies.ToArray();
        }

        public string Title { get; }

        public string Author { get; }

        public IReplyViewModel[] Replies { get; }
    }
}
