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

namespace Nemesys.Controllers
{
    public class AdminController : Controller
    {
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly INemesysRepository _nemesysRepository;
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, INemesysRepository nemesysRepository)
        {
            //_roleManager = roleManager;
            _logger = logger;
            _nemesysRepository = nemesysRepository;
            //_userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index() {
            try
            {
                return View();
            }
            catch (Exception e) 
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUserToRole() {
            try
            {
                var userRoleList = _nemesysRepository.GetAllUserRoles().Select(r => new RoleViewModel() 
                { 
                    Id = r.Id,
                    RoleName = r.NormalizedName
                }).ToList();

                var model = new AddUserToRoleViewModel() 
                { 
                    UserRoleList = userRoleList
                };

                return View(model);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddUserToRole([Bind("userEmail, RoleId")] AddUserToRoleViewModel userToRole) {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = _nemesysRepository.GetUserByEmail(userToRole.userEmail);
                    string newRole = _nemesysRepository.GetRoleNameById(userToRole.RoleId);
                    if (user != null && newRole != null)
                    {
                        _nemesysRepository.ChangeUserRole(user, newRole);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var userRoleList = _nemesysRepository.GetAllUserRoles().Select(r => new RoleViewModel()
                        {
                            Id = r.Id,
                            RoleName = r.NormalizedName
                        }).ToList();

                        userToRole.UserRoleList = userRoleList;

                        return View(userToRole);
                    }
                }
                else
                {
                    var userRoleList = _nemesysRepository.GetAllUserRoles().Select(r => new RoleViewModel()
                    {
                        Id = r.Id,
                        RoleName = r.Name
                    }).ToList();

                    userToRole.UserRoleList = userRoleList;

                    return View(userToRole);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AllRoles()
        {
            try
            {
                var model = new ListRolesViewModel()
                {
                    TotalEntries = _nemesysRepository.GetAllUserRoles().Count(),
                    UserRoles = _nemesysRepository.GetAllUserRoles().Select(r => new RoleViewModel()
                    { 
                        Id = r.Id,
                        RoleName = r.Name
                    })
                };
                return View(model);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole() 
        {
            try
            {
                return View();
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole([Bind("Id, RoleName")] CreateRoleViewModel newRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = newRole.RoleName
                    };

                    IdentityResult result = _nemesysRepository.AddNewRole(identityRole).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _logger.LogError(result.Errors.ToString());
                        return View("Error");
                    }
                } 
                else
                    return View(newRole);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }
    }
}