﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nemesys.DAL;
using Nemesys.Models.FormModels;
using Nemesys.Models.Interfaces;
using Nemesys.ViewModels;
using Nemesys.ViewModels.Investigations;
using Nemesys.ViewModels.Reports;

namespace Nemesys.Controllers
{
    public class ReportsController : Controller
    {
        private readonly INemesysRepository _nemesysRepository;

        public ReportsController(INemesysRepository nemesysRepository)
        {
            _nemesysRepository = nemesysRepository;
        }

        public IActionResult Index()
        {
            var model = new ReportListViewModel() {
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
                    upvotes = r.upvotes,
                    image = r.image,
                    status = r.status.ToString()
                })
            };
            return View(model);
        }

        public IActionResult Details(int id) {
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
                        }
                        //reporter = new ReportViewModel(){}
                    };
                }
                else {
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
                        /*investigation = new InvestigationViewModel()
                        {
                            Id = report.investidationId,
                            dateTime = report.investigation.dateTime,
                            description = report.investigation.description,
                        }*/
                        //reporter = new ReportViewModel(){}
                    };
                }
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            var existingReport = _nemesysRepository.GetReportById(id);
            if (existingReport != null)
            {
                CreateEditReportViewModel model = new CreateEditReportViewModel()
                {
                    Id = existingReport.idNum,
                    Title = existingReport.title,
                    description = existingReport.description,
                    imageUrl = existingReport.image
                };

                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, [Bind("Id, Title, description, image")] CreateEditReportViewModel updatedReport) {
            var modelToUpdate = _nemesysRepository.GetReportById(id);
            if (modelToUpdate == null)
                return NotFound();

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
                modelToUpdate.latitude = modelToUpdate.latitude;
                modelToUpdate.longitude = modelToUpdate.longitude;
                modelToUpdate.description = updatedReport.description;
                modelToUpdate.image = imageUrl;

                _nemesysRepository.UpdateReport(modelToUpdate);
                return RedirectToAction("Index");
            }
            else
                return View(updatedReport);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Title, dateTime, description, latitude, longitude, image")] CreateEditReportViewModel newReport) {
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
                    reporteridNum = 1,
                    investidationId = 1
                };

                _nemesysRepository.AddNewReport(report);
                return RedirectToAction("Index");
            }
            else
                return View(newReport);
        }

        [HttpDelete]
        public IActionResult Delete(int id) {
            var reportToDelete = _nemesysRepository.GetReportById(id);
            if (reportToDelete == null)
                return NotFound();
            if (reportToDelete.investigation != null)
                _nemesysRepository.DeleteInvestigation(reportToDelete.investigation);
            _nemesysRepository.DeleteReport(reportToDelete);
            return RedirectToAction("Index");
        }
    }
}
