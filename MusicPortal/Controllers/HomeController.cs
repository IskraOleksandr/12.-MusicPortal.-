using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System.Diagnostics;
using AutoMapper;
using Newtonsoft.Json;
using MusicPortal.Filters;

namespace MusicPortal.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly IMusicService songService;
        public HomeController(IMusicService song)
        {
            songService = song;          
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<MusicDTO> s = await songService.GetAllSongs();
            ViewBag.Songs = s;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Find(string str)
        {
            IEnumerable<MusicDTO> s = await songService.FindSongs(str);
            string response = JsonConvert.SerializeObject(s);
            return Json(response);
        }
        public async Task<IActionResult> AllSongs()
        {
            IEnumerable<MusicDTO> s = await songService.GetAllSongs();
            string response = JsonConvert.SerializeObject(s);
             return Json(response);
           
        }
        public ActionResult ChangeCulture(string lang)
        {
           // string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home/Index";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "uk", "de", "fr" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            //return Redirect(returnUrl);
            return Redirect("/Home/Index");
        }
    }
}