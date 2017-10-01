using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KUniversity.Models;
using KUniversity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly KUniversityContext _context;

        public HomeController(KUniversityContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginVM)
        {
            if (loginVM == null)
            {
                return View();
            }
            return View(loginVM);
        }

        [ActionName("Login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.Student.
                    SingleOrDefaultAsync(s => s.Id == vm.AccountID);

                string pwd = student?.Password ?? string.Empty;

                if (pwd.Equals(vm.Password))
                {
                    Program.AccoutnID = student.Id;
                    //HttpContext.Session.Set("Account", student.ID);
                    return RedirectToAction("Index", "Student/StudentHome");
                }

                var instructor = await _context.Instructor.
                    SingleOrDefaultAsync(i => i.Id == vm.AccountID);
                pwd = instructor?.Password ?? string.Empty;

                if (pwd.Equals(vm.Password))
                {
                    Program.AccoutnID = instructor.Id;
                    //HttpContext.Session.Set("Account", instructor.ID);
                    return RedirectToAction("Index", "Instructor/InstructorHome");
                }

                //ModelState.AddModelError(string.Empty, "用户名或密码错误");
                //return RedirectToAction("Index", vm);
            }

            return RedirectToAction("LoginFail");
        }

        public IActionResult AdminLogin()
        {
            return View();
        }

        [ActionName("TryAdminLogin")]
        public async Task<IActionResult> AdminLoginAsync(AdminLoginViewModel vm)
        {
            bool loginValid = false;

            if (ModelState.IsValid)
            {
                var admin = await _context.Admin
                    .SingleOrDefaultAsync(a => a.Password.Equals(vm.Password));

                loginValid = admin != null;
            }
            return loginValid ? RedirectToAction("Index", "Admin/KUAdmins") : RedirectToAction("AdminLoginFail");
        }

        public IActionResult LoginFail()
        {
            return View();
        }

        public IActionResult AdminLoginFail()
        {
            return View();
        }
    }
}
