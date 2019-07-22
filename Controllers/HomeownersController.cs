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
    public class HomeownersController : Controller
    {
        private readonly EasyLeasesDBContext _context;

        public HomeownersController(EasyLeasesDBContext context)
        {
            _context = context;
        }

        // GET: Homeowners
        public async Task<IActionResult> Index()
        {
            var easyLeasesDBContext = _context.Homeowners.Include(h => h.FkRealtors);
            return View(await easyLeasesDBContext.ToListAsync());
        }

        // GET: Homeowners/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeowners = await _context.Homeowners
                .Include(h => h.FkRealtors)
                .FirstOrDefaultAsync(m => m.HomeownersId == id);
            if (homeowners == null)
            {
                return NotFound();
            }

            return View(homeowners);
        }

        // GET: Homeowners/Create
        public IActionResult Create()
        {
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId");
            return View();
        }

        // POST: Homeowners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HomeownersId,FkRealtorsId,IsRepresented,HomeownerName,HomeownerEmail,HomeownerPhoneNumber,HomeownerAddress,HomeownerCity,HomeownerState,HomeownerZip,HomeownerUserName,HomeownerPassword")] Homeowners homeowners)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeowners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", homeowners.FkRealtorsId);
            return View(homeowners);
        }

        // GET: Homeowners/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeowners = await _context.Homeowners.FindAsync(id);
            if (homeowners == null)
            {
                return NotFound();
            }
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", homeowners.FkRealtorsId);
            return View(homeowners);
        }

        // POST: Homeowners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("HomeownersId,FkRealtorsId,IsRepresented,HomeownerName,HomeownerEmail,HomeownerPhoneNumber,HomeownerAddress,HomeownerCity,HomeownerState,HomeownerZip,HomeownerUserName,HomeownerPassword")] Homeowners homeowners)
        {
            if (id != homeowners.HomeownersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeowners);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeownersExists(homeowners.HomeownersId))
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
            ViewData["FkRealtorsId"] = new SelectList(_context.Realtors, "RealtorsId", "RealtorsId", homeowners.FkRealtorsId);
            return View(homeowners);
        }

        // GET: Homeowners/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeowners = await _context.Homeowners
                .Include(h => h.FkRealtors)
                .FirstOrDefaultAsync(m => m.HomeownersId == id);
            if (homeowners == null)
            {
                return NotFound();
            }

            return View(homeowners);
        }

        // POST: Homeowners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var homeowners = await _context.Homeowners.FindAsync(id);
            _context.Homeowners.Remove(homeowners);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeownersExists(long id)
        {
            return _context.Homeowners.Any(e => e.HomeownersId == id);
        }
    }
}
