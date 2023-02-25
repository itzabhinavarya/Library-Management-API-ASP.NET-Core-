using Book_Store_Application.Models;
using Book_Store_Application.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_Application.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] Signup signup )
        {
            var result =await _accountRepository.SignupAsync(signup);
            if (result.Succeeded)
                return Ok($"User Added Successfully \n{result.Succeeded}");
            return Unauthorized();
        }
        [HttpPost("Signin")]
        public async Task<IActionResult> Signin([FromBody] Login login)
        {
            var result = await _accountRepository.LoginAsync(login);
            if (string.IsNullOrEmpty(result))
                return Unauthorized();
            return Ok(result);
        }
    }
}
