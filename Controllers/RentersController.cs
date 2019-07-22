using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyLeasesDB.Models;

namespace EasyLeasesDB.Controllers
{
    public class RentersController : Controller
    {
        private readonly EasyLeasesDBContext _context;

        public RentersController(EasyLeasesDBContext context)
        {
            _context = context;
        }

        // GET: Renters
        public async Task<IActionResult> Index()
        {
            var easyLeasesDBContext = _context.Renters.Include(r => r.FkProperties).Include(r => r.FkRealtors);
            return View(await easyLeasesDBContext.ToListAsync());
        }

        // GET: Renters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renters = await _context.Renters
                .Include(r => r.FkProperties)
                .Include(r => r.FkRealtors)
                .FirstOrDefaultAsync(m => m.RentersId == id);
            if (renters == null)
            {
                return NotFound();
            }

            return View(renters);
        }

        // GET: Renters/Create
        public IActionResult Create()
        {
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId");
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId");
            return View();
        }

        // POST: Renters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentersId,FkPropertiesId,FkRealtorsId,RenterName,RenterGender,RenterSsn,RenterMonthlySalary,RenterEmail,RenterPhoneNumber,RenterUserName,RenterPassword")] Renters renters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(renters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId", renters.FkPropertiesId);
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", renters.FkRealtorsId);
            return View(renters);
        }

        // GET: Renters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renters = await _context.Renters.FindAsync(id);
            if (renters == null)
            {
                return NotFound();
            }
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId", renters.FkPropertiesId);
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", renters.FkRealtorsId);
            return View(renters);
        }

        // POST: Renters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RentersId,FkPropertiesId,FkRealtorsId,RenterName,RenterGender,RenterSsn,RenterMonthlySalary,RenterEmail,RenterPhoneNumber,RenterUserName,RenterPassword")] Renters renters)
        {
            if (id != renters.RentersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(renters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentersExists(renters.RentersId))
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
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId", renters.FkPropertiesId);
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", renters.FkRealtorsId);
            return View(renters);
        }

        // GET: Renters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renters = await _context.Renters
                .Include(r => r.FkProperties)
                .Include(r => r.FkRealtors)
                .FirstOrDefaultAsync(m => m.RentersId == id);
            if (renters == null)
            {
                return NotFound();
            }

            return View(renters);
        }

        // POST: Renters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var renters = await _context.Renters.FindAsync(id);
            _context.Renters.Remove(renters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentersExists(long id)
        {
            return _context.Renters.Any(e => e.RentersId == id);
        }
    }
}
