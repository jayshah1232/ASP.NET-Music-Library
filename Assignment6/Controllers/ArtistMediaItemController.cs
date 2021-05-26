using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3_WEB524.Controllers
{
    public class ArtistMediaItemController : Controller
    {
        Manager m = new Manager();

        public ActionResult Index()
        {
            return View("index", "home");
        }

        [Route("ArtistMedia/{id}")]
        public ActionResult Details(int? id)
        {
            var artistMediaItem = m.ArtistMediaItemGetById(id.GetValueOrDefault());

            if (artistMediaItem == null)
            {
                return HttpNotFound();
            }
            else
            {
                return File(artistMediaItem.Content, artistMediaItem.ContentType);
            }
        }
    }
}