using LAB7_WEB.ContextModels;
using LAB7_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LAB7_WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StiriContext _context;
        public List<StireModel>? Stiri { get; set; }

        public HomeController(ILogger<HomeController> logger, StiriContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<StireModel> stiriRecente = _context.Stiri
                .OrderByDescending(stire => stire.Data)
                .Include(stire => stire.Categorie)
                .Take(3) 
                .ToList();

            return View(stiriRecente);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
