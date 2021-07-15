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

        public SelectList GetSuppliers()
        {
           List<SelectListItem> suppliers = _context.Supplier.AsNoTracking()
               .OrderBy(s => s.Name)
               .Select(s =>
                   new SelectListItem
                   {
                      Value = s.Id.ToString(),
                      Text = s.Name
                   }).ToList();
           return new SelectList(suppliers, "Text", "Text");
        }

        public async Task<IActionResult> Index(string supplier, string consumerUnit, int? active)
        {

            IQueryable<string> supplierQuery = from p in _context.PowerPlant
                                        orderby p.Supplier
                                        select p.Supplier;

            var powerPlants = from p in _context.PowerPlant
                            select p;

            if(!string.IsNullOrEmpty(consumerUnit))
                powerPlants = powerPlants.Where(s => s.ConsumerUnit.Contains(consumerUnit));

            if(!string.IsNullOrEmpty(supplier))
                powerPlants = powerPlants.Where(p => p.Supplier == supplier);

            if(!(active is null))
            {
                powerPlants = powerPlants.Where(p => p.Active == (active != 0));
            }

            var powerPlantSupplierVM = new PowerPlantSupplier
            {
                Suppliers = GetSuppliers(),
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
            var powerPlantSupplierVM = new PowerPlantSupplier
            {
                Suppliers = GetSuppliers()
            };

            return View(powerPlantSupplierVM);
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
                if(_context.PowerPlant.Any(plant => plant.Supplier == powerPlant.Supplier && 
                                                    plant.ConsumerUnit == powerPlant.ConsumerUnit))
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

            var powerPlantSupplierVM = new PowerPlantSupplier
            {
                Suppliers = GetSuppliers(),//new SelectList(await supplierQuery.Distinct().ToListAsync()),
                Supplier = powerPlant.Supplier,
                ConsumerUnit = powerPlant.ConsumerUnit,
                Active = powerPlant.Active,
                PowerPlants = (new List<PowerPlant>())
            };
            powerPlantSupplierVM.PowerPlants.Add(powerPlant);

            return View(powerPlantSupplierVM);
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

                if(_context.PowerPlant.Any(plant => plant.Supplier == powerPlant.Supplier && 
                                                    plant.ConsumerUnit == powerPlant.ConsumerUnit &&
                                                    plant.Id!= powerPlant.Id))
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
            var powerPlantSupplierVM = new PowerPlantSupplier
            {
                Suppliers = GetSuppliers(),//new SelectList(await supplierQuery.Distinct().ToListAsync()),
                Supplier = powerPlant.Supplier,
                ConsumerUnit = powerPlant.ConsumerUnit,
                Active = powerPlant.Active,
                PowerPlants = (new List<PowerPlant>())
            };
            powerPlantSupplierVM.PowerPlants.Add(powerPlant);

            return View(powerPlantSupplierVM);

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
