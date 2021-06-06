using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nemesys.DAL;
using Nemesys.Models;
using Nemesys.Models.Interfaces;
using Nemesys.ViewModels.HazardTypes;

namespace Nemesys.Controllers
{
    public class HazardTypesController : Controller
    {
        private readonly INemesysRepository _nemesysRepository;
        private readonly ILogger<HazardTypesController> _logger;

        public HazardTypesController(INemesysRepository nemesysRepository, ILogger<HazardTypesController> logger)
        {
            _nemesysRepository = nemesysRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var model = new HazardTypeListViewModel()
                {
                    TotalEntries = _nemesysRepository.GetAllHazardTypes().Count(),
                    HazardTypes = _nemesysRepository.GetAllHazardTypes().Select(h => new HazardTypeViewModel
                    {
                        Id = h.Id,
                        hazardTypeName = h.hazardTypeName
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
                var hazardType = _nemesysRepository.GetHazardTypeById(id);

                if (hazardType == null)
                    return NotFound();
                else
                {
                    var model = new HazardTypeViewModel()
                    {
                        Id = hazardType.Id,
                        hazardTypeName = hazardType.hazardTypeName
                    };
                    return View(model);
                }
            }
            catch (Exception e) {
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
        public IActionResult Create([Bind("hazardTypeName")] HazardType newHazardType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HazardType hazardType = new HazardType()
                    {
                        hazardTypeName = newHazardType.hazardTypeName
                    };

                    _nemesysRepository.AddNewHazardType(hazardType);

                    return RedirectToAction("Index");
                }
                else
                    return View(newHazardType);
            }
            catch (DbUpdateException e)
            {
                SqlException s = e.InnerException as SqlException;
                if (s != null)
                {
                    ModelState.AddModelError(string.Empty, "A database error occured - please contact an admin.");
                }
                return View(newHazardType);
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }

        // Since hazard types dont have a lot of data associated with it, no need to edit

/*
      [HttpGet]
        public IActionResult Edit(int id)
        {
            var existingHazardType = _nemesysRepository.GetHazardTypeById(id);

            if (existingHazardType != null)
            {
                CreateEditHazardTypeViewModel model = new CreateEditHazardTypeViewModel() {
                    hazardTypeName = existingHazardType.hazardTypeName
                };

                return View(model);
            } else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("hazardTypeName")] HazardType updatedHazardType)
        {
            var modelToUpdate = _nemesysRepository.GetHazardTypeById(id);

            if (modelToUpdate == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                modelToUpdate.hazardTypeName = updatedHazardType.hazardTypeName;

                _nemesysRepository.updat
            }
        }
*/
       /* // GET: HazardTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hazardType = await _context.HazardTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hazardType == null)
            {
                return NotFound();
            }

            return View(hazardType);
        }*/

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var hazardType = _nemesysRepository.GetHazardTypeById(id);
                if (hazardType == null)
                    return NotFound();
                if (hazardType.Reports != null)
                    _nemesysRepository.DeleteHazardType(hazardType);
                else
                    return BadRequest("This hazard type has reports connected to it!");

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                _logger.LogError(e, e.Message, e.Data);
                return View("Error");
            }
        }
    }
}
