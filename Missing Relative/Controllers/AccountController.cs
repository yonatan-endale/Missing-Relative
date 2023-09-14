using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Missing_Relative.Models;
using Missing_Relative.Repository;

namespace Missing_Relative.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository iuserrepository;
        private UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IServiceProvider serviceProvider;
        private readonly RoleManager<IdentityRole> rolemanager;

        public AccountController(UserManager<IdentityUser> usrMgr,IUserRepository iuserRepository
          , IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager)
        {
            userManager = usrMgr;
            this.iuserrepository = iuserRepository;
            this.signInManager = signInManager;
            this.serviceProvider = serviceProvider;
            this.rolemanager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task defualt()
        {
            string email = "admin@gmail.com";
            var password = "123ADMIN_admin";

            //await signInManager.SignOutAsync();
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = email,
                    Email = email,

                };

                var result = await userManager.CreateAsync(newUser, password);
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roleExist = await RoleManager.RoleExistsAsync("Admin");

                if (!roleExist)
                {
                    await RoleManager.CreateAsync(new IdentityRole("Admin"));
                }
                await userManager.AddToRoleAsync(newUser, "Admin");
            }

        }
        public async Task<IActionResult> signIn(SignInModel signInModel)
        {

            await defualt();

            if (!ModelState.IsValid)
                return View(signInModel);

            var user = await userManager.FindByEmailAsync(signInModel.Email);
            if (user != null)
            {
                //user is found check password
                var passwordcheck = await userManager.CheckPasswordAsync(user, signInModel.Password);
                if (passwordcheck)
                {
                    //	var checkRole= userManager.IsInRoleAsync();
                    // password correct signing in
                    var result = await signInManager.PasswordSignInAsync(user, signInModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        //	if(checkRole)
                        return RedirectToAction("Index", "Home");
                    }
                }
                // password is incorrect

                TempData["Error"] = "wrong credentials.please try again";

                return View(signInModel);
            }
            //user not found

            TempData["Error"] = "wrong credentails. please try again";
            return View(signInModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupModel signupModel)
        {
            if (!ModelState.IsValid)
                return View(signupModel);
            var user = await userManager.FindByEmailAsync(signupModel.Email);
            if (user != null)
            {
                TempData["Error"] = "This Email Address is already in Use";
                return View(signupModel);
            }

            var newUser = new IdentityUser
            {
                UserName = signupModel.Email,
                Email = signupModel.Email

            };

            var result = await userManager.CreateAsync(newUser, signupModel.Password);
            if (result.Succeeded)
            {
                //checks if that Role exists if it doesn't it creates it

                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roleExist = await RoleManager.RoleExistsAsync("User");
                if (!roleExist)
                {
                    await RoleManager.CreateAsync(new IdentityRole("User"));
                }

                await userManager.AddToRoleAsync(newUser, "User");
                TempData["message"] = "register successfull";
                return RedirectToAction("Index", "Home");
            }
            return View(signupModel);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




        public IActionResult Index()
        {

            return View();
        }
    }
}
