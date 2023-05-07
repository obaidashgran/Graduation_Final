using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MedAppProject.ViewMoels;
using MedAppProject.Repositories;
using MedAppProject.Models;

namespace MedAppProject.Controllers
{
    public class PatientController : Controller
    {
        private readonly IMedAppRepository<Specialization> _specialization;
        private readonly IMedAppRepository<Doctor> _doctor;
        private readonly IMedAppRepository<Patient> _patient;
        private readonly IMedAppRepository<DoctorAppointment> _docAppointment;
        private readonly IMedAppRepository<DoctorAvailableTimes> _docAvilableTime;



        public PatientController(IMedAppRepository<Specialization> specialization, IMedAppRepository<Doctor> doctor,
            IMedAppRepository<DoctorAppointment> docAppointment, IMedAppRepository<Patient> patient,
            IMedAppRepository<DoctorAvailableTimes> docAvilableTime)
        {
            _specialization = specialization;
            _doctor = doctor;
            _docAppointment = docAppointment;
            _patient = patient;
            _docAvilableTime = docAvilableTime;
        }

        // GET: PatientController
        public ActionResult Index()
        {
            return View();
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
                modelSort.Doctors = modelSort.Doctors.Where(d => d.AvailableTime.Any(a => a.Time.Date.Equals(DateTime.Now.Date))).ToList();
                
            }


            return View("~/Views/Patient/Index.cshtml", modelSort);
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
                _docAvilableTime.Delete(int.Parse(appointment));
                //add to docApp
                _docAppointment.Add(doctopApp);
            }
            else
            {
                ViewData["AppointmentMsg"] = "You have an appointment in this time , please try again!";
            }
            
            model.Specializations= _specialization.GetAll().ToList();
            return View("~/Views/Patient/Index.cshtml", model);

        }
        public ActionResult DoctorProfile()
        {
            string docId = Request.Query["docId"];
           var doc =  _doctor.GetById(int.Parse(docId));
            return View(doc);
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
