using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using System.Diagnostics;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class MusicController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMusicService musicService;
        private readonly ISingerService singerService;
        private readonly IMusicStyleService styleService;
        private readonly IUserService userService;

        public MusicController(IMusicService s,ISingerService a, IMusicStyleService ms, IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            musicService = s;
            singerService = a;
            styleService = ms;
            _appEnvironment = webHostEnvironment;
            this.userService = userService;

        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MusicDTO> singers = await musicService.GetAllSongs();
            ViewBag.Musics = singers;//Task.Run(()) => 
            return View("Index");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Music");
        }

        public async Task<IActionResult> Create()
        {
            var styles = await styleService.GetAllStyles();
            var singers = await singerService.GetAllArtists();

            ViewBag.Style_List = new SelectList(styles, "Id", "StyleName");
            ViewBag.Singer_List = new SelectList(singers, "Id", "SingerName");
            return PartialView("Create");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Video_Name,Album,Year,Video_URL,VideoDate,MusicStyleId,SingerId,UserId")] AddMusic music, IFormFile Video_URL)
        {

            var user_login = HttpContext.Session.GetString("Login");

            if (HttpContext.Session.GetString("Login") == null)
                return PartialView("~/Views/User/Login.cshtml");

            MusicDTO musicDTO = new();
            var us = await userService.GetUser(user_login);
            musicDTO.userId = us.Id;

           // var style = await styleService.GetStyle(music.MusicStyleId);
            musicDTO.music_styleId = music.MusicStyleId;

            //var singer = await singerService.GetArtist(music.SingerId);
            musicDTO.singerId = music.SingerId;

            musicDTO.Video_Name = music.Video_Name;
            musicDTO.Year = music.Year;
            musicDTO.Singer = music.SingerName;
            musicDTO.User = us.Login;

            musicDTO.Album = music.Album;
            musicDTO.VideoDate = DateTime.Now;
            try
            {
                if (Video_URL != null)
                {
                    string file_path = "/Music/" + Video_URL.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + file_path, FileMode.Create))
                    {
                        await Video_URL.CopyToAsync(fileStream); // копируем файл в поток
                    }
                    musicDTO.Video_URL = "~" + file_path;
                    await musicService.AddSong(musicDTO);
                    
                    return PartialView("~/Views/Music/Success.cshtml");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MusicExists(music.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return PartialView("Create");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await musicService.GetAllSongs() == null)
            {
                return NotFound();
            }

            var music = await musicService.GetSong((int)id);
            if (music == null)
            {
                return NotFound();
            }
            AddMusic addMusic = MusicDTO_To_AddMusic(music);
            var styles = await styleService.GetAllStyles();
            var singers = await singerService.GetAllArtists();

            ViewBag.Style_List = new SelectList(styles, "Id", "StyleName");
            ViewBag.Singer_List = new SelectList(singers, "Id", "SingerName");
            return PartialView("Edit", addMusic);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Video_Name,Album,Year,Video_URL,VideoDate,MusicStyleId,SingerId,UserId")] AddMusic music, IFormFile Video_URL)
        {
            if (id != music.Id)
                return NotFound();

            var user_login = HttpContext.Session.GetString("Login");

            if (HttpContext.Session.GetString("Login") == null)
                return PartialView("~/Views/User/Login.cshtml");

            MusicDTO musicDTO = new MusicDTO();
            var us = await userService.GetUser(user_login);
            musicDTO.userId = us.Id;

           // var style = await styleService.GetStyle(music.MusicStyleId);
            musicDTO.music_styleId = music.MusicStyleId;

            //var singer = await singerService.GetArtist(music.SingerId);
            musicDTO.singerId = music.SingerId;

            musicDTO.userId = us.Id;
            musicDTO.VideoDate = DateTime.Now;
            try
            {
                if (Video_URL != null)
                {
                    string file_path = "/Music/" + Video_URL.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + file_path, FileMode.Create))
                    {
                        await Video_URL.CopyToAsync(fileStream); // копируем файл в поток
                    }
                    musicDTO.Video_URL = "~" + file_path;

                    musicService.UpdateSong(musicDTO);
                    
                    return PartialView("~/Views/Music/Success.cshtml");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MusicExists(music.Id))
                {
                    return NotFound();
                }
                else throw;
            }
            return PartialView("Edit");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await musicService.GetAllSongs() == null)
            {
                return NotFound();
            }

            var music = await musicService.GetSong((int)id);
            if (music == null)
            {
                return NotFound();
            }
            AddMusic addMusic= MusicDTO_To_AddMusic(music);

            return PartialView(addMusic);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await musicService.GetAllSongs() == null)
            {
                return Problem("Entity set 'Music_PortalContext.Musics'  is null.");
            }

            var music = await musicService.GetSong(id);
            if (music != null)
            {
                await musicService.DeleteSong(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MusicExists(int id)
        {
            IEnumerable<MusicDTO> list = await musicService.GetAllSongs();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private AddMusic MusicDTO_To_AddMusic(MusicDTO music)
        {
            AddMusic addMusic = new AddMusic();
            addMusic.Id = music.Id;
            addMusic.Video_Name = music.Video_Name;
            addMusic.Album = music.Album;
            addMusic.VideoDate = music.VideoDate;
            addMusic.Year = music.Year;
            addMusic.UserLogin = music.User;
            return addMusic;
        }
    }
}