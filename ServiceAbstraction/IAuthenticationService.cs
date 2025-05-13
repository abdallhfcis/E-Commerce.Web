using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Identity;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDto> LgainAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        //Check Email
        Task<bool> CheckEmailAsync(string email);
        //GetCurrentUserAddress
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        //UpdateCurrentUserAddress
        Task<AddressDto> UpdateCurrentUserAddressAsync (string email,AddressDto addressDto);
        //GetCurrentUser
        Task<UserDto> GetCurrentUserAsync(string email);
    }
}
