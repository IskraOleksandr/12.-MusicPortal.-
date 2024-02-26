using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using Azure;
using MusicPortal.DAL.Entities;
using MusicPortal.Filters;

namespace MusicPortal.Controllers
{
    [Culture]
    public class LoginController : Controller
    {
        private readonly IUserService userService;
        public LoginController(IUserService u)
        {
            userService = u;           
        }      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterModel user)
        {
            try
            {
                if (Convert.ToInt32(user.age) < 0 || Convert.ToInt32(user.age) > 99)
                    return Json("age");
            }
            catch { return Json("age"); }
            if (ModelState.IsValid)
            {               
                UserDTO u = new();
                    u.Login = user.Login;
                    u.email = user.email;          
                    u.Password = user.Password;
                    try
                    {
                        await userService.CreateUser(u);                      
                    }
                    catch { return Json(false); }
                 return Json(true);
            }
            return Json(false);
        }    
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel user)
        {
           
            if (ModelState.IsValid)
            {

                var u = await userService.GetUser(user.Login);
                {
                    if (u != null )
                    {
                        if (await userService.CheckPassword(u, user.Password))
                        {
                            string response = "0";
                            HttpContext.Session.SetString("login", user.Login);
                            if (u.Level == 1)
                            { 
                                HttpContext.Session.SetString("level", "level");
                                 response = "1";
                            }
                            if (u.Level == 2)
                            {
                                HttpContext.Session.SetString("admin", "admin");
                                 response = "admin";                              
                            }
                            return Json(response);
                        }                                              
                        else
                        {
                            return Json(false);
                        }
                    }
                    else
                    { 
                        return Json(false);
                    }
                }
            }
            return Json(false);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            bool isUnique = true;
            UserDTO u = await userService.GetEmail(email);
            if (u == null)
                isUnique = false;
            return Json(isUnique);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsLoginInUse( string login)
        {

            bool isUnique = true;
            UserDTO u = await userService.GetUser(login);
            if (u == null)
                isUnique = false;
            return Json(isUnique);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailIn(string email)
        {
            bool isUnique = true;
            UserDTO u = await userService.GetEmail(email);
            if (u == null)
                isUnique = false;
            return Json(!isUnique);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsLoginIn(string login)
        {
            bool isUnique = true;
            UserDTO u = await userService.GetUser(login);
            if (u == null)
                isUnique = false;
            return Json(!isUnique);
        }
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckAge(string age)
        {
            try
            {
                if (Convert.ToInt32(age) < 0 || Convert.ToInt32(age) > 99)
                    return Json(true);
                else
                    return Json(false);
            }
            catch { return Json(true); }
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); // очищается сессия
            return Json(true);
        }
        public ActionResult GetName()
        {
            string response = HttpContext.Session.GetString("login");
            return Json(response);
        }
        public async Task<IActionResult> GetUserLevel()
        {
            string response;
            string login = HttpContext.Session.GetString("login");
            UserDTO u = await userService.GetUser(login);
            if (u != null)
            {
                if (u.Level == 0) 
                 response ="0";
                else if (u.Level == 2)
                    response = "admin";
                else
                    response = "1";
                return Json(response);
            }
            else
                return Json(false);
        }
    }
}
