using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using MusicPortal.Models;
using MusicPortal.Filters;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;

namespace MusicPortal.Controllers
{
    [Culture]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService u)
        {
            userService = u;
        }
        public async Task<IActionResult> Index()
        {
            return View(await userService.GetAllUsers());
        }

        public ActionResult Login()
        {
            return PartialView("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var u = await userService.GetUser(logon.Login);
                {
                    if (u != null)
                    {
                        if (await userService.CheckPassword(u, logon.Password))
                        {
                            HttpContext.Session.SetString("Login", u.Login);
                            HttpContext.Session.SetInt32("Level", u.Level.Value);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Не коректный логин или пароль!");
                            return PartialView(logon);
                        }
                        return PartialView("~/Views/Music/Success.cshtml");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не коректный логин или пароль!" + u.Password);
                        return PartialView(logon);
                    }
                }
            }
            return PartialView(logon);
        }

        public IActionResult Register()
        {
            return PartialView("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register_Model reg)
        {
            if (reg.Login == "admin")
                ModelState.AddModelError("Login", "admin - запрещенный логин");

            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO();
                user.First_Name = reg.FirstName;
                user.Last_Name = reg.LastName;
                user.Login = reg.Login;
                user.email = reg.Email;



                user.Password = reg.Password;
                user.Level = reg.Level;

                try
                {
                    await userService.CreateUser(user);
                }
                catch
                {
                    ModelState.AddModelError("", "Не коректный логин или пароль!");
                    return PartialView(reg);
                }
                return PartialView("~/Views/Music/Success.cshtml");
            }

            return PartialView("Register");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await userService.GetAllUsers() == null)
            {
                return NotFound();
            }

            var user = await userService.GetUser((int)id);
            if (user == null)
            {
                return NotFound();
            }
            AddUser addUser = UserDTO_To_AddUser(user);

            return PartialView("Edit", addUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Login,Email,Password,Salt,Level")] UserDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    userService.UpdateUser(user);
                }
                catch { return View("Edit", user); }
                return PartialView("~/Views/Music/Success.cshtml");
            }
            return PartialView("Edit", user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await userService.GetAllUsers() == null)
            {
                return NotFound();
            }

            var user = await userService.GetUser((int)id);
            if (user == null)
            {
                return NotFound();
            }
            AddUser addUser = UserDTO_To_AddUser(user);


            return PartialView("Delete", addUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await userService.GetAllUsers() == null)
            {
                return Problem("Entity set 'Music_PortalContext.Students'  is null.");
            }

            var user = await userService.GetUser(id);
            if (user != null)
            {
                await userService.DeleteUser(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(int id)
        {
            IEnumerable<UserDTO> list = await userService.GetAllUsers();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            if (email == "admin@mail.ru" || email == "admin@gmail.com")
                return Json(false);
            return Json(true);
        }

        private AddUser UserDTO_To_AddUser(UserDTO user)
        {
            AddUser addUser = new AddUser();
            addUser.Id = user.Id;
            addUser.FirstName = user.First_Name;
            addUser.LastName = user.Last_Name;
            addUser.Login = user.Login;
            addUser.Email = user.email;
            addUser.Level = user.Level.Value;
            return addUser;
        }
    }
}
