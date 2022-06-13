using Bal.Leaves;
using Dal.Models;
using Dal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProject.Controllers
{
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<Users> userManager;
        private readonly SignInManager<Users> signInManager;
        private readonly ILeaveApiRepository _leaveApiRepository;
        public AccountController(ILeaveApiRepository leaveApiRepository, IAccountRepository accountRepository, UserManager<Users> userManager,
                                  SignInManager<Users> signInManager)
        {
            _accountRepository = accountRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _leaveApiRepository = leaveApiRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterViewModel registerViewModel)
        {
            var result = await _accountRepository.SignUpAsync(registerViewModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }
        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            

                var result = await _accountRepository.LoginAsync(model);
                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized();

                }

            var info =  await _leaveApiRepository.GetEmail(model);
            string y = result +"\n"+ info;
            //await _leaveApiRepository.Get();
                return Ok(y);


        }
    }
}
