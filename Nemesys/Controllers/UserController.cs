using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nemesys.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Nemesys.Models;
using Nemesys.Models.Interfaces;
using Nemesys.ViewModels.Users;
using Nemesys.Models.FormModels;
using Nemesys.ViewModels.Reports;
using Nemesys.ViewModels.HazardTypes;
using Nemesys.ViewModels.Investigations;

namespace Nemesys.Controllers
{
    public class UserController : Controller
    {
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly INemesysRepository _nemesysRepository;
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ILogger<AdminController> logger, INemesysRepository nemesysRepository, UserManager<ApplicationUser> userManager)
        {
            //_roleManager = roleManager;
            _logger = logger;
            _nemesysRepository = nemesysRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ProfilePage(string email) {
            try
            {
                var user = _nemesysRepository.GetUserByEmail(email);
                if (user == null)
                    return NotFound();
                else
                {
                    var model = new ProfilePageViewModel()
                    {
                        idNum = user.Id,
                        email = user.Email,
                        faculty = user.faculty,
                        role = _nemesysRepository.GetRoleNameByUser(user)
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

        [Authorize]
        [HttpGet]
        public IActionResult Reports(string id){
            try
            {
                IEnumerable<ReportViewModel> reports = _nemesysRepository
                    .GetReportsByOwner(id)
                    .OrderByDescending(r => r.dateTime)
                    .Select(r => new ReportViewModel { 
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
                        upvotes = r.upvotes,
                        image = r.image,
                        status = r.status.ToString(),
                        Reporter = new ReporterViewModel() 
                        {
                            idNum = r.UserId,
                            email = _userManager.FindByIdAsync(r.UserId).Result.Email
                        }
                    });
                
                var model = new ReportListViewModel()
                {
                    TotalEntries = reports.Count(),
                    Reports = reports
                };

                return View(model);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        public IActionResult Investigations(string id)
        {
            try
            {
                IEnumerable<InvestigationViewModel> investigations = _nemesysRepository
                    .GetInvestigationsByOwner(id)
                    .OrderByDescending(i => i.dateTime)
                    .Select(i => new InvestigationViewModel()
                    {
                        Id = i.idNum,
                        dateTime = i.dateTime,
                        description = i.description,
                        investigator = new InvestigatorViewModel()
                        {
                            idNum = i.UserId,
                            email = _userManager.FindByIdAsync(i.UserId).Result.Email
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
                    });

                var model = new InvestigationListViewModel()
                {
                    TotalEntries = investigations.Count(),
                    Investigations = investigations
                };

                return View(model);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }
    }
}