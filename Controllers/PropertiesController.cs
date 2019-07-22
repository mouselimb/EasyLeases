using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyLeasesDB.Models;
using EasyLeasesDB.App_Code;
using RestSharp;

namespace EasyLeasesDB.Controllers
{
    [Route("api/[controller]")]
    public class PropertiesController : Controller
    {
        private readonly EasyLeasesDBContext _context;
        private readonly PropertiesRepository _properties;

        public PropertiesController(EasyLeasesDBContext context)
        {
            _context = context;
            _properties = new PropertiesRepository(context);
        }

        [HttpGet("byid")]
        [Route("Properties/{id}")]
        public ActionResult AddEditProperty(int itemId)
        {
            Properties model = new Properties();
            if (itemId > 0)
            {
                model = _properties.GetProperty(itemId);
            }
            return PartialView("_propertyForm", model);
        }

        [Route("Properties/")]
        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var easyLeasesDBContext = _context.Properties.Include(p => p.FkHomeowners);
            return View(await easyLeasesDBContext.ToListAsync());
        }

        [HttpGet("")]
        public IActionResult GetAllProperties()
        {
            List<Properties> properties = _properties.GetAllProperties();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public IActionResult GetProperty(long id)
        {
            Properties property = _properties.GetProperty(id);
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpGet("byaddress")]
        //"/api/Customers/byusername?userName="
        public Properties GetPropertyAddress(String propAddress)
        {
            PropertiesRepository _propRepoAddress = new PropertiesRepository(_context);
            Properties propRepoAddress = _propRepoAddress.GetPropertyAddress(propAddress);
            return propRepoAddress;
        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var properties = await _context.Properties
                .Include(p => p.FkHomeowners)
                .FirstOrDefaultAsync(m => m.PropertiesId == id);
            if (properties == null)
            {
                return NotFound();
            }

            return View(properties);
        }

        // GET: Properties/Create
        //public IActionResult Create([Bind("PropertiesId,FkHomeownersId,AvailableDate,PropertyRentAmount,PropertyAddress,PropertyCity,PropertyState,PropertyZip,PropertyFloors,PropertyBeds,PropertyBaths,PropertyLivingSqFt,IsLeased")] Properties properties)
        //{
        //    ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId");
        //    return View();
        //}

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Properties/Create")]
        public async Task<IActionResult> Create([Bind("PropertiesId,AvailableDate,PropertyRentAmount,PropertyAddress,PropertyCity,PropertyState,PropertyZip,PropertyFloors,PropertyBeds,PropertyBaths,PropertyLivingSqFt,IsLeased")] Properties properties)
        {
            if (ModelState.IsValid)
            {
                if (properties.PropertiesId > 0)
                    _properties.UpdateProperty(properties.PropertiesId, properties);
                else
                    _properties.CreateProperty(properties);
            }
            await _context.SaveChangesAsync();
            ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId", properties.FkHomeownersId);
            return RedirectToAction("Index");
        }

        // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var properties = await _context.Properties.FindAsync(id);
            if (properties == null)
            {
                return NotFound();
            }
            ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId", properties.FkHomeownersId);
            return View(properties);
        }
        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Properties/Edit")]
        public async Task<IActionResult> Edit(long id, [Bind("PropertiesId,AvailableDate,PropertyRentAmount,PropertyAddress,PropertyCity,PropertyState,PropertyZip,PropertyFloors,PropertyBeds,PropertyBaths,PropertyLivingSqFt,IsLeased")] Properties properties)
        {
            if (id != properties.PropertiesId)
            {
                return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(properties);
                        await _context.SaveChangesAsync();
                        }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PropertiesExists(properties.PropertiesId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index");
                }
                ViewData["FkHomeownersId"] = new SelectList(_context.Homeowners, "HomeownersId", "HomeownersId", properties.FkHomeownersId);
                return View(properties);
            }
        
            // GET: Properties/Delete/5
            public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var properties = await _context.Properties
                .Include(p => p.FkHomeowners)
                .FirstOrDefaultAsync(m => m.PropertiesId == id);
            if (properties == null)
            {
                return NotFound();
            }

            return View(properties);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var properties = await _context.Properties.FindAsync(id);
            _context.Properties.Remove(properties);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertiesExists(long id)
        {
            return _context.Properties.Any(e => e.PropertiesId == id);
        }
    }
}
