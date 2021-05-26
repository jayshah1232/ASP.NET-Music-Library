using Assignment3_WEB524.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3_WEB524.Controllers
{
    public class ArtistController : Controller
    {
        private Manager m = new Manager();
        // GET: Artist
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            var obj = m.ArtistGetById(id.GetValueOrDefault());
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(obj);
            }
        }

        // GET: Artist/Create
        public ActionResult Create()
        {
            var artist = new ArtistAddFormViewModel();
            artist.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");
            //artist.Executive = User.Identity.Name;
            return View(artist);
        }

        // POST: Artist/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ArtistAddViewModel artist)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
                return View(artist);
            }
            try
            {
                // TODO: Add insert logic here
                var addedArtist = m.ArtistAdd(artist);

                if (addedArtist == null)
                {
                    return View(artist);
                }
                else
                {
                    return RedirectToAction("Details", new { id = addedArtist.Id });
                }
            }
            catch
            {
                return View(artist);
            }
        }

        [Route("artist/{id}/addalbum")]
        public ActionResult AddAlbum(int? id)
        {
            var album = m.ArtistGetById(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                var newAlbum = new AlbumAddFormViewModel();
                newAlbum.Id = album.Id;
                newAlbum.Name = album.Name;

                newAlbum.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

                return View(newAlbum);
            }
        }

        [Route("artist/{id}/addalbum")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddAlbum(AlbumAddViewModel newAlbum)
        {
            if (!ModelState.IsValid)
            {
                return View(newAlbum);
            }

            var addedAlbum = m.AlbumAdd(newAlbum);

            if (addedAlbum == null)
            {
                return View(newAlbum);
            }
            else
            {
                return RedirectToAction("details", "album", new { id = addedAlbum.Id });
            }
        }

        [Route("Artist/{id}/AddArtistMediaItem")]
        public ActionResult AddArtistMediaItem(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());

            if (artist == null)
            {
                return HttpNotFound();
            }
            else
            {
                var artistMediaItem = new ArtistMediaItemAddFormViewModel();

                artistMediaItem.ArtistId = artist.Id;

                return View(artistMediaItem);
            }
        }

        [HttpPost]
        [Route("Artist/{id}/AddArtistMediaItem")]
        public ActionResult AddMediaItem(ArtistMediaItemAddViewModel newItem)
        {
            var artist = m.ArtistGetById(newItem.ArtistId);

            if (!ModelState.IsValid && artist != null)
            {
                var o = new ArtistMediaItemAddFormViewModel();

                o.ArtistId = artist.Id;

                return View(o);
            }

            var artistMediaItem = m.AddArtistMediaItem(newItem);
            if (artistMediaItem == null)
            {
                var obj = new ArtistMediaItemAddFormViewModel();
                obj.ArtistId = artist.Id;

                return View(obj);
            }
            else
            {
                return RedirectToAction("details", "Artist", new { id = artistMediaItem.Id });
            }
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
