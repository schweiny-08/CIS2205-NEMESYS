using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nemesys.DAL;
using Nemesys.Models.FormModels;
using Nemesys.Models.Interfaces;
using Nemesys.Models.Repositories;
using Nemesys.ViewModels.Investigations;
using Nemesys.ViewModels.Reports;
using Nemesys.ViewModels.Users;

namespace Nemesys.Controllers
{
    public class InvestigationsController : Controller
    {
        private readonly INemesysRepository _nemesysRepository;

        public InvestigationsController(INemesysRepository nemesysRepository)
        {
            _nemesysRepository = nemesysRepository;
        }

        // GET: Investigations
        public IActionResult Index()
        {
            var model = new InvestigationListViewModel() {
                TotalEntries = _nemesysRepository.GetAllInvestigations().Count(),
                Investigations = _nemesysRepository
                .GetAllInvestigations()
                .OrderByDescending(i => i.dateTime)
                .Select(i => new InvestigationViewModel(){ 
                    Id = i.idNum,
                    dateTime = i.dateTime,
                    description = i.description,
                    investigator = new InvestigatorViewModel() { 
                        idNum = i.investigator.idNum,
                        email = i.investigator.email,
                        fName = i.investigator.fName,
                        lName = i.investigator.lName,
                        imageUrl = i.investigator.image,
                        deptNum = i.investigator.deptNum
                    },
                    report = new ReportViewModel()
                    {
                        Id = i.report.idNum,
                        Title = i.report.title,
                        dateTime = i.report.dateTime,
                        description = i.report.description,
                        location = i.report.description,
                        upvotes = i.report.upvotes,
                        image = i.report.image,
                        status = i.report.status.ToString()
                    }
                })
            };
            return View(model);
        }

        // GET: Investigations/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var investigation = _nemesysRepository.GetInvestigationById(id);

            if (investigation == null)
                return NotFound();
            else {
                var model = new InvestigationViewModel()
                {
                    Id = investigation.idNum,
                    dateTime = investigation.dateTime,
                    description = investigation.description,
                    investigator = new InvestigatorViewModel()
                    {
                        idNum = investigation.investigator.idNum,
                        email = investigation.investigator.email,
                        fName = investigation.investigator.fName,
                        lName = investigation.investigator.lName,
                        imageUrl = investigation.investigator.image,
                        deptNum = investigation.investigator.deptNum
                    },
                    report = new ReportViewModel()
                    {
                        Id = investigation.report.idNum,
                        Title = investigation.report.title,
                        dateTime = investigation.report.dateTime,
                        description = investigation.report.description,
                        location = investigation.report.location,
                        upvotes = investigation.report.upvotes,
                        image = investigation.report.image,
                        status = investigation.report.status.ToString()
                    }
                };
                return View(model);
            }
        }

        // GET: Investigations/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Investigations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, dateTime, description, reportStatus, reportId")] CreateEditInvestigationViewModel newInvestigation)
        {
            if (ModelState.IsValid)
            {
                Investigation investigation = new Investigation()
                {
                    dateTime = newInvestigation.dateTime,
                    description = newInvestigation.description,
                    investigatorId = 2, //hard coded
                    reportId = 1 //hard coded
                };

                //CHECK IF REPORT HAS INVESTIGATION

                _nemesysRepository.AddNewInvestigation(investigation);
                return RedirectToAction("Index");
            }
            else
                return View(newInvestigation);
        }

        // GET: Investigations/Edit/5
        /* public async Task<IActionResult> Edit(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var investigation = await _nemesysRepository.Investigations.FindAsync(id);
             if (investigation == null)
             {
                 return NotFound();
             }
             ViewData["investigatorId"] = new SelectList(_nemesysRepository.Investigators, "idNum", "email", investigation.investigatorId);
             ViewData["reportId"] = new SelectList(_nemesysRepository.Reports, "idNum", "idNum", investigation.reportId);
             return View(investigation);
         }*/

        // POST: Investigations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("investigatorId,reportId,idNum,dateTime,description")] Investigation investigation)
        {
            if (id != investigation.idNum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _nemesysRepository.Update(investigation);
                    await _nemesysRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestigationExists(investigation.idNum))
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
            ViewData["investigatorId"] = new SelectList(_nemesysRepository.Investigators, "idNum", "email", investigation.investigatorId);
            ViewData["reportId"] = new SelectList(_nemesysRepository.Reports, "idNum", "idNum", investigation.reportId);
            return View(investigation);
        }
*/
        // GET: Investigations/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigation = await _nemesysRepository.Investigations
                .Include(i => i.investigator)
                .Include(i => i.report)
                .FirstOrDefaultAsync(m => m.idNum == id);
            if (investigation == null)
            {
                return NotFound();
            }

            return View(investigation);
        }*/

        // POST: Investigations/Delete/5
        /* [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             var investigation = await _nemesysRepository.Investigations.FindAsync(id);
             _nemesysRepository.Investigations.Remove(investigation);
             await _nemesysRepository.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }*/

        /*  private bool InvestigationExists(int id)
          {
              return _nemesysRepository.Investigations.Any(e => e.idNum == id);
          }*/
    }
}
