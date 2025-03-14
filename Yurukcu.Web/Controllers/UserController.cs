using Microsoft.AspNetCore.Mvc;
using Yurukcu.Web.Data;
using Yurukcu.Web.Entity;
using Yurukcu.Web.Models;

namespace Yurukcu.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;

        public UserController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult UserLogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogIn(string password, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.EMail == email && u.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEMail", user.EMail);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult UserSignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserSignUp(UserCreateViewModel model)
        {
            var entity = new User
            {
                EMail = model.EMail,
                Password = model.Password,
            };
            _context.Users.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}