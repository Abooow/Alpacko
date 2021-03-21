using Alpacko.Client.AdminConsole.Services;
using System;
using System.Linq;
using System.Net.Http;

namespace Alpacko.Client.AdminConsole.Views
{
    public class MainMenuView : IView
    {
        private AuthService authService;

        public MainMenuView(AuthService authService)
        {
            this.authService = authService;
        }

        public void Render()
        {
            string[] validInput = new string[] { "1", "2", "3", "out", "exit" };
            string input;

            do
            {
                if (!authService.UserIsAuthenticated)
                    new SignInView(authService).Render();

                Console.Clear();
                Console.WriteLine("~~~~~~ Main Menu ~~~~~~~");
                Console.WriteLine($"Signed in as: {authService.SignInUserEmail}");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("1) Create/View Post Offices");
                Console.WriteLine("2) Create/View User Roles");
                Console.WriteLine("3) Create/View Users");
                Console.WriteLine();
                Console.WriteLine("\"out\" to sign out");
                Console.WriteLine("\"exit\" to exit");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.Write("> ");
                input = Console.ReadLine().Trim().ToLower();
            } while (!validInput.Contains(input));

            switch (input)
            {
                case "1":
                    ViewManager.RenderView<PostOfficeView>();
                    break;

                case "out":
                    authService.SignOut();
                    ViewManager.RenderView<SignInView>();
                    break;

                case "exit":
                    return;
            }

            Render();
        }
    }
}