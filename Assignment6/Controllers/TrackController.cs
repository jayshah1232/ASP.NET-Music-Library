using Assignment3_WEB524.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3_WEB524.Controllers
{
    public class TrackController : Controller
    {
        private Manager m = new Manager();

        public ActionResult Index()
        {
            var query = m.TrackGetAll();
            return View(query);
        }

        public ActionResult Details(int? id)
        {
            var obj = m.TrackGetById(id.GetValueOrDefault());
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(obj);
            }
        }

        public ActionResult Edit(int? id)
        {
            var track = m.TrackGetById(id.GetValueOrDefault());

            if (track == null)
            {
                return HttpNotFound();
            }
            else
            {
                var obj = new TrackEditFormViewModel();
                obj.Name = track.Name;

                return View(obj);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int? id, TrackEditViewModel track)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                       .Where(x => x.Value.Errors.Count > 0)
                       .Select(x => new { x.Key, x.Value.Errors })
                       .ToArray();
                return RedirectToAction("Edit", new { id = track.Id });
            }
            else
            {
                if (id.GetValueOrDefault() != track.Id)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var editTrack = m.TrackEdit(track);

                    if (editTrack == null)
                    {
                        return RedirectToAction("Edit", new { id = track.Id });
                    }
                    else
                    {
                        return RedirectToAction("Details", new { id = track.Id });
                    }
                }
            }
        }

        public ActionResult Delete(int? id)
        {
            var track = m.TrackGetById(id.GetValueOrDefault());

            if (track == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(track);
            }
        }

        [HttpPost]
        public ActionResult Delete(int? id, HttpPostedFileBase audioFile)
        {
            var trackDeleted = m.TrackDelete(id.GetValueOrDefault(), audioFile);
            return RedirectToAction("index");
        }
    }
}