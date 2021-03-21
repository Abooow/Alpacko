using Alpacko.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.Client.Web.Services
{
    public interface IAuthService
    {
        Task<SignInResultModel> SignIn(SignInModel signInModel);
        Task SignOut();
        Task<SignUpResultModel> SignUp(SignUpModel signUpModel);
    }
}
