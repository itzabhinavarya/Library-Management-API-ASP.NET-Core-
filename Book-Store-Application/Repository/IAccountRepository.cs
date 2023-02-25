using Book_Store_Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store_Application.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignupAsync(Signup signup);
        Task<string> LoginAsync(Login login);
    }
}
