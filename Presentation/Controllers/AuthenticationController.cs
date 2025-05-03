using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Identity;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):ApiBaseController
    {
        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var User= await _serviceManager.AuthenticationService.LgainAsync(loginDto);
            return Ok(User);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto rigsterDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(rigsterDto);
            return Ok(User);
        }
    }
}
