using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using crud_registration_web_system.Models;

namespace crud_registration_web_system.Controllers
{
    public class PowerPlantController : Controller
    {
        private readonly MvcPowerPlantContext _context;

        public PowerPlantController(MvcPowerPlantContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string supplierName, string searchString, string activationStatus)
        {
            IQueryable<string> supplierQuery = from p in _context.PowerPlant
                                        orderby p.Supplier
                                        select p.Supplier;

            IQueryable<string> activationQuery = from p in _context.PowerPlant
                                        orderby p.Active
                                        select p.Active.ToString();


            var powerPlants = from p in _context.PowerPlant
                            select p;

            if(!string.IsNullOrEmpty(searchString))
            {
                powerPlants = powerPlants.Where(s => s.ConsumerUnit.Contains(searchString));
            }

            if(!string.IsNullOrEmpty(supplierName))
            {
                powerPlants = powerPlants.Where(s => s.Supplier == supplierName);
            }

            if(!string.IsNullOrEmpty(activationStatus))
            {
                bool activationBool = activationStatus.Equals("True", StringComparison.CurrentCultureIgnoreCase) ? true : false;
                powerPlants = powerPlants.Where(s => s.Active == activationBool);
            }

            var powerPlantSupplierVM = new PowerPlantSupplier
            {
                Suppliers = new SelectList(await supplierQuery.Distinct().ToListAsync()),
                ActivationStatuses = new SelectList(await activationQuery.Distinct().ToListAsync()),
                PowerPlants = await powerPlants.ToListAsync()
            };

            return View(powerPlantSupplierVM);
        }


        // GET: PowerPlant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerPlant = await _context.PowerPlant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (powerPlant == null)
            {
                return NotFound();
            }

            return View(powerPlant);
        }

        // GET: PowerPlant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PowerPlant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConsumerUnit,Supplier,Active")] PowerPlant powerPlant)
        {
            if (ModelState.IsValid)
            {
                if(_context.PowerPlant.Any(plant => plant.Supplier == powerPlant.Supplier && plant.ConsumerUnit == powerPlant.ConsumerUnit))
                {
                    return ValidationProblem();
                }

                _context.Add(powerPlant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(powerPlant);
        }

        // GET: PowerPlant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerPlant = await _context.PowerPlant.FindAsync(id);
            if (powerPlant == null)
            {
                return NotFound();
            }
            return View(powerPlant);
        }

        // POST: PowerPlant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConsumerUnit,Supplier,Active")] PowerPlant powerPlant)
        {
            if (id != powerPlant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                if(_context.PowerPlant.Any(plant => plant.Supplier == powerPlant.Supplier && plant.ConsumerUnit == powerPlant.ConsumerUnit && plant.Id!= powerPlant.Id))
                {
                    return ValidationProblem();
                }

                    _context.Update(powerPlant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PowerPlantExists(powerPlant.Id))
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
            return View(powerPlant);
        }

        // GET: PowerPlant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var powerPlant = await _context.PowerPlant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (powerPlant == null)
            {
                return NotFound();
            }

            return View(powerPlant);
        }

        // POST: PowerPlant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var powerPlant = await _context.PowerPlant.FindAsync(id);
            _context.PowerPlant.Remove(powerPlant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PowerPlantExists(int id)
        {
            return _context.PowerPlant.Any(e => e.Id == id);
        }
    }
}
