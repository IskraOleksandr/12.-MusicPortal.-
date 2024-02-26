using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class SingerController : Controller
    {
        private readonly ISingerService singerService;

        public SingerController(ISingerService s)
        {
            singerService = s;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<SingerDTO> singers = await Task.Run(() => singerService.GetAllArtists());
            ViewBag.Singers = singers;
            return View();
        }


        public IActionResult Create()
        {
            return PartialView("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SingerName")] SingerDTO singer)//SingerDTO
        {
            if (ModelState.IsValid)
            {
                await singerService.AddArtist(singer);
                return PartialView("~/Views/Music/Success.cshtml");
            }
            return PartialView("Create");
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await singerService.GetAllArtists() == null)
            {
                return NotFound();
            }

            var singer = await singerService.GetArtist((int)id);
            if (singer == null)
            {
                return NotFound();
            }
            AddSinger addSinger = SingerDTO_To_AddSinger(singer);
            return PartialView("Edit", addSinger);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SingerName")] SingerDTO singer)
        {
            if (id != singer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    singerService.UpdateSinger(singer);
                }
                catch { return View("Edit", singer); }
                return PartialView("~/Views/Music/Success.cshtml");
            }
            return PartialView("Edit", singer);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await singerService.GetAllArtists() == null)
            {
                return NotFound();
            }

            var singer = await singerService.GetArtist((int)id);
            if (singer == null)
            {
                return NotFound();
            }
            AddSinger addSinger = SingerDTO_To_AddSinger(singer);

            return PartialView("Delete", addSinger);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await singerService.GetAllArtists() == null)
            {
                return Problem("Entity set 'Music_PortalContext.Singers'  is null.");
            }

            var singer = await singerService.GetArtist(id);
            if (singer != null)
            {
                await singerService.DeleteArtist(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SingerExists(int id)
        {
            IEnumerable<SingerDTO> list = await singerService.GetAllArtists();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private AddSinger SingerDTO_To_AddSinger(SingerDTO singer)
        {
            AddSinger addSinger = new AddSinger();
            addSinger.Id = singer.Id;
            addSinger.SingerName = singer.SingerName;
            return addSinger;
        }
    }
}
