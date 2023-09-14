using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Missing_Relative.Repository;

namespace Missing_Relative.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository iuserrepository;
        private UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> usrMgr,IUserRepository iuserRepository)
        {
            userManager = usrMgr;
            this.iuserrepository = iuserRepository;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Index()
        {

            return View();
        }
    }
}
