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
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,string searchString,int? page,string? ddlStatus,string? txtSearchEmail, string? txtSearchmobileNo)
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
                ViewData["EmailSortParm"] = sortOrder == "Email" ? "email_desc" : "Email";
                ViewData["DateEntered"] = sortOrder == "DateEntered" ? "de_desc" : "DateEntered";
                ViewData["DateModified"] = sortOrder == "DateModified" ? "dm_desc" : "DateModified";

                ViewData["SequenceSort"] = sortOrder == "SequenceSort" ? "desc_sequence" : "SequenceSort";

                ViewData["CurrentFilter"] = searchString;
                ViewBag.SearchStatus = ddlStatus;

                var model = from l in _context.CustomerMasters where l.IsDeleted == false select l;

                #region searching 
                if (!string.IsNullOrEmpty(searchString))
                {
                    model = model.Where(x => x.Name.Trim() == searchString.Trim());
                    ViewData["searchname"] = searchString;
                }

                if (!string.IsNullOrEmpty(txtSearchEmail))
                {
                    model = model.Where(x=>x.EmailId.Trim().ToLower()  == txtSearchEmail.ToLower().ToLower());
                    ViewBag.searchEmail = txtSearchEmail;
                }

                if (!string.IsNullOrEmpty(txtSearchmobileNo))
                {
                    model = model.Where(x => x.MobileNumber.Trim() == txtSearchmobileNo.Trim());
                    ViewBag.searchMobileNo = txtSearchmobileNo;
                }
                #endregion

                #region Sorting 
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

                    case "Email":
                        model = model.OrderBy(s => s.EmailId);
                        break;

                    case "email_desc":
                        model = model.OrderByDescending(s => s.EmailId);
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
                #endregion

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

        // GET: CustomerMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerMasters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerMaster customerMaster)
        {
            if (ModelState.IsValid)
            {
                if(customerMaster != null)
                {
                    // Sanitize and validate user input
                    customerMaster.Name = customerMaster.Name.Trim();
                    customerMaster.EmailId = customerMaster.EmailId.Trim();
                    customerMaster.MobileNumber = customerMaster.MobileNumber.Trim();

                    if (!await _context.CustomerMasters.AnyAsync(x=>x.EmailId == customerMaster.EmailId))
                    {
                        if(!await _context.CustomerMasters.AnyAsync(x=>x.MobileNumber == customerMaster.MobileNumber))
                        {
                            try
                            {
                                await _context.AddAsync(customerMaster);
                                await _context.SaveChangesAsync();

                                TempData["Success"] = "Customer Successfully Created.";

                                return RedirectToAction(nameof(Index));
                            }
                            catch(Exception ex)
                            {
                                var msg = CommonFunctions.getExceptionMessage(ex);
                            }

                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            //same mobile present
                            TempData["warning"] = "Customer with same mobile number already present in database.";
                        }
                    }
                    else
                    {
                        //same email present 
                        TempData["warning"] = "Customer with same email ID already present in database.";
                    }
                }

            }

            return View(customerMaster);
        }

        // GET: CustomerMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["warning"] = "The provided id value is null.";

                return RedirectToAction(nameof(Index));
            }

            var customerMaster = await _context.CustomerMasters.FindAsync(id);

            if (customerMaster == null)
            {
                TempData["warning"] = "Data not found in given ID";

                return RedirectToAction(nameof(Index));

            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CustomerMaster customerMaster)
        {
            if (id != customerMaster.CustomerId)
            {
                TempData["warning"] = "URL value and primery key in form not match.";

                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(customerMaster != null)
                    {
                        // Sanitize and validate user input
                        customerMaster.Name = customerMaster.Name.Trim();
                        customerMaster.EmailId = customerMaster.EmailId.Trim();
                        customerMaster.MobileNumber = customerMaster.MobileNumber.Trim();

                        if (!await _context.CustomerMasters.AnyAsync(x=>x.EmailId == customerMaster.EmailId && x.CustomerId != id))
                        {
                            if(!await _context.CustomerMasters.AnyAsync(x=>x.MobileNumber == customerMaster.MobileNumber && x.CustomerId != id))
                            {
                                try
                                {
                                    var model = _context.CustomerMasters.Find(id);

                                    if (model != null)
                                    {
                                        model.Name = customerMaster.Name;
                                        model.EmailId = customerMaster.EmailId;
                                        model.MobileNumber = customerMaster.MobileNumber;
                                        model.Address = customerMaster.Address;

                                        model.WhenModified = DateTime.UtcNow;

                                        await _context.SaveChangesAsync();

                                        TempData["Success"] = "Customer Successfully Updated.";

                                    }
                                    else
                                    {
                                        TempData["warning"] = "Data not found in given ID";
                                    }
                                }
                                catch(Exception ex)
                                {
                                    var msg = CommonFunctions.getExceptionMessage(ex);
                                }
                            }
                            else
                            {
                                //same mobile no
                                TempData["warning"] = "Customer with same mobile number already present in database.";

                                return View(customerMaster);
                            }
                        }
                        else
                        {
                            //same email 
                            TempData["warning"] = "Customer with same email ID already present in database.";

                            return View(customerMaster);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CustomerMasterExists(customerMaster.CustomerId))
                    {
                        //return NotFound();
                        var msg = CommonFunctions.getExceptionMessage(ex);
                    }
                    else
                    {
                        var msg = CommonFunctions.getExceptionMessage(ex);
                        //throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customerMaster);
        }

        // POST: CustomerMasters/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var customerMaster = await _context.CustomerMasters.FindAsync(id);
                if (customerMaster != null)
                {
                    customerMaster.IsDeleted = true;

                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Customer Successfully Deleted.";
                }
                else
                {
                    TempData["warning"] = "Data not found in given ID";
                }
            }
            catch(Exception ex)
            {
                var msg = CommonFunctions.getExceptionMessage(ex);
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int statusID)
        {
            try
            {
                var customerMaster = await _context.CustomerMasters.FindAsync(statusID);

                if (customerMaster != null)
                {
                    if(customerMaster.IsActive)
                    {
                        customerMaster.IsActive = false;
                    }
                    else
                    {
                        customerMaster.IsActive = true;
                    }

                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Customer Status Change Successfully.";
                }
                else
                {
                    TempData["warning"] = "Data not found in given ID";
                }
            }
            catch (Exception ex)
            {
                var msg = CommonFunctions.getExceptionMessage(ex);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerMasterExists(int id)
        {
            return _context.CustomerMasters.Any(e => e.CustomerId == id);
        }
    }
}
