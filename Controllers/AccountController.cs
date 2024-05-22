using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication16.Models;
using WebApplication16.ViewModel.Account;

namespace WebApplication16.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;

        public AccountController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async IActionResult Result(RegisterVm registerVm)
        {
            if (!ModelState.IsValid) return View();

            User newUser = new User()
            {
                Name = registerVm.Name,
                Email = registerVm.Email,
                Surname = registerVm.Surname,
                UserName = registerVm.UserName,
            };

            var result = await userManager.CreateAsync(newUser,registerVm.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            return RedirectToAction("Login");
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login(LoginVm loginVm)
        {
            return View();

        }
    }
}
