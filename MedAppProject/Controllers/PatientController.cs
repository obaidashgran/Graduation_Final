using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedAppProject.ViewMoels;
using MedAppProject.Repositories;
using MedAppProject.Models;
using MedAppProject.ViewModels;
using MedAppProject.ServicesClasses;

namespace MedAppProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly IMedAppRepository<Specialization> _specialization;
        private readonly IMedAppRepository<Doctor> _doctor;
        private readonly IMedAppRepository<Patient> _patient;
        private readonly IMedAppRepository<DoctorAppointment> _docAppointment;
        private readonly IMedAppRepository<LabAppointment> _labAppointment;
        private readonly IMedAppRepository<DoctorAvailableTimes> _docAvilableTime;
        private readonly IMedAppRepository<LabAvailableTimes> _labAvilableTime;
        private readonly IMedAppRepository<Lab> _lab;
        private readonly IMedAppRepository<Test> _test;
        private readonly IEmailSender _emailSender;



        public PatientController(IMedAppRepository<Specialization> specialization, IMedAppRepository<Doctor> doctor,
            IMedAppRepository<DoctorAppointment> docAppointment, IMedAppRepository<Patient> patient,
            IMedAppRepository<DoctorAvailableTimes> docAvilableTime, IMedAppRepository<Lab> lab, IMedAppRepository<LabAvailableTimes> labAvilableTime, IMedAppRepository<LabAppointment> labAppointment, IMedAppRepository<Test> test, IEmailSender emailSender)
        {
            _specialization = specialization;
            _doctor = doctor;
            _docAppointment = docAppointment;
            _patient = patient;
            _docAvilableTime = docAvilableTime;
            _lab = lab;
            _labAvilableTime = labAvilableTime;
            _labAppointment = labAppointment;
            _test = test;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> SendEmail()
        {
            // Create email content
            var receiver = "smabdullah19@cit.just.edu.jo";
            var subject = "Hello!";
            var body = "Hi i'm obaida from MedApp company!";

            // Send the email
            await _emailSender.SendEmailAsync(receiver, subject, body);

            return View();
        }

        // GET: PatientController
        public IActionResult AddDataToSession(Patient patient)
        {
            HttpContext.Session.SetInt32("Id", patient.Id);
            // Redirect to another action or return a view
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var pa = _patient.GetById(getId);
            PatientDashboardViewModel pd = new PatientDashboardViewModel
            {
                PatientInfo = pa,
                Specializations = _specialization.GetAll().ToList(),

            };
            return View(pd);
        }
        public ActionResult LabProfile(int labId)
        {
            var lab = _lab.GetById(labId);
            return View(lab);
        }
        public ActionResult LabSearch()
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var pa = _patient.GetById(getId);
            return View(pa);
        }
        [HttpPost]
        public ActionResult SearchForLabs([FromForm] string city , [FromForm] string? testName)
        {

            List<Lab> la = _lab.GetAll().Where(l=>l.LabLocation.Equals(city)).ToList();
            if (testName != null)
            {
                la = la.Where(l => l.TestsInfo.Any(a=>a.Name.Equals(testName))).ToList();
            }
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            LabDashboardViewModel labs = new LabDashboardViewModel
            {
                Labs = la,
                Patient = _patient.GetById(getId)

            };
            return View("Views/Patient/SearchLabResult.cshtml", labs);
        }
        public ActionResult SearchLabResult(LabDashboardViewModel lab)
        {
            return View(lab);
        }
        
		public ActionResult BookAppointment(int docId)
		{
            var doc = _doctor.GetById(docId);
            return View(doc);
		}
		public ActionResult DoctorProfile(int docId)
        {
            var doc = _doctor.GetById(docId);
            return View(doc);
        }
		public ActionResult PatientProfile()
		{
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var pa = _patient.GetById(getId);
            DateTime today = DateTime.Today;

            pa.DoctorAppointments = pa.DoctorAppointments.OrderBy(x =>
            {
                if (x.bookTime.Date >= today)
                {
                    return x.bookTime.Date - today;
                }
                else
                {
                    return today - x.bookTime.Date;
                }
            }).ToList();

            return View(pa);
		}
		[HttpPost]
        public ActionResult SearchForDoctors(PatientDashboardViewModel model , [FromForm] string? searchBox ,
            [FromForm] string? city, [FromForm] string sort, [FromForm] string? date)

        {
            var modelSort = model;
            modelSort.Specializations = _specialization.GetAll().ToList();
            if (searchBox != null)
            {
                modelSort.Doctors = _doctor.GetAll().Where(x => x.DoctorSpecialization.Id.Equals(modelSort.SelectedSpecialization.Id)
                && (x.FirstName.StartsWith(searchBox))).ToList();
            }

            else
            {
                
                modelSort.Doctors = _doctor.GetAll().Where(x => x.DoctorSpecialization.Id.Equals(modelSort.SelectedSpecialization.Id)).ToList();
            }
                
            if (city != null)
            {
                modelSort.Doctors = modelSort.Doctors.Where(x => x.ClincLocation.Equals(city));
            }
            if (sort.ToLower().Equals("price"))
            {
                var doct = modelSort.Doctors.ToList();
                doct.Sort((a, b) => a.Fees.CompareTo(b.Fees));
                modelSort.Doctors = doct;
            }
            else if (sort.ToLower().Equals("rate"))
            {
                var doct = modelSort.Doctors.ToList();
                doct.Sort((a, b) => b.DoctorRate.CompareTo(a.DoctorRate));
                modelSort.Doctors = doct;
            }
            ViewBag.selectedDate = DateTime.Now.Date;
            if(date!=null)
            {   
                modelSort.Doctors = modelSort.Doctors.Where(d => d.AvailableTime.Any(a => a.Time.Date.Equals(DateTime.Parse(date).Date))).ToList();
                ViewBag.selectedDate = date;
            }
            else
            {
                modelSort.Doctors = modelSort.Doctors.Where(d => d.AvailableTime.Any(a => a.Time.Date>=DateTime.Now.Date)).ToList();
                
            }
            


            return View("~/Views/Patient/SearchDResult.cshtml", modelSort);
        }
        [HttpPost]
        public ActionResult SearchForDoctorsBySpecializations(PatientDashboardViewModel model, [FromForm] string spe)
        {
            model.Specializations = _specialization.GetAll().ToList();
           // model.SelectedSpecialization = new Specialization { Id = 1, Name = "dentistry" };
            model.Doctors = _doctor.GetAll().Where(x => x.DoctorSpecialization.Id.Equals(int.Parse(spe))).ToList();
            return View("~/Views/Patient/SearchDResult.cshtml", model);
        }
        
        public ActionResult bookDoctor(int docId , DateTime time , int avId)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var pa = _patient.GetById(getId);
            var doc = _doctor.GetById(docId);
            var pa2 = pa.DoctorAppointments.SingleOrDefault(a => a.bookTime == time);
            var pa3 = pa.LabAppointments.SingleOrDefault(a => a.bookTime == time);
            
            
            if (pa2 == null && pa3==null)
            {
                DoctorAppointment docApp = new DoctorAppointment
                {
                    patient = pa,
                    Doctor = doc,
                    bookTime = time
                };
                _docAppointment.Add(docApp);
                var av = _docAvilableTime.GetById(avId);
                _docAvilableTime.Delete(av);
            }
            PatientDashboardViewModel model= new PatientDashboardViewModel();

            model.Specializations = _specialization.GetAll().ToList();
            model.PatientInfo = pa;
            return View("~/Views/Patient/Index.cshtml", model);
        }

        public ActionResult bookLab(int labId, DateTime time, int avId)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var pa = _patient.GetById(getId);
            var lab = _lab.GetById(labId);
            var pa2 = pa.DoctorAppointments.SingleOrDefault(a => a.bookTime == time);
            var pa3 = pa.LabAppointments.SingleOrDefault(a => a.bookTime == time);


            if (pa2 == null && pa3 == null)
            {
                LabAppointment labApp = new LabAppointment
                {
                    patient = pa,
                    Lab = lab,
                    bookTime = time
                };
                _labAppointment.Add(labApp);
                var av = _labAvilableTime.GetById(avId);
                _labAvilableTime.Delete(av);
            }
            PatientDashboardViewModel model = new PatientDashboardViewModel();

            model.Specializations = _specialization.GetAll().ToList();
            model.PatientInfo = pa;
            return View("~/Views/Patient/Index.cshtml", model);
        }


        [HttpPost]
        public ActionResult MakeAppointment([FromForm] string patient, [FromForm] string appointment ,
            [FromForm] string id ,PatientDashboardViewModel model)
        {
            Patient patientChek = _patient.GetById(int.Parse(patient));
            DateTime times = _docAvilableTime.GetById(int.Parse(appointment)).Time;
            var av = patientChek.DoctorAppointments.SingleOrDefault(a=>a.bookTime.Equals(times));
            ViewData["AppointmentMsg"] = null;
            if (av == null)
            {
                //create DocApp obj to add 
                DoctorAppointment doctopApp = new DoctorAppointment()
                {
                    patient = _patient.GetById(int.Parse(patient)),
                    bookTime = _docAvilableTime.GetById(int.Parse(appointment)).Time,
                    Doctor = _doctor.GetById(int.Parse(id))

                };


                //remove from available time
                var dv = _docAvilableTime.GetById(int.Parse(appointment));
                _docAvilableTime.Delete(dv);
                //add to docApp
                _docAppointment.Add(doctopApp);
            }
            else
            {
                ViewData["AppointmentMsg"] = "You have an appointment in this time , please try again!";
            }
            
            model.Specializations= _specialization.GetAll().ToList();
            model.PatientInfo = patientChek;
            return View("~/Views/Patient/Index.cshtml", model);

        }
        [HttpPost]
        public ActionResult MakeLabAppointment([FromForm] string patient, [FromForm] string appointment,
            [FromForm] string id)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            Patient patientChek = _patient.GetById(getId);
            DateTime times = _labAvilableTime.GetById(int.Parse(appointment)).Time;
            var av = patientChek.LabAppointments.SingleOrDefault(a => a.bookTime.Equals(times));
            ViewData["AppointmentMsg"] = null;
            if (av == null)
            {
                //create LabApp obj to add 
                LabAppointment labApp = new LabAppointment()
                {
                    patient = _patient.GetById(getId),
                    bookTime = _labAvilableTime.GetById(int.Parse(appointment)).Time,
                    Lab = _lab.GetById(int.Parse(id))

                };


                //remove from available time
                var dv = _labAvilableTime.GetById(int.Parse(appointment));
                _labAvilableTime.Delete(dv);
                //add to docApp
                _labAppointment.Add(labApp);
            }
            else
            {
                ViewData["AppointmentMsg"] = "You have an appointment in this time , please try again!";
            }

            return View("~/Views/Patient/LabSearch.cshtml", patientChek);
        }
        

        public IActionResult DownloadFile(int id)
        {
            var test = _test.GetById(id);
            if (test == null || test.ResultFile == null)
            {
                // Handle case where the test or the file does not exist
                return NotFound();
            }

            // Return the file as a FileResult
            return File(test.ResultFile, "application/octet-stream");
        }
        public IActionResult DeleteAppointment(int apId,int docId)
		{
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;

            var ap = _docAppointment.GetById(apId);
            var doc = _doctor.GetById(docId);

            var av = new DoctorAvailableTimes
            {
                Doctor = doc,
                Time = ap.bookTime
            };
            _docAppointment.Delete(ap);
            _docAvilableTime.Add(av);
            return RedirectToAction("PatientProfile", "Patient");
		}
        // GET: PatientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
