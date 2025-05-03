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
    }
}
