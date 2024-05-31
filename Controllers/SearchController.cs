using LAB7_WEB.ContextModels;
using LAB7_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB7_WEB.Controllers
{
    public class SearchController : Controller
    {
        private readonly StiriContext _context;

        public SearchController(StiriContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SearchPortal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchPortal(string? searchTerm, DateTime? dataInf, DateTime? dataSup)
        {
            if (string.IsNullOrEmpty(searchTerm) && !dataInf.HasValue && !dataSup.HasValue)
            {
                return View(new List<StireModel>());
            }

            IQueryable<StireModel> query = _context.Stiri;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(stire => stire.Titlu.ToLower().Contains(searchTerm.ToLower()));
            }

            if (dataInf.HasValue)
            {
                query = query.Where(stire => stire.Data >= dataInf);
            }

            if (dataSup.HasValue)
            {
                query = query.Where(stire => stire.Data <= dataSup);
            }

            List<StireModel> stiri = query.ToList();

            ViewBag.SearchTerm = searchTerm;
            ViewBag.dataInf = dataInf;
            ViewBag.dataSup = dataSup;

            return View(stiri);
        }
    }
}
