using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MedAppProject.Models;
using MedAppProject.ViewMoels;
using MedAppProject.Repositories;

namespace MedAppProject.Controllers
{
    public class AccessController : Controller
    {
        private readonly IMedAppRepository<Patient> patient;
        private readonly IMedAppRepository<Doctor> _doctor;
        private readonly IMedAppRepository<Specialization> _specialization;
        private readonly IMedAppRepository<VMLogin> login;

        public AccessController(
            IMedAppRepository<VMLogin> _login,
            IMedAppRepository<Patient> _patient,
            IMedAppRepository<Doctor> doctor,
            IMedAppRepository<Specialization> specialization = null
        )
        {
            this.login = _login;
            this.patient = _patient;
            this._doctor = doctor;
            _specialization = specialization;
        }

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
                    Patient pa = patient.GetById(isValid.userId);
                    var viewModel = new PatientDashboardViewModel
                    {
                        
                        PatientInfo = pa,
                        
                        Doctors = Enumerable.Empty<Doctor>(),
                        Specializations = _specialization.GetAll().ToList()
                    };

                    return View("~/Views/Patient/Index.cshtml", viewModel);
                }
                else if (isValid.RoleId == Role.Doctor)
                {
                    var doc = _doctor.GetById(isValid.userId);
                    return RedirectToAction("AddDataToSession", "Doctor", doc);
                    //return View("~/Views/Doctor/Index.cshtml", doc);
                }
                else if (isValid.RoleId == Role.Pharmacy)
                {
                    return RedirectToAction("Index", "Pharmacy");
                }
                else if (isValid.RoleId == Role.Lab)
                {
                    return RedirectToAction("Index", "Lab");
                }
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
    }
}
