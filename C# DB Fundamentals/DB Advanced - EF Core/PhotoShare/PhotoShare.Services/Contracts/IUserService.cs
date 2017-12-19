namespace PhotoShare.Services.Contracts
{
    using Models;

    public interface IUserService
    {
        User ById(int id);

        User ByUserName(string username);

        User ByUserNameAndPassword(string username, string password);

        User Create(string username, string password, string email);

        void Delete(string username);

        User ModifyPassword(string username, string newPassword);

        User ModifyBorntown(string username, string newBornTown);

        User ModifyCurrentTown(string username, string newCurrentTown);

        void AddFriend(string firstUsername, string secondUsername);

        void AceptFriend(string firstUsername, string secondUsername);

        string ListFriends(string username);
    }
}
