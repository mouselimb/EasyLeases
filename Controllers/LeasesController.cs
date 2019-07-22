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
    public class LeasesController : Controller
    {
        private readonly EasyLeasesDBContext _context;

        public LeasesController(EasyLeasesDBContext context)
        {
            _context = context;
        }

        // GET: Leases
        public async Task<IActionResult> Index()
        {
            var easyLeasesDBContext = _context.Leases.Include(l => l.FkHomeowners).Include(l => l.FkProperties).Include(l => l.FkRealtors).Include(l => l.FkRenters);
            return View(await easyLeasesDBContext.ToListAsync());
        }

        // GET: Leases/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leases = await _context.Leases
                .Include(l => l.FkHomeowners)
                .Include(l => l.FkProperties)
                .Include(l => l.FkRealtors)
                .Include(l => l.FkRenters)
                .FirstOrDefaultAsync(m => m.LeasesId == id);
            if (leases == null)
            {
                return NotFound();
            }

            return View(leases);
        }

        // GET: Leases/Create
        public IActionResult Create()
        {
            ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId");
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId");
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId");
            ViewData["FkRentersId"] = new SelectList(_context.Renters, "RentersId", "RentersId");
            return View();
        }

        // POST: Leases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeasesId,FkArId,FkHomeownersId,FkPropertiesId,FkRealtorsId,FkRentersId,LeaseNumber,LeaseStartDate,LeaseEndDate,LeaseAmount")] Leases leases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId", leases.FkHomeownersId);
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId", leases.FkPropertiesId);
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", leases.FkRealtorsId);
            ViewData["FkRentersId"] = new SelectList(_context.Renters, "RentersId", "RentersId", leases.FkRentersId);
            return View(leases);
        }

        // GET: Leases/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leases = await _context.Leases.FindAsync(id);
            if (leases == null)
            {
                return NotFound();
            }
            ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId", leases.FkHomeownersId);
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId", leases.FkPropertiesId);
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", leases.FkRealtorsId);
            ViewData["FkRentersId"] = new SelectList(_context.Renters, "RentersId", "RentersId", leases.FkRentersId);
            return View(leases);
        }

        // POST: Leases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("LeasesId,FkArId,FkHomeownersId,FkPropertiesId,FkRealtorsId,FkRentersId,LeaseNumber,LeaseStartDate,LeaseEndDate,LeaseAmount")] Leases leases)
        {
            if (id != leases.LeasesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leases);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeasesExists(leases.LeasesId))
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
            ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId", leases.FkHomeownersId);
            ViewData["FkPropertiesId"] = new SelectList(_context.Properties, "PropertiesId", "PropertiesId", leases.FkPropertiesId);
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", leases.FkRealtorsId);
            ViewData["FkRentersId"] = new SelectList(_context.Renters, "RentersId", "RentersId", leases.FkRentersId);
            return View(leases);
        }

        // GET: Leases/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leases = await _context.Leases
                .Include(l => l.FkHomeowners)
                .Include(l => l.FkProperties)
                .Include(l => l.FkRealtors)
                .Include(l => l.FkRenters)
                .FirstOrDefaultAsync(m => m.LeasesId == id);
            if (leases == null)
            {
                return NotFound();
            }

            return View(leases);
        }

        // POST: Leases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var leases = await _context.Leases.FindAsync(id);
            _context.Leases.Remove(leases);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeasesExists(long id)
        {
            return _context.Leases.Any(e => e.LeasesId == id);
        }
    }
}
