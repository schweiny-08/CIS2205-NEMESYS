using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nemesys.Models.Interfaces;
using Nemesys.Models.UserModels;
using Nemesys.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nemesys.Controllers
{
    public class AccountController : Controller
    {
        private readonly INemesysRepository _nemesysRepositroy;

        public AccountController(INemesysRepository nemesysRepository)
        {
            _nemesysRepositroy = nemesysRepository;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Login() {
            return View(); 
        }

        [HttpPost]
        public IActionResult Login([Bind("email, password")] LoginViewModel userLogin) {
            if (ModelState.IsValid)
            {
                var user = _nemesysRepositroy.GetReporterByEmail(userLogin.email);

                if (user != null)
                {
                    // User with matching email found
                    var password = user.password;
                    if (password == userLogin.password)
                    {
                        //ViewData["UserID"] = user.idNum;
                        HttpContext.Session.Set(UserSession, user);
                        ViewData["IsLoggedIn"] = true;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        return View(userLogin);
                }
            }
            return View(userLogin);
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        // Binds the input variables from the page to the ViewModel
        public IActionResult Register([Bind("email, password, fName, lName, image")] RegisterAccountViewModel newAccount)
        {
            if (ModelState.IsValid)
            {
                // Adding the image to wwwroot and saving a url in the image's place
                string fileName = "";
                if (newAccount.image != null)
                {
                    //Checking the image file
                    var extention = "." + newAccount.image.FileName.Split('.')[newAccount.image.FileName.Split('.').Length - 1];
                    fileName = Guid.NewGuid().ToString() + extention;
                    var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\profileimages\\" + fileName;
                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        newAccount.image.CopyTo(bits);
                    }
                }

                // Creating instance of Reporter (Person) and adding to db
                // All accounts start out as a reporter. Investigators are given Inv. permission via invitation from another investigator
                Reporter reporter = new Reporter()
                {
                    email = newAccount.email,
                    password = newAccount.password,
                    fName = newAccount.fName,
                    lName = newAccount.lName,
                    image = "/images/profileimages/" + fileName
                };

                _nemesysRepositroy.AddNewReporter(reporter);
                return RedirectToAction("Index", "Home");
            }
            else {
                return View(newAccount);
            }
        }
    }
}