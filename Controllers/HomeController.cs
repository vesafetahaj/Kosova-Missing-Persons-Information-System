
using KosovaMap2.Data;
using KosovaMap2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JobApplicationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KosovaMapContext _context;


        public HomeController(ILogger<HomeController> logger, KosovaMapContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Retrieve all TeDhena records from the database
            var allData = _context.TeDhenas
                                   .Include(t => t.VendlindjaNavigation)
                                   .Include(t => t.VendiZhdukjesNavigation)
                                   .ToList();

            // Filter the data to include only entries where Vendlindja is "Peja"
            var pejaData = allData.Where(data => data.VendlindjaNavigation.QytetiEmri == "Peja").ToList();

            // Pass the filtered data to the view
            return View(pejaData);
        }

        public IActionResult Register()
        {
            var cities = _context.Qytetis.ToList();

            ViewData["Cities"] = cities;
            return View();
        }
        public IActionResult Data(string city)
        {
            // Filter data based on the city parameter
            List<TeDhena> filteredData;

            if (!string.IsNullOrEmpty(city))
            {
                filteredData = _context.TeDhenas
                                        .Include(t => t.VendlindjaNavigation)
                                        .Include(t => t.VendiZhdukjesNavigation)
                                        .Where(d => d.VendiZhdukjesNavigation.QytetiEmri == city)
                                        .ToList();
            }
            else
            {
                // If no city is specified, return all data
                filteredData = _context.TeDhenas
                                        .Include(t => t.VendlindjaNavigation)
                                        .Include(t => t.VendiZhdukjesNavigation)
                                        .ToList();
            }

            return View(filteredData);
        }


        // POST: /Home/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Id, EmriMbiemri, DataLindjes, Vendlindja, VendiZhdukjes, DataZhdukjes, Komente")] TeDhena teDhena)
        {
            if (ModelState.IsValid)
            {
                // Ensure DataZhdukjes is not later than DataLindjes
                if (teDhena.DataZhdukjes != null && teDhena.DataLindjes != null && DateTime.Parse(teDhena.DataZhdukjes) < DateTime.Parse(teDhena.DataLindjes))
                {
                    ModelState.AddModelError("DataZhdukjes", "Data e Zhdukjes nuk mund të jetë më heret se Data e Lindjes.");
                    ViewData["Cities"] = _context.Qytetis.ToList();
                    return View(teDhena);
                }

                _context.TeDhenas.Add(teDhena);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = _context.Qytetis.ToList();
            return View(teDhena);
        }
        private bool TeDhenaExists(int id)
        {
            return _context.TeDhenas.Any(e => e.Id == id);
        }

        public IActionResult Update(int id)
        {
            var teDhena = _context.TeDhenas.Find(id);
            if (teDhena == null)
            {
                return NotFound();
            }

            ViewData["Cities"] = _context.Qytetis.ToList();
            return View(teDhena);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, [Bind("Id, EmriMbiemri, DataLindjes, Vendlindja, VendiZhdukjes, DataZhdukjes, Komente")] TeDhena teDhena)
        {
            if (id != teDhena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (teDhena.DataZhdukjes != null && teDhena.DataLindjes != null && DateTime.Parse(teDhena.DataZhdukjes) < DateTime.Parse(teDhena.DataLindjes))
                {
                    ModelState.AddModelError("DataZhdukjes", "Data e Zhdukjes nuk mund të jetë më heret se Data e Lindjes.");
                    ViewData["Cities"] = _context.Qytetis.ToList();
                    return View(teDhena);
                }

                try
                {
                    _context.Update(teDhena);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeDhenaExists(teDhena.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cities"] = _context.Qytetis.ToList();
            return View(teDhena);
        }
        public IActionResult Delete(int id)
        {
            var teDhena = _context.TeDhenas
                                  .Include(t => t.VendlindjaNavigation)
                                  .Include(t => t.VendiZhdukjesNavigation)
                                  .FirstOrDefault(t => t.Id == id);
            if (teDhena == null)
            {
                return NotFound();
            }

            return View(teDhena);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var teDhena = _context.TeDhenas.FirstOrDefault(t => t.Id == id);
            if (teDhena == null)
            {
                return NotFound();
            }

            _context.TeDhenas.Remove(teDhena);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}