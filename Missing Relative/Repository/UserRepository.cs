
using Microsoft.AspNetCore.Identity;
using Missing_Relative.Models;

namespace Missing_Relative.Repository
{

    public class UserRepository : IUserRepository
        {
            private readonly UserManager<IdentityUser> userManager;
            public UserRepository(UserManager<IdentityUser> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<IdentityResult> CreateUser(SignupModel signupmodel)
            {
                var user = new IdentityUser()
                {
                    Email = signupmodel.Email,
                    UserName = signupmodel.Email
                };

                var result = await userManager.CreateAsync(user, signupmodel.Password);
                return result;
            }

        }
    }


