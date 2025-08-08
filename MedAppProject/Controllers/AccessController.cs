using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MedAppProject.Models;
using MedAppProject.ServicesClasses;
using MedAppProject.ViewMoels;
using MedAppProject.Repositories;
//using MedAppProject.ServicesClasses;

namespace MedAppProject.Controllers
{
    public class AccessController : Controller
    {
        private readonly IMedAppRepository<Patient> _patient;
        private readonly IMedAppRepository<Doctor> _doctor;
        private readonly IMedAppRepository<Lab> _lab;
        private readonly IMedAppRepository<Pharmacist> _pharmacist;
        private readonly IMedAppRepository<Specialization> _specialization;
        private readonly IMedAppRepository<VMLogin> _login;
        private readonly IEmailSender _emailSender;
        //private readonly EmailService _emailService;

        public AccessController(
            IMedAppRepository<VMLogin> login,
            IMedAppRepository<Patient> patient,
            IMedAppRepository<Doctor> doctor,
            IMedAppRepository<Specialization> specialization
,
            IMedAppRepository<Lab> lab,
            IMedAppRepository<Pharmacist> pharmacist
,
            IEmailSender emailSender
            // EmailService emailService
            )
        {
            this._login = login;
            this._patient = patient;
            this._doctor = doctor;
            _specialization = specialization;
            _lab = lab;
            _pharmacist = pharmacist;
            _emailSender = emailSender;
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
        static string GenerateCode()
        {
            Random random = new Random();
            string code = "";
            for (int i = 0; i < 6; i++)
            {
                int digit = random.Next(0, 10);
                code += digit.ToString();
            }
            return code;
        }
        public ActionResult PatientRegister([FromForm]string firstName , [FromForm] string lastName, [FromForm] string mail, [FromForm] string phone, [FromForm] string pass)
        {
            Patient pa= new Patient
            {
                CreatedDate = DateTime.Now,
                FirstName = firstName,
                LastName = lastName,
                Email = mail,
                PhoneNumber = phone

            };
            _patient.Add(pa);
            string hash = BCrypt.Net.BCrypt.HashPassword(pass);
            VMLogin vm = new VMLogin
            {
                Email = mail,
                KeepLoggedIn = true,
                Password = pass,
                RoleId = Role.Patient,
                userId = pa.Id,
                isVerified = false,
                code = GenerateCode()
            };
            _login.Add(vm);
            //string generatedCode = GenerateCode();
            //_emailSender.SendEmailAsync(mail, "Account Verification", vm.code);
            return RedirectToAction("Login", "Access");
        }
        public ActionResult DoctorRegister([FromForm] string firstName, [FromForm] string lastName, [FromForm] string mail, [FromForm] string phone, [FromForm] string pass, [FromForm]string location)
        {
            var a = _specialization.GetAll();
            if(a is null)
            {
                _specialization.Add(new Specialization { Name = "test" });
            }
            var spe = _specialization.GetAll().FirstOrDefault();
            Doctor doc = new Doctor
            {
                CreatedDate = DateTime.Now,
                FirstName = firstName,
                LastName = lastName,
                Email = mail,
                PhoneNumber = phone,
                ClincLocation = location,
                DoctorSpecialization = spe
            };
            _doctor.Add(doc);
            VMLogin vm = new VMLogin
            {
                Email = mail,
                KeepLoggedIn = true,
                Password = pass,
                RoleId = Role.Doctor,
                userId = doc.Id,
                isVerified = false,
                code = GenerateCode()
            };
            _login.Add(vm);
            return RedirectToAction("Login", "Access");
        }
        public ActionResult LabRegister([FromForm] string labName, [FromForm] string mail, [FromForm] string phone, [FromForm] string pass , [FromForm] string location)
        {
            Lab lab = new Lab
            {
                CreatedDate = DateTime.Now,
                FirstName = labName,
                LastName = "Lab",
                Email = mail,
                PhoneNumber = phone,
                LabLocation = location
            };
            _lab.Add(lab);
            VMLogin vm = new VMLogin
            {
                Email = mail,
                KeepLoggedIn = true,
                Password = pass,
                RoleId = Role.Lab,
                userId = lab.Id,
                isVerified = false,
                code = GenerateCode()
            };
            _login.Add(vm);
            return RedirectToAction("Login", "Access");
        }
        public ActionResult PharmacyRegister([FromForm] string Name, [FromForm] string location, [FromForm] string mail, [FromForm] string phone, [FromForm] string pass)
        {
            Pharmacist pha = new Pharmacist
            {
                CreatedDate = DateTime.Now,
                FirstName = Name,
                LastName = "Pharmacy",
                Email = mail,
                PhoneNumber = phone,
                Location = location
            };
            _pharmacist.Add(pha);
            VMLogin vm = new VMLogin
            {
                Email = mail,
                KeepLoggedIn = true,
                Password = pass,
                RoleId = Role.Pharmacy,
                userId = pha.Id,
                isVerified = false,
                code = GenerateCode()
            };
            _login.Add(vm);
            return RedirectToAction("Login", "Access");
        }
        public ActionResult confirmationPage(VMLogin vm)
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult confimationPage(VMLogin vm, [FromForm] string code)
        {
            var vmLog = _login.GetById(vm.Id);
         
            if(code.Equals(vmLog.code))
            {
                vmLog.isVerified=true;
                _login.Update(vmLog);
                return RedirectToAction("Login", "Access");
            }
            else
            {
                ViewData["ValidateMessage"] = "invalid code";
            }
            return RedirectToAction("confirmationPage", "Access", vmLog);
        }

        public ActionResult ViewDataResult()
        {
            var log = _login.GetAll;
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
            _patient.Add(entity);
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
            var isValid = await _login.CheckCredentialsAsync(email, pass);

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
                    var pa = _patient.GetById(isValid.userId);

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
