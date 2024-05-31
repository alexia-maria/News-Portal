using LAB7_WEB.ContextModels;
using LAB7_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LAB7_WEB.Controllers
{
    public class CategorieController : Controller
    {
        private readonly StiriContext _context;

        public List<CategorieModel> Categorii { get; set; }

        public CategorieController(StiriContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            Categorii = _context.Categorii.ToList();
            if (Categorii == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(Categorii);
        }

        [HttpGet]
        public IActionResult AdaugaCategorie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdaugaCategorie(CategorieModel categorieNoua)
        {
            if (!ModelState.IsValid)
            {
                return View(categorieNoua);
            }

            _context.Add(categorieNoua);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ModificaCategorie(int categorieId)
        {
            CategorieModel? categorie = _context.Categorii
                .Where(categorie => categorie.Id == categorieId).FirstOrDefault();
            if (categorie == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(categorie);
        }

        [HttpPost]
        public IActionResult ModificaCategorie(CategorieModel categorie)
        {
            if (!ModelState.IsValid)
            {
                return View(categorie);
            }

            _context.Update(categorie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> StiriInCategorie(int categorieId, string sortare)
        {
            List<StireModel> stiri = await _context.Stiri
                .Where(stire => stire.CategorieId == categorieId)
                .ToListAsync();

            CategorieModel? categorie = await _context.Categorii.FindAsync(categorieId);

            if (categorie == null)
            {
                return RedirectToAction("Error", "Home");
            }

            switch (sortare)
            {
                case "data":
                    stiri = stiri.OrderBy(s => s.Data).ToList();
                    break;
                case "titlu":
                    stiri = stiri.OrderBy(s => s.Titlu).ToList();
                    break;
                case "continut":
                    stiri = stiri.OrderByDescending(s => s.Continut.Length).ToList();
                    break;
                default:
                    break;
            }

            ViewBag.CategorieNume = categorie.Nume;
            ViewBag.Sortare = sortare;
            ViewBag.CategorieId = categorieId;
            return View(stiri);
        }

    }
}
