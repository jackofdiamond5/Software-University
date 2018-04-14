namespace Forum.App.Commands
{
    using Contracts;

    public class PostCommand : ICommand
    {
        private ISession session;
        private ICommandFactory commandFactory;
        private IPostService postService;

        public PostCommand(ISession session, ICommandFactory commandFactory, IPostService postService)
        {
            this.session = session;
            this.commandFactory = commandFactory;
            this.postService = postService;
        }

        public IMenu Execute(params string[] args)
        {
            var userId = this.session.UserId;

            var postTitle = args[0];
            var postCategory = args[1];
            var postContent = args[2];

            var postId = this.postService.AddPost(userId, postTitle, postCategory, postContent);

            this.session.Back();
            var viewPostCommand = this.commandFactory.CreateCommand("ViewPostMenu");

            return viewPostCommand.Execute(postId.ToString());
        }
    }
}
