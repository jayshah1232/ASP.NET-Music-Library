using Assignment3_WEB524.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3_WEB524.Controllers
{
    public class AlbumController : Controller
    {
        private Manager m = new Manager();
        // GET: Album
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            var query = m.AlbumGetById(id.GetValueOrDefault());

            if (query == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(query);
            }
        }

        [Route("Artist/{id}/AddTrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());

            if (album == null)
            {
                return HttpNotFound();
            }
            else
            {
                var newTrack = new TrackAddFormViewModel();
                newTrack.Id = album.Id;
                newTrack.AlbumName = album.Name;
                newTrack.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");

                return View(newTrack);
            }
        }

        [Route("Artist/{id}/AddTrack")]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel newTrack)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();
                return View(newTrack);
            }
            else
            {
                var track = m.TrackAdd(newTrack);
                if (track == null)
                {
                    return View(newTrack);
                }
                else
                {
                    return RedirectToAction("details", "track", new { id = track.Id });
                }
            }
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Album/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
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

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
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
