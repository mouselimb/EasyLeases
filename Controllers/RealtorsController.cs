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
    public class RealtorsController : Controller
    {
        private readonly EasyLeasesDBContext _context;

        public RealtorsController(EasyLeasesDBContext context)
        {
            _context = context;
        }

        // GET: Realtors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Realtors.ToListAsync());
        }



        // GET: Realtors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtors = await _context.Realtors
                .FirstOrDefaultAsync(m => m.RealtorsId == id);
            if (realtors == null)
            {
                return NotFound();
            }

            return View(realtors);
        }

        // GET: Realtors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Realtors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("RealtorsId,RealtorName,CommissionRate,RealtorCompany,RealtorPhoneNumber,RealtorUserName,RealtorPassword,RealtorReview")] Realtors realtors)
        {
            if (ModelState.IsValid)
            {
                _context.Add(realtors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(realtors);
        }

        // GET: Realtors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtors = await _context.Realtors.FindAsync(id);
            if (realtors == null)
            {
                return NotFound();
            }
            return View(realtors);
        }

        // POST: Realtors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RealtorsId,RealtorName,CommissionRate,RealtorCompany,RealtorPhoneNumber,RealtorUserName,RealtorPassword,RealtorReview")] Realtors realtors)
        {
            if (id != realtors.RealtorsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realtors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealtorsExists(realtors.RealtorsId))
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
            return View(realtors);
        }

        // GET: Realtors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtors = await _context.Realtors
                .FirstOrDefaultAsync(m => m.RealtorsId == id);
            if (realtors == null)
            {
                return NotFound();
            }

            return View(realtors);
        }

        // POST: Realtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var realtors = await _context.Realtors.FindAsync(id);
            _context.Realtors.Remove(realtors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealtorsExists(long id)
        {
            return _context.Realtors.Any(e => e.RealtorsId == id);
        }
    }
}
