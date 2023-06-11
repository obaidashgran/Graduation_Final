﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MedAppProject.Models;
using MedAppProject.ViewMoels;
using MedAppProject.Repositories;
//using MedAppProject.ServicesClasses;

namespace MedAppProject.Controllers
{
    public class AccessController : Controller
    {
        private readonly IMedAppRepository<Patient> patient;
        private readonly IMedAppRepository<Doctor> _doctor;
        private readonly IMedAppRepository<Lab> _lab;
        private readonly IMedAppRepository<Pharmacist> _pharmacist;
        private readonly IMedAppRepository<Specialization> _specialization;
        private readonly IMedAppRepository<VMLogin> login;
        //private readonly EmailService _emailService;

        public AccessController(
            IMedAppRepository<VMLogin> _login,
            IMedAppRepository<Patient> _patient,
            IMedAppRepository<Doctor> doctor,
            IMedAppRepository<Specialization> specialization
,
            IMedAppRepository<Lab> lab,
            IMedAppRepository<Pharmacist> pharmacist
           // EmailService emailService
            )
        {
            this.login = _login;
            this.patient = _patient;
            this._doctor = doctor;
            _specialization = specialization;
            _lab = lab;
            _pharmacist = pharmacist;
            //_emailService = emailService;
        }
        //public IActionResult SendEmail()
        //{
        //    // Create email content
        //    var recipient = "obaida.shgran09@gmail.com";
        //    var subject = "Hello!";
        //    var body = "Hi i'm obaida from MedApp company!";

        //    // Send the email
        //    _emailService.SendEmail(recipient, subject, body);

        //    return View();
        //}

        public ActionResult ViewDataResult()
        {
            var log = login.GetAll;
            return View(log);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Patient entity)
        {
            entity.CreatedDate = DateTime.Now;
            patient.Add(entity);
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
            var email = modelLogin.Email;
            var pass = modelLogin.Password;
            var isValid = await login.CheckCredentialsAsync(email, pass);

            if (isValid != null)
            {
                List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier , modelLogin.Email),
                    new Claim("OtherProperties","Example Role")
                    };

                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                if (isValid.RoleId == Role.Admin)
                {
                    // return RedirectToAction()
                }
                else if (isValid.RoleId == Role.Patient)
                {
                    var pa = patient.GetById(isValid.userId);

                    //Patient pa = patient.GetById(isValid.userId);
                    //var viewModel = new PatientDashboardViewModel
                    //{
                        
                    //    PatientInfo = pa,
                        
                    //    Doctors = Enumerable.Empty<Doctor>(),
                    //    Specializations = _specialization.GetAll().ToList()
                    //};

                    return RedirectToAction("AddDataToSession", "Patient" ,pa);
                }
                else if (isValid.RoleId == Role.Doctor)
                {
                    var doc = _doctor.GetById(isValid.userId);
                    return RedirectToAction("AddDataToSession", "Doctor", doc);
                    //return View("~/Views/Doctor/Index.cshtml", doc);
                }
                else if (isValid.RoleId == Role.Pharmacy)
                {
                    var pha = _pharmacist.GetById(isValid.userId);
                    return RedirectToAction("AddDataToSession", "Pharmacist",pha);
                }
                else if (isValid.RoleId == Role.Lab)
                {
                    var lab = _lab.GetById(isValid.userId);
                    return RedirectToAction("AddDataToSession", "Lab", lab);
                }
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
