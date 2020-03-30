using Microsoft.AspNetCore.Mvc;
using OsuRec.Data.Interfaces;
using OsuRec.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OsuRec.Controllers
{
    public class RelatedBeatmapController : Controller
    {
        private readonly RelationProcess _relationProcess = new RelationProcess();


        public ViewResult ListOfRelatedBeatmaps()
        {
            _relationProcess.ShowStats("D4NES1337");
            ViewBag.Mod = $"Main Mod: {_relationProcess.playStyle.MainMod}".ToString();
            var relatedMapsByMainMod = _relationProcess.RelatedBeatmapsByMainMod;
            return View(relatedMapsByMainMod);
        }
    }
}
