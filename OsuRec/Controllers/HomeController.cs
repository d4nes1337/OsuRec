using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OsuRec.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace OsuRec.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string username)
        {
            return RedirectToAction("ListOfRelatedBeatmaps", "RelatedBeatmap", new { _username = username});
        }

    }
}
