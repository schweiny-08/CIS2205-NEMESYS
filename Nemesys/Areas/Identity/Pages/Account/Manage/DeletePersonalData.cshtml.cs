using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nemesys.DAL;
using Nemesys.Models;
using Nemesys.Models.Interfaces;

namespace Nemesys.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly INemesysRepository _nemesysRepository;
        private readonly NemesysContext _nemesysContext;

        public DeletePersonalDataModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            INemesysRepository nemesysRepository,
            NemesysContext nemesysContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _nemesysRepository = nemesysRepository;
            _nemesysContext = nemesysContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            await _userManager.RemoveFromRoleAsync(user, _userManager.GetRolesAsync(user).Result.ToString());

            var reports = _nemesysRepository.GetReportsByOwner(user.Id);
            if (reports != null) {
                foreach (var rep in reports) {
                    rep.User = null;
                    rep.UserId = null;
                    
                    _nemesysContext.Entry(rep).State = EntityState.Modified;
                    //_nemesysContext.SaveChanges();
                }
            }

            var investigations = _nemesysRepository.GetInvestigationsByOwner(user.Id);
            if(investigations != null)
            {
                foreach(var inv in investigations)
                {
                    inv.User = null;
                    inv.UserId = null;

                    _nemesysContext.Entry(inv).State = EntityState.Modified;
                    //_nemesysContext.SaveChanges();
                }
            }

            var result = await _userManager.DeleteAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
