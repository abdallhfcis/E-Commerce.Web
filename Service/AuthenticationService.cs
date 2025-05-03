using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.Identity;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LgainAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundExpception(loginDto.Email);

            var IsPasswordvalid = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordvalid)
            {
                return new UserDto()
                {
                    DisplyName = User.DisplayName,
                    Email = User.Email,
                    Token = CreateTokenAsync(User)
                };
            }
            else throw new UnauthorizedException();
        }

      

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var Result= await _userManager.CreateAsync(User);
            if (Result.Succeeded) 
            { 
                return new UserDto() { DisplyName=User.DisplayName,Email= User.Email,Token= CreateTokenAsync(User) };
            }
            else
            {
                var Errors=Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        private static string CreateTokenAsync(ApplicationUser User)
        {
            return "To_Do";
        }
    }
}
