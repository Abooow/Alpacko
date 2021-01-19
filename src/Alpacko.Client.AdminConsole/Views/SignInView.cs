using Alpacko.Client.AdminConsole.Models;
using Alpacko.Client.AdminConsole.Services;
using Alpacko.Client.AdminConsole.Validation;
using System;

namespace Alpacko.Client.AdminConsole.Views
{
    public class SignInView : IView
    {
        private AuthService authService;

        public SignInView(AuthService authService)
        {
            this.authService = authService;
        }

        public void Render()
        {
            SignInResultModel signInResult = new SignInResultModel();
            string errorMessage = null;

            while (!signInResult.Successful || !authService.UserHasRole("Admin"))
            {
                Console.Clear();

                if (errorMessage != null)
                {
                    Utils.WriteLineInColor(errorMessage, Utils.ErrorColor);
                    Console.WriteLine();
                }

                Console.WriteLine("~~~~~~~~ Sign In ~~~~~~~");
                SignInModel signInModel = Validate.GetValidInstance<SignInModel>();
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");

                Console.WriteLine("Signing in...");

                signInResult = authService.SignIn(signInModel).GetAwaiter().GetResult();
                errorMessage = signInResult.ErrorMessage;
                Console.WriteLine();

                if (signInResult.Successful && !authService.UserHasRole("Admin"))
                    errorMessage = "This user does not have the authorization to access this app.";
            }

            if (signInResult.Successful)
                Utils.WriteLineInColor("Successfully signed in!", Utils.SuccessColor);

            Input.PressAnyKeyToContinue();
        }
    }
}