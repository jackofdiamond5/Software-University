namespace Forum.App.Controllers
{
    using Forum.App.Views;
    using Forum.App.Services;
    using Forum.App.Exceptions;
    using Forum.App.UserInterface;
    using Forum.App.Controllers.Contracts;
    using Forum.App.UserInterface.Contracts;

    public class PostDetailsController : IController, IUserRestrictedController
    {
        public bool LoggedInUser { get; set; }

        public int PostId { get; private set; }

        public MenuState ExecuteCommand(int index)
        {
            switch ((Command)index)
            {
                case Command.AddReply:
                    return MenuState.AddReplyToPost;
                case Command.Back:
                    ForumViewEngine.ResetBuffer();
                    return MenuState.Back;
                default:
                    throw new InvalidCommandException();
            }
        }
        
        public IView GetView(string userName)
        {
            var pmv = PostService.GetPostViewModel(this.PostId);
            return new PostDetailsView(pmv, this.LoggedInUser);
        }

        public void SetPostId(int postId)
        {
            this.PostId = postId;
        }

        public void UserLogIn()
        {
            this.LoggedInUser = true;
        }

        public void UserLogOut()
        {
            this.LoggedInUser = false;
        }

        private enum Command
        {
            Back,
            AddReply
        }
    }
}
