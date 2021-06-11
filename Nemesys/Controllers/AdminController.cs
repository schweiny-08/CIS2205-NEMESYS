using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Nemesys.ViewModels.Admin;

namespace Nemesys.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AllRoles()
        {
            try
            {
                var model = new ListRolesViewModel()
                {
                    TotalEntries = _roleManager.Roles.Count(),
                    UserRoles = _roleManager.Roles.Select(r => new RoleViewModel()
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
        public async Task<IActionResult> CreateRole([Bind("Id, RoleName")] CreateRoleViewModel newRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = newRole.RoleName
                    };

                    IdentityResult result = await _roleManager.CreateAsync(identityRole);

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