using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.Models;

namespace MusicPortal.Controllers
{
    public class MusicStyleController : Controller
    {
        private readonly IMusicStyleService styleService;

        public MusicStyleController(IMusicStyleService s)
        {
            styleService = s;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<MusicStyleDTO> styles = await Task.Run(() => styleService.GetAllStyles());
            ViewBag.MusicStyles = styles;
            return View();

        }

        public IActionResult Create()
        {
            return PartialView("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StyleName")] AddMusicStyle style)
        {
            if (ModelState.IsValid)
            {
                MusicStyleDTO musicStyleDTO = new MusicStyleDTO();
                musicStyleDTO.StyleName = style.StyleName;
                await styleService.AddStyle(musicStyleDTO);

                return PartialView("~/Views/Music/Success.cshtml");
            }
            return PartialView("Create");
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await styleService.GetAllStyles() == null)
            {
                return NotFound();
            }

            var style = await styleService.GetStyle((int)id);
            if (style == null)
            {
                return NotFound();
            }

            AddMusicStyle addmusicStyle = MusicStyleDTO_To_AddMusicStyle(style);
            return PartialView("Edit", addmusicStyle);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StyleName")] AddMusicStyle style)
        {
            if (id != style.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    MusicStyleDTO musicStyleDTO = new MusicStyleDTO();
                    musicStyleDTO.Id = style.Id;
                    musicStyleDTO.StyleName = style.StyleName;

                    styleService.UpdateMusicStyle(musicStyleDTO);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MusicStyleExists(style.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return PartialView("~/Views/Music/Success.cshtml");
            }
            return PartialView("Edit", style);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await styleService.GetAllStyles() == null)
            {
                return NotFound();
            }
            var style = await styleService.GetStyle((int)id);
            if (style == null)
            {
                return NotFound();
            }

            AddMusicStyle addmusicStyle = MusicStyleDTO_To_AddMusicStyle(style);
            return PartialView("Delete", addmusicStyle);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await styleService.GetAllStyles() == null)
            {
                return Problem("Entity set 'Music_PortalContext.MusicStyles'  is null.");
            }

            var style = await styleService.GetStyle(id);
            if (style != null)
            {
                await styleService.DeleteStyle(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MusicStyleExists(int id)
        {
            IEnumerable<MusicStyleDTO> list = await styleService.GetAllStyles();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private AddMusicStyle MusicStyleDTO_To_AddMusicStyle(MusicStyleDTO musicStyle)
        {
            AddMusicStyle addMusicStyle = new AddMusicStyle();
            addMusicStyle.Id = musicStyle.Id;
            addMusicStyle.StyleName = musicStyle.StyleName;
            return addMusicStyle;
        }
    }
}
