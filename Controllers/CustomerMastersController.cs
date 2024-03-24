using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerDemoOne.Models;
using X.PagedList;
using CustomerDemoOne.Common;

namespace CustomerDemoOne.Controllers
{
    public class CustomerMastersController : Controller
    {
        private readonly CustomerDemoOneDbContext _context;

        public CustomerMastersController(CustomerDemoOneDbContext context)
        {
            _context = context;
        }

        // GET: CustomerMasters
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,string searchString,int? page,string? ddlStatus)
        {
            try
            {
                if(page != null && page < 1)
                {
                    page = 1;
                }

                ViewData["CurrentSort"] = sortOrder;
                ViewData["IDSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
                ViewData["DateEntered"] = sortOrder == "DateEntered" ? "de_desc" : "DateEntered";
                ViewData["DateModified"] = sortOrder == "DateModified" ? "dm_desc" : "DateModified";

                ViewData["SequenceSort"] = sortOrder == "SequenceSort" ? "desc_sequence" : "SequenceSort";

                ViewData["CurrentFilter"] = searchString;
                ViewBag.SearchStatus = ddlStatus;

                var model = from l in _context.CustomerMasters where l.IsDeleted == false select l;

                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.Name.Trim() == searchString.Trim());
                    ViewData["searchname"] = searchString;
                }

                switch (sortOrder)
                {
                    case "id_desc":
                        model = model.OrderBy(s => s.CustomerId);
                        break;

                    case "Name":
                        model = model.OrderBy(s=>s.Name);
                        break;

                    case "name_desc":
                        model = model.OrderByDescending(s => s.Name);
                        break;
                    case "DateEntered":
                        model = model.OrderBy(s => s.WhenEntered);
                        break;
                    case "de_desc":
                        model = model.OrderByDescending(s => s.WhenEntered);
                        break;
                    case "DateModified":
                        model = model.OrderBy(s => s.WhenModified);
                        break;
                    case "dm_desc":
                        model = model.OrderByDescending(s => s.WhenModified);
                        break;

                    default:
                        model = model.OrderByDescending(s => s.CustomerId);
                        break;

                }

                ViewBag.totalModelCount = 0;

                if(model != null && model.Any())
                {
                    ViewBag.totalModelCount = model.Count();
                }

                var listModel = model!.ToPagedList(page??1,Config.pageSize);

                return View(listModel);

            }
            catch(Exception ex)
            {
                var meg = ex;
            }

            return View(await _context.CustomerMasters.ToListAsync());
        }

        // GET: CustomerMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerMaster = await _context.CustomerMasters
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerMaster == null)
            {
                return NotFound();
            }

            return View(customerMaster);
        }

        // GET: CustomerMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Name,EmailId,MobileNumber,Address,IsActive,IsDeleted,WhenEntered,WhenModified")] CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerMaster);
        }

        // GET: CustomerMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerMaster = await _context.CustomerMasters.FindAsync(id);
            if (customerMaster == null)
            {
                return NotFound();
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Name,EmailId,MobileNumber,Address,IsActive,IsDeleted,WhenEntered,WhenModified")] CustomerMaster customerMaster)
        {
            if (id != customerMaster.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerMasterExists(customerMaster.CustomerId))
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
            return View(customerMaster);
        }

        // GET: CustomerMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerMaster = await _context.CustomerMasters
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerMaster == null)
            {
                return NotFound();
            }

            return View(customerMaster);
        }

        // POST: CustomerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerMaster = await _context.CustomerMasters.FindAsync(id);
            if (customerMaster != null)
            {
                _context.CustomerMasters.Remove(customerMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerMasterExists(int id)
        {
            return _context.CustomerMasters.Any(e => e.CustomerId == id);
        }
    }
}
