using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nemesys.DAL;
using Nemesys.Models;
using Nemesys.Models.FormModels;
using Nemesys.Models.Interfaces;
using Nemesys.ViewModels;
using Nemesys.ViewModels.HazardTypes;
using Nemesys.ViewModels.Investigations;
using Nemesys.ViewModels.Reports;
using Nemesys.ViewModels.Users;

namespace Nemesys.Controllers
{
    public class ReportsController : Controller
    {
        private readonly INemesysRepository _nemesysRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReportsController> _logger;
        public ReportsController(INemesysRepository nemesysRepository, UserManager<ApplicationUser> userManager, ILogger<ReportsController> logger)
        {
            _nemesysRepository = nemesysRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new ReportListViewModel()
                {
                    TotalEntries = _nemesysRepository.GetAllReports().Count(),
                    Reports = _nemesysRepository
                    .GetAllReports()
                    .OrderByDescending(r => r.dateTime)
                    .Select(r => new ReportViewModel
                    {
                        Id = r.idNum,
                        Title = r.title,
                        dateTime = r.dateTime,
                        description = r.description,
                        latitude = r.latitude,
                        longitude = r.longitude,
                        hazardType = new HazardTypeViewModel()
                        {
                            Id = r.hazardTypeId,
                            hazardTypeName = r.hazardType.hazardTypeName
                        },
                        //Owner goes here
                        upvotes = r.upvotes,
                        image = r.image,
                        status = r.status.ToString(),
                        Reporter = new ReporterViewModel()
                        {
                            idNum = r.UserId,
                            email = _userManager.FindByIdAsync(r.UserId).Result.Email
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

        public IActionResult Details(int id) {
            try
            {
                var report = _nemesysRepository.GetReportById(id);
                if (report == null)
                    return NotFound();
                else
                {
                    //ReportViewModel model;
                    var model = new ReportViewModel();
                    if (report.investigation != null)
                    {
                        model = new ReportViewModel()
                        {
                            Id = report.idNum,
                            Title = report.title,
                            dateTime = report.dateTime,
                            description = report.description,
                            latitude = report.latitude,
                            longitude = report.longitude,
                            upvotes = report.upvotes,
                            image = report.image,
                            status = report.status.ToString(),
                            investigation = new InvestigationViewModel()
                            {
                                Id = report.investidationId,
                                dateTime = report.investigation.dateTime,
                                description = report.investigation.description,
                            },
                            hazardType = new HazardTypeViewModel()
                            {
                                Id = report.hazardTypeId,
                                hazardTypeName = report.hazardType.hazardTypeName
                            },
                            Reporter = new ReporterViewModel()
                            {
                                idNum = report.UserId,
                                email = _userManager.FindByIdAsync(report.UserId).Result.Email
                            }
                        };
                    }
                    else
                    {
                        model = new ReportViewModel()
                        {
                            Id = report.idNum,
                            Title = report.title,
                            dateTime = report.dateTime,
                            description = report.description,
                            latitude = report.latitude,
                            longitude = report.longitude,
                            upvotes = report.upvotes,
                            image = report.image,
                            status = report.status.ToString(),
                            hazardType = new HazardTypeViewModel()
                            {
                                Id = report.hazardTypeId,
                                hazardTypeName = report.hazardType.hazardTypeName
                            },
                            Reporter = new ReporterViewModel()
                            {
                                idNum = report.UserId,
                                email = _userManager.FindByIdAsync(report.UserId).Result.Email
                            }
                        };
                    }
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
        public async Task<IActionResult> Edit(int id) {
            try
            {
                var existingReport = _nemesysRepository.GetReportById(id);
                if (existingReport != null)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (existingReport.User.Id == currentUser.Id)
                    {
                        CreateEditReportViewModel model = new CreateEditReportViewModel()
                        {
                            Id = existingReport.idNum,
                            Title = existingReport.title,
                            description = existingReport.description,
                            imageUrl = existingReport.image,
                            hazardTypeId = existingReport.hazardTypeId,
                            latitude = existingReport.latitude,
                            longitude = existingReport.longitude
                        };

                        var hazazrdTypeList = _nemesysRepository.GetAllHazardTypes().Select(h => new HazardTypeViewModel()
                        {
                            Id = h.Id,
                            hazardTypeName = h.hazardTypeName
                        }).ToList();

                        model.HazardTypeList = hazazrdTypeList;

                        return View(model);
                    }
                    else
                        return Unauthorized();
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
        public async Task<IActionResult> Edit([FromRoute] int id, [Bind("Id, Title, description, image, hazardTypeId")] CreateEditReportViewModel updatedReport) {
            try
            {
                var modelToUpdate = _nemesysRepository.GetReportById(id);
                if (modelToUpdate == null)
                    return NotFound();

                var currenUser = await _userManager.GetUserAsync(User);
                if (modelToUpdate.User.Id == currenUser.Id)
                {
                    if (ModelState.IsValid)
                    {
                        string imageUrl = "";
                        if (updatedReport.image != null)
                        {
                            string fileName = "";
                            var extention = "." + updatedReport.image.FileName.Split('.')[updatedReport.image.FileName.Split('.').Length - 1];
                            fileName = Guid.NewGuid().ToString() + extention;
                            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\reportimages\\" + fileName;
                            using (var bits = new FileStream(path, FileMode.Create))
                            {
                                updatedReport.image.CopyTo(bits);
                            }
                            imageUrl = "/images/reportimages/" + fileName;
                        }
                        else
                            imageUrl = modelToUpdate.image;

                        modelToUpdate.title = updatedReport.Title;
                        modelToUpdate.latitude = updatedReport.latitude;
                        modelToUpdate.longitude = updatedReport.longitude;
                        modelToUpdate.description = updatedReport.description;
                        modelToUpdate.image = imageUrl;
                        modelToUpdate.hazardTypeId = updatedReport.hazardTypeId;

                        _nemesysRepository.UpdateReport(modelToUpdate);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var hazardTypeList = _nemesysRepository.GetAllHazardTypes().Select(h => new HazardTypeViewModel()
                        {
                            Id = h.Id,
                            hazardTypeName = h.hazardTypeName
                        }).ToList();

                        updatedReport.HazardTypeList = hazardTypeList;

                        return View(updatedReport);
                    }
                }
                else
                    return Unauthorized();
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create() {
            try
            {
                var hazardTypeList = _nemesysRepository.GetAllHazardTypes().Select(h => new HazardTypeViewModel()
                {
                    Id = h.Id,
                    hazardTypeName = h.hazardTypeName
                }).ToList();

                var model = new CreateEditReportViewModel()
                {
                    HazardTypeList = hazardTypeList
                };

                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Create([Bind("Title, dateTime, description, latitude, longitude, image, hazardTypeId,")] CreateEditReportViewModel newReport) {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    if (newReport.image != null)
                    {
                        var extention = "." + newReport.image.FileName.Split('.')[newReport.image.FileName.Split('.').Length - 1];
                        fileName = Guid.NewGuid().ToString() + extention;
                        var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\reportimages\\" + fileName;
                        using (var bits = new FileStream(path, FileMode.Create))
                        {
                            newReport.image.CopyTo(bits);
                        }
                    }


                    Report report = new Report()
                    {
                        title = newReport.Title,
                        dateTime = newReport.dateTime,
                        description = newReport.description,
                        latitude = newReport.latitude,
                        longitude = newReport.longitude,
                        image = "/images/reportimages/" + fileName,
                        upvotes = 0,
                        status = Report.Status.Open,
                        hazardTypeId = newReport.hazardTypeId,
                        UserId = _userManager.GetUserId(User)
                    };

                    _nemesysRepository.AddNewReport(report);
                    return RedirectToAction("Index");
                }
                else
                {
                    var hazardTypeList = _nemesysRepository.GetAllHazardTypes().Select(h => new HazardTypeViewModel()
                    {
                        Id = h.Id,
                        hazardTypeName = h.hazardTypeName
                    }).ToList();

                    newReport.HazardTypeList = hazardTypeList;

                    return View(newReport);
                }
            }
            catch(DbUpdateException e)
            {
                SqlException s = e.InnerException as SqlException;
                if(s != null)
                {
                    switch (s.Number)
                    {
                        case 547:
                            {
                                ModelState.AddModelError(string.Empty, string.Format("Foreign key for hazard type with Id '{0}' does not exist.", newReport.hazardTypeId));
                                break;
                            }
                        default:
                            {
                                ModelState.AddModelError(string.Empty, "A database error occured! Please contact a system admin.");
                                break;
                            }
                    }
                    var hazardTypeList = _nemesysRepository.GetAllHazardTypes().Select(h => new HazardTypeViewModel()
                    {
                        Id = h.Id,
                        hazardTypeName = h.hazardTypeName
                    }).ToList();

                    newReport.HazardTypeList = hazardTypeList;
                }
                return View(newReport);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int id) {
            try
            {
                var reportToDelete = _nemesysRepository.GetReportById(id);
                if (reportToDelete == null)
                    return NotFound();
                if (reportToDelete.investigation != null)
                    _nemesysRepository.DeleteInvestigation(reportToDelete.investigation);

                _nemesysRepository.DeleteReport(reportToDelete);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }
    }
}
