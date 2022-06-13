using Dal.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Leaves
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(RegisterViewModel model);
        Task<string> LoginAsync(LoginViewModel loginViewModel);
    }
}
