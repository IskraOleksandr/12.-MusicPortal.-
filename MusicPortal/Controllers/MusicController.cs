using Microsoft.AspNetCore.Mvc;
using MusikPortal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Services;
using MusicPortal.Filters;


namespace MusikPortal.Controllers
{
    [Culture]
    public class MusicController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private readonly IMusicService songService;
        private readonly ISingerService artistService;
        private readonly IMusicStyleService styleService;
        public MusicController(IMusicService s, ISingerService a, IMusicStyleService st, IWebHostEnvironment appEnvironment)
        {
            songService = s;
            artistService = a;
            styleService = st;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await putStylesArtists();
            //return View("AddSong");
            return PartialView("AddSong");
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AddSong song, IFormFile file)
        {            
            if (file == null)
                ModelState.AddModelError("", "put the file");
            DateTime today = DateTime.Today;
            int currentYear = today.Year;
            try
            {
                if (Convert.ToInt32(song.Year) < 0 || Convert.ToInt32(song.Year) > currentYear)
                    ModelState.AddModelError("", "uncorrectly year");
            }
            catch { ModelState.AddModelError("", "uncorrectly year"); }
            if (file != null)
            {
                string str= file.FileName.Replace(" ", "_");
                string str1 = str.Replace("-", "_");
                // Путь к папке Files
                string path = "/MusicFiles/" + str1; // имя файла

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream); // копируем файл в поток
                }
                MusicDTO s = new();
                MusicStyleDTO sStyle = await styleService.GetStyle(song.MusicStyleId);
                SingerDTO aArtist = await artistService.GetArtist(song.SingerId);
                s.Video_Name = song.Video_Name;
                s.music_style =sStyle.StyleName;
                s.music_styleId = sStyle.Id;
                s.singer = aArtist.SingerName;
                s.singerId = aArtist.Id;
                s.VideoDate = song.VideoDate;
                s.Year = song.Year;
                s.Album=song.Album; 
                s.Video_URL = path;
                if (ModelState.IsValid)
                {
                    try
                    {
                        await songService.AddSong(s);                       
                        await songService.AddSongToArtist(song.SingerId, s);
                         return PartialView("Success");
                       // return Json("success");
                    }
                    catch
                    {
                        await putStylesArtists();
                        return PartialView("AddSong", song);
                    }
                }
                else
                {
                    await putStylesArtists();
                    return PartialView("AddSong", song);
                }
            }
            await putStylesArtists();
            return PartialView("AddSong", song);
        }
        public async Task putStylesArtists()
        {
            IEnumerable<MusicStyleDTO> s = await styleService.GetAllStyles();
            IEnumerable<SingerDTO> a = await artistService.GetAllArtists();
            ViewData["StyleId"] = new SelectList(s, "Id", "Name");
            ViewData["ArtistId"] = new SelectList(a, "Id", "Name");
        }
       
    }
}
