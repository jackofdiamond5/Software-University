﻿namespace Forum.App.Controllers
{
    using Forum.App;
    using Forum.App.Services;
    using Forum.App.UserInterface;
    using Forum.App.Controllers.Contracts;
    using Forum.App.UserInterface.Contracts;

    using System;

    public class SignUpController : IController, IReadUserInfoController
    {
        private const string DETAILS_ERROR = "Invalid Username or Password!";
        private const string USERNAME_TAKEN_ERROR = "Username already in use!";

        public string Username { get; private set; }

        private string Password { get; set; }

        private string ErrorMessage { get; set; }

        public MenuState ExecuteCommand(int index)
        {
            switch ((Command)index)
            {
                case Command.ReadUsername:
                    this.ReadUsername();
                    return MenuState.Signup;

                case Command.ReadPassword:
                    this.ReadPassword();
                    return MenuState.Signup;

                case Command.SignUp:
                    var signUp = UserService.TrySignUpUser(this.Username, this.Password);
                    switch (signUp)
                    {
                        case SignUpStatus.Success:
                            return MenuState.SignedUp;

                        case SignUpStatus.DetailsError:
                            this.ErrorMessage = USERNAME_TAKEN_ERROR;
                            return MenuState.SignUpError;
                    }
                    break;

                case Command.Back:
                    this.ResetSignUp();
                    return MenuState.Back;
            }

            throw new InvalidOperationException();
        }

        public IView GetView(string userName)
        {
            return new SignUpView(this.ErrorMessage);
        }

        public void ReadPassword()
        {
            this.Password = ForumViewEngine.ReadRow();
            ForumViewEngine.HideCursor();
        }

        public void ReadUsername()
        {
            this.Username = ForumViewEngine.ReadRow();
            ForumViewEngine.HideCursor();
        }

        private void ResetSignUp()
        {
            this.ErrorMessage = string.Empty;
            this.Username = string.Empty;
            this.Password = string.Empty;
        }

        private enum Command
        {
            ReadUsername, ReadPassword, SignUp, Back
        }
    }
}
