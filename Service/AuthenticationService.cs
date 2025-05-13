using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.Identity;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var User= await _userManager.FindByEmailAsync(email);
            return User is not null;
           
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var User= await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundExpception(email);
            return new UserDto()
            {
                Email = User.Email,
                DisplyName=User.DisplayName,
                Token= await CreateTokenAsync(User),
            };
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
             var User= await _userManager.Users.Include(U => U.Address)
                                        .FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundExpception(email);

            if(User.Address is not null)
                return _mapper.Map<Address,AddressDto>(User.Address);
            else
                throw new AddressNotFoundException(email);

        }

       
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var User = await _userManager.Users.Include(U => U.Address)
                                       .FirstOrDefaultAsync(U => U.Email == email) ?? throw new UserNotFoundExpception(email);

            if (User.Address is not null)
            {
                User.Address.FristName = addressDto.Firstname;
                User.Address.LastName=addressDto.Lastname;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street=addressDto.Street;
            }
            else
            {
                User.Address= _mapper.Map<AddressDto,Address>(addressDto);
            }
            await _userManager.UpdateAsync(User);

            return _mapper.Map<Address, AddressDto>(User.Address);
                
        }


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
                    Token = await CreateTokenAsync(User)
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
                return new UserDto() { DisplyName=User.DisplayName,Email= User.Email,Token= await CreateTokenAsync(User) };
            }
            else
            {
                var Errors=Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser User)
        {
            var Claims = new List<Claim>()
            {
                new Claim (ClaimTypes.Email, User.Email!),
                new Claim (ClaimTypes.Name,User.UserName!),
                new Claim(ClaimTypes.NameIdentifier, User.Id!)
            };
            var Roles=await _userManager.GetRolesAsync(User);

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecretyeKey = _configuration.GetSection("JWTOptions")["SecretyKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretyeKey));

            var Cred =new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWTOptions")["Issuer"],
                audience: _configuration.GetSection("JWTOptions")["Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Cred
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
