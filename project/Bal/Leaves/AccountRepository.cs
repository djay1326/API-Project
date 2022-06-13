using Dal.Models;
using Dal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bal.Leaves
{
    public class AccountRepository : IAccountRepository
    {
        private SignInManager<Users> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> userManager;
        public AccountRepository(SignInManager<Users> signInManager, UserManager<Users> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> SignUpAsync(RegisterViewModel model)
        {
            var user = new Users() 
            { 
                
                FirstName = model.FirstName,
                
                Email = model.Email,
                UserName = model.Email
            };
            return await userManager.CreateAsync(user, model.Password);
        }
        public async Task<string> LoginAsync(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,loginViewModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials : new SigningCredentials(authSignInKey,SecurityAlgorithms.HmacSha256Signature)
                );
           
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
