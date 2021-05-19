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
                .Select(i => new InvestigationViewModel() {
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
                        latitude = i.report.latitude,
                        longitude = i.report.longitude,
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
                        latitude = investigation.report.latitude,
                        longitude = investigation.report.longitude,
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
                    reportId = 3 //hard coded
                };

                int status = 0;
                if (String.Equals(newInvestigation.reportStatus, "Being Investigated"))
                    status = 1;
                //investigation.report.status = Report.Status.Investigating;
                else if (String.Equals(newInvestigation.reportStatus, "No Action Needed"))
                    status = 2;
                //investigation.report.status = Report.Status.NoAction;
                else if (String.Equals(newInvestigation.reportStatus, "Closed"))
                    status = 3;
                    //investigation.report.status = Report.Status.Closed;
                //CHECK IF REPORT HAS INVESTIGATION

                _nemesysRepository.AddNewInvestigation(investigation);
                _nemesysRepository.AddInvestigationToReport(investigation.reportId, investigation.idNum, status);
                return RedirectToAction("Index");
            }
            else
                return View(newInvestigation);
        }

        // GET: Investigations/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var existingInvestigation = _nemesysRepository.GetInvestigationById(id);
            if (existingInvestigation != null)
            {
                CreateEditInvestigationViewModel model = new CreateEditInvestigationViewModel()
                {
                    Id = existingInvestigation.idNum,
                    dateTime = existingInvestigation.dateTime,
                    description = existingInvestigation.description,
                    reportStatus = existingInvestigation.report.status.ToString()
                };
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Investigations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, dateTime, description, reportStatus")] CreateEditInvestigationViewModel updatedInvestigation)
        {
            var modelToUpdate = _nemesysRepository.GetInvestigationById(id);
            if (modelToUpdate == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                modelToUpdate.dateTime = updatedInvestigation.dateTime;
                modelToUpdate.description = updatedInvestigation.description;

                if (String.Equals(updatedInvestigation.reportStatus, "Being Investigated"))
                    modelToUpdate.report.status = Report.Status.Investigating;
                else if (String.Equals(updatedInvestigation.reportStatus, "No Action Needed"))
                    modelToUpdate.report.status = Report.Status.NoAction;
                else if (String.Equals(updatedInvestigation.reportStatus, "Closed"))
                    modelToUpdate.report.status = Report.Status.Closed;

                _nemesysRepository.UpdateInvestigation(modelToUpdate);
                return RedirectToAction("Index");
            }
            else
                return View(updatedInvestigation);
            
        }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var investigationToDelete = _nemesysRepository.GetInvestigationById(id);
            if (investigationToDelete == null)
                return NotFound();
            _nemesysRepository.DeleteInvestigation(investigationToDelete);
            return RedirectToAction("Index");
        }

        /*private bool InvestigationExists(int id)
        {
            return _nemesysRepository.Investigations.Any(e => e.idNum == id);
        }*/
    }
}
