using LAB7_WEB.ContextModels;
using LAB7_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LAB7_WEB.Controllers;

public class StireController : Controller
{
    private readonly StiriContext _context;
    public List<StireModel>? Stiri {  get; set; }
    public StireModel? StireCurenta { get; set; }
    public StireController(StiriContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
        Stiri = _context.Stiri.Include(stire => stire.Categorie).ToList();
        if(Stiri == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(Stiri);
    }

    [HttpGet]
    public IActionResult Stire(int stireId)
    {
        /*
        StireCurenta = _context.Stiri.Where(stire => stire.Id == stireId).Include(stire => stire.Categorie).Include(comentariu => stire.Comentarii).FirstOrDefault();
        if(StireCurenta == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(StireCurenta);
        */

        StireCurenta  = _context.Stiri.Where(stire => stire.Id == stireId).Include(stire => stire.Categorie).FirstOrDefault();

        if (StireCurenta == null)
        {
            return RedirectToAction("Error", "Home");
        }

        List<ComentariuModel> comentarii = _context.Comentarii
            .Where(comentariu => comentariu.StireId == stireId)
            .ToList();

        ViewBag.Comentarii = comentarii;

        return View(StireCurenta);
    }

    [HttpGet]
    public IActionResult AdaugaStire()
    {
        List<SelectListItem> categorii = _context.Categorii
            .Select(categorie => new SelectListItem { Text = categorie.Nume, Value = categorie.Id.ToString() }).ToList();


        
        ViewBag.Categorii = categorii;
        return View();
    }

    [HttpPost]
    public IActionResult AdaugaStire(StireModel stireNoua)
    {
        if(!ModelState.IsValid) 
          {
            List<SelectListItem> categorii = _context.Categorii
           .Select(categorie => new SelectListItem { Text = categorie.Nume, Value = categorie.Id.ToString() }).ToList();
            ViewBag.Categorii = categorii;
            return View(stireNoua);
          }

        
        stireNoua.Categorie = _context.Categorii.Where(categorie => categorie.Id == stireNoua.CategorieId).FirstOrDefault();
        _context.Add(stireNoua);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificaStire(int stireId)
    {
        List<SelectListItem> categorii = _context.Categorii
         .Select(categorie => new SelectListItem { Text = categorie.Nume, Value = categorie.Id.ToString() }).ToList();
        ViewBag.Categorii = categorii;

        StireModel? stire = _context.Stiri
            .Where(stire => stire.Id == stireId).Include(stire => stire.Categorie).FirstOrDefault();
        if (stire == null)
        {
            return RedirectToAction("Error", "Home");
        }
        return View(stire);
    }

    [HttpPost]
    public IActionResult ModificaStire(StireModel stire)
    {
        if (!ModelState.IsValid)
        {
            List<SelectListItem> categorii = _context.Categorii
           .Select(categorie => new SelectListItem { Text = categorie.Nume, Value = categorie.Id.ToString() }).ToList();
            ViewBag.Categorii = categorii;
            return View(stire);
        }
        stire.Categorie = _context.Categorii.Where(categorie => categorie.Id == stire.CategorieId).FirstOrDefault();
        _context.Update(stire);
        _context.SaveChanges();
        return View("Stire",stire);
    }

    [HttpGet]
    public IActionResult StergeStire(int stireId)
    {
        StireModel? stire = _context.Stiri
            .Where(stire => stire.Id == stireId).Include(stire => stire.Categorie).FirstOrDefault();
        if (stire == null)
        {
            return RedirectToAction("Error", "Home");
        }
        _context.Remove(stire);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult AdaugaComentariu(int stireId, string autor, string continut)
    {
        var comentariu = new ComentariuModel
        {
            StireId = stireId,
            Autor = autor,
            Continut = continut,
            Data = DateTime.Now
        };

        _context.Comentarii.Add(comentariu);
        _context.SaveChanges();

        return RedirectToAction("Stire", new { stireId = stireId });
    }

}
