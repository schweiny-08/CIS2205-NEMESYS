using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<InvestigationsController> _logger;

        public InvestigationsController(INemesysRepository nemesysRepository, UserManager<IdentityUser> userManager, ILogger<InvestigationsController> logger)
        {
            _nemesysRepository = nemesysRepository;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Investigations
        public IActionResult Index()
        {
            try
            {
                var model = new InvestigationListViewModel()
                {
                    TotalEntries = _nemesysRepository.GetAllInvestigations().Count(),
                    Investigations = _nemesysRepository
                    .GetAllInvestigations()
                    .OrderByDescending(i => i.dateTime)
                    .Select(i => new InvestigationViewModel()
                    {
                        Id = i.idNum,
                        dateTime = i.dateTime,
                        description = i.description,
                        investigator = new InvestigatorViewModel()
                        {
                            idNum = i.UserId,
                            email = _userManager.FindByIdAsync(i.UserId).Result.Email,
                            fName = (_userManager.FindByIdAsync(i.UserId).Result != null) ? _userManager.FindByIdAsync(i.UserId).Result.UserName : "Anonymous"
                            //lName = i.investigator.lName,
                            //imageUrl = i.investigator.image,
                            //deptNum = i.investigator.deptNum
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
            catch (Exception e) {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var investigation = _nemesysRepository.GetInvestigationById(id);

                if (investigation == null)
                    return NotFound();
                else
                {
                    var model = new InvestigationViewModel()
                    {
                        Id = investigation.idNum,
                        dateTime = investigation.dateTime,
                        description = investigation.description,
                        investigator = new InvestigatorViewModel()
                        {
                            idNum = investigation.UserId,
                            email = _userManager.FindByIdAsync(investigation.UserId).Result.Email,
                            fName = (_userManager.FindByIdAsync(investigation.UserId).Result != null) ? _userManager.FindByIdAsync(investigation.UserId).Result.UserName : "Anonymous"  
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
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, dateTime, description, reportStatus, reportId")] CreateEditInvestigationViewModel newInvestigation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Investigation investigation = new Investigation()
                    {
                        dateTime = newInvestigation.dateTime,
                        description = newInvestigation.description,
                        UserId = _userManager.GetUserId(User),
                        reportId = 1 //hard coded
                    };

                    int status = 0;
                    if (String.Equals(newInvestigation.reportStatus, "Being Investigated"))
                        status = 1;
                    else if (String.Equals(newInvestigation.reportStatus, "No Action Needed"))
                        status = 2;
                    else if (String.Equals(newInvestigation.reportStatus, "Closed"))
                        status = 3;
                    //CHECK IF REPORT HAS INVESTIGATION

                    _nemesysRepository.AddNewInvestigation(investigation);
                    _nemesysRepository.AddInvestigationToReport(investigation.reportId, investigation.idNum, status);
                    return RedirectToAction("Index");
                }
                else
                    return View(newInvestigation);
            }
            catch (DbUpdateException e) {
                SqlException s = e.InnerException as SqlException;
                if (s != null) {
                    switch (s.Number) {
                        case 547: 
                            {
                                ModelState.AddModelError(string.Empty, string.Format("Foreign key for report with Id '{0}' does not exist!", newInvestigation.reportId));
                                break;
                            }
                        default:
                            {
                                ModelState.AddModelError(string.Empty, "A database error has occured! Please contact an admin.");
                                break;
                            }
                    }
                }
                return View(newInvestigation);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
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
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, dateTime, description, reportStatus")] CreateEditInvestigationViewModel updatedInvestigation)
        {
            try
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
            catch (Exception e) {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        // POST: Investigations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var investigationToDelete = _nemesysRepository.GetInvestigationById(id);
                if (investigationToDelete == null)
                    return NotFound();
                _nemesysRepository.DeleteInvestigation(investigationToDelete);
                return RedirectToAction("Index");
            }
            catch (Exception e){
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }   
    }
}
