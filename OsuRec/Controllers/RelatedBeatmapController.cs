using Microsoft.AspNetCore.Mvc;
using OsuRec.Data.Interfaces;
using OsuRec.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OsuRec.ViewModels;
using OsuRec.Data;

namespace OsuRec.Controllers
{
    public class RelatedBeatmapController : Controller
    {
        private RelatedBeatmapsListViewModel relatedViewModel;

        [Route("ListOfRelatedBeatmapsByMainMod")]
        public ViewResult ListOfRelatedBeatmaps(string _username)
        {
            ViewBag.Title = "Main mod related beatmaps";
            relatedViewModel = new RelatedBeatmapsListViewModel(_username);
            return View(relatedViewModel);
        }
        //[Route("ListOfRelatedBeatmapsBySecondMod")]
        //public ActionResult ListOfRelatedBeatmapsBySecondMod()
        //{
        //    ViewBag.Title = "Second mod related beatmaps";
        //    return PartialView();
        //}
    }
}
