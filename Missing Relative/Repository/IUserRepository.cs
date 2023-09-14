using Microsoft.AspNetCore.Identity;
using Missing_Relative.Models;

namespace Missing_Relative.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(SignupModel signupmodel);
    }
}
