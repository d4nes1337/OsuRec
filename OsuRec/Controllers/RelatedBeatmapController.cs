using Microsoft.AspNetCore.Mvc;
using OsuRec.Data.Interfaces;
using OsuRec.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OsuRec.ViewModels;
using OsuRec.Data.mock;

namespace OsuRec.Controllers
{
    public class RelatedBeatmapController : Controller
    {
        private readonly List<MockRelatedBeatmap> relatedBeatmaps;


        public ViewResult ListOfRelatedBeatmaps()
        {
            ViewBag.Title = "Main mod related beatmaps";
            RelatedBeatmapsListViewModel relatedViewModel = new RelatedBeatmapsListViewModel();
            //relatedViewModel.RelatedBeatmaps = relatedBeatmaps;
            return View(relatedViewModel);
        }
    }
}
