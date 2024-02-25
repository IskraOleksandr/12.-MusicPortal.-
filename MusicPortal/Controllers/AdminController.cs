using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusikPortal.Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Infrastructure;
using System.IO;
using MusicPortal.BLL.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MusicPortal.Filters;

namespace MusikPortal.Controllers
{
    [Culture]
    public class AdminController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private readonly IMusicService songService;
        private readonly ISingerService artistService;
        private readonly IMusicStyleService styleService;
        private readonly IUserService userService;
        public AdminController(IMusicService s, ISingerService a, IMusicStyleService st, IUserService u, IWebHostEnvironment appEnvironment)
        {
            songService = s;
            artistService = a;
            styleService = st;
            userService = u;

            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> Styles()
        {
            IEnumerable<MusicStyleDTO> s = await styleService.GetAllStyles();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");
            return PartialView("Styles");
        }
        public async Task<IActionResult> Artists()
        {
            IEnumerable<SingerDTO> a = await artistService.GetAllArtists();
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
            return PartialView("Artists");
        }
        public async Task<IActionResult> EditArtist(int id)
        {
            SingerDTO a = await artistService.GetArtist(id);
            return PartialView("EditArtist", a);
        }
        public async Task<IActionResult> EditStyle(int id)
        {
            MusicStyleDTO a = await styleService.GetStyle(id);
            return PartialView("EditStyle", a);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStyle(MusicStyleDTO s)
        {
            MusicStyleDTO style = new MusicStyleDTO();
            style.StyleName = s.StyleName;
            if (ModelState.IsValid)
            {
                try
                {
                    await styleService.AddStyle(style);
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    await putStyles();
                    return View("Styles", s);
                }

            }
            await putStyles();
            return View("Styles", s);
        }
        [HttpPost]
        public async Task<IActionResult> CreateArtist(string Name)
        {

            SingerDTO art = new SingerDTO();
            art.SingerName = Name;

            try
            {
                await artistService.AddArtist(art);
                return Json(true);
            }
            catch
            {
                await putArtists();
                return Json(false);
            }

            await putArtists();
            return Json(false);
        }
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStyle(int id)
        {
            try
            {
                /* await styleService.DeleteStyle(s.Id);
                 return RedirectToAction("Index", "Home");*/
                MusicStyleDTO a = await styleService.GetStyle(id);
                return PartialView(a);
            }
            catch
            {
                await putStyles();
                return Json(false);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            try
            {
                SingerDTO a = await artistService.GetArtist(id);
                return PartialView(a);
            }
            catch
            {
                await putArtists();
                return Json(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteArtist(int id)
        {
            try
            {
                await artistService.DeleteArtist(id);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDeleteStyle(int id)
        {
            try
            {
                await styleService.DeleteStyle(id);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelDeleteArtist()
        {
            await putArtists();
            return View("Artists");
        }
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStyle(MusicStyleDTO s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await styleService.UpdateStyle(s.Id, s.StyleName);
                    return Json(true);

                }
                catch
                {
                    await putStyles();
                    // return View("Styles");
                    return Json(false);
                }

            }
            await putStyles();
            return Json(false);
        }
        [HttpPost]
        public async Task<IActionResult> EditArtist(SingerDTO s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                        await artistService.UpdateArtist(s.Id, s.SingerName);
                        return Json(true);
                    
                   
                }
                catch
                {
                    return Json(false);
                }

            }
            return Json(false);
        }
        public async Task putStyles()
        {
            IEnumerable<MusicStyleDTO> s = await styleService.GetAllStyles();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");
        }
        public async Task putArtists()
        {
            IEnumerable<SingerDTO> a = await artistService.GetAllArtists();
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
        }
        public async Task putUsers()
        {
            IEnumerable<UserDTO> s = await userService.GetUsers(HttpContext.Session.GetString("login"));
            ViewBag.Users = s;
        }
        public async Task<IActionResult> Users()
        {
            IEnumerable<UserDTO> s = await userService.GetUsers(HttpContext.Session.GetString("login"));
            return PartialView(s);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(int id, int level)//[Bind("Id,Name,email,Age,Level")]  UserDTO u)  //UserDTO u)
        {

            try
            {
                UserDTO u = await userService.GetUser(id);
                if (u != null)
                {
                    await userService.UpdateUser(id, level);
                    IEnumerable<UserDTO> s1 = await userService.GetUsers(HttpContext.Session.GetString("login"));
                    return Json(true);
                }
                else
                    return Json(false);
            }
            catch
            {
                return Json(false);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                UserDTO u = await userService.GetUser(id);
                if (u != null)
                {
                    return PartialView("ChangeUser", u);
                }
                else
                    return Json(false);
            }
            catch
            {
                return Json(false);
            }
        }


        public async Task<IActionResult> DeleteSong(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MusicDTO s = await songService.GetSong(id);
            if (s == null)
            {
                return NotFound();
            }

            return PartialView(s);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSongConfirmd(int id)
        {
            try
            {
                await songService.DeleteSong(id);

                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }
        [HttpPost, ActionName("CancelDeleteSong")]
        [ValidateAntiForgeryToken]
        public IActionResult CancelDeleteSong()
        {
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> EditSong(int id)
        {
            try
            {
                MusicDTO s = await songService.GetSong(id);
                AddSong s1 = new();
                s1.SongId = id;
                s1.Video_Name = s.Video_Name;
                s1.VideoDate = s.VideoDate;
                s1.Year = s.Year;
                s1.Album = s.Album;
                int i = await artistService.GetArtistId(s);
                int i1 = await styleService.GetStyleId(s);
                s1.SingerId = i;
                s1.MusicStyleId = i1;
                s1.Video_URL = s.Video_URL;
                await putStyles();
                await putArtists();
                return PartialView("EditSong", s1);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditSong(AddSong s, IFormFile? p)
        {
            try
            {
                DateTime today = DateTime.Today;
                int currentYear = today.Year;
                try
                {
                    if (Convert.ToInt32(s.Year) < 0 || Convert.ToInt32(s.Year) > currentYear)
                        ModelState.AddModelError("", "uncorrectly year");
                }
                catch { ModelState.AddModelError("", "uncorrectly year"); }
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (p != null)
                        {
                            // Путь к папке Files
                            string path = "/MusicFiles/" + p.FileName; // имя файла

                            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                            {
                                await p.CopyToAsync(fileStream); // копируем файл в поток
                            }
                            s.Video_URL = path;
                        }
                        MusicDTO song = new(); //await songService.GetSong(s.SongId.Value);
                        song.Id = s.SongId.Value;
                        song.Video_Name = s.Video_Name;
                        song.VideoDate = s.VideoDate;
                        song.Year = s.Year;
                        song.Album = s.Album;
                        SingerDTO a = await artistService.GetArtist(s.SingerId);
                        song.singer = a.SingerName;
                        song.singerId = a.Id;

                        MusicStyleDTO st = await styleService.GetStyle(s.MusicStyleId);
                        song.music_style = st.StyleName;
                        song.music_styleId = st.Id;
                        song.Video_URL = s.Video_URL;
                        await songService.UpdateSong(song);
                        return Json(true);
                    }
                    catch
                    {
                        return Json(false);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            catch
            {
                return Json(false);
            }
        }
    }
}

