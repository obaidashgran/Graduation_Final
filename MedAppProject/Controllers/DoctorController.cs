using MedAppProject.Models;
using MedAppProject.ViewModels;
using MedAppProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedAppProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IMedAppRepository<Doctor> _doctor;
        private readonly IMedAppRepository<DoctorAvailableTimes> _doctorAvailableTimes;
        private readonly IMedAppRepository<Patient> _patient;
        private readonly IMedAppRepository<Prescription> _prescription;
        private readonly IMedAppRepository<Bill> _bill;
        private readonly IMedAppRepository<MedicalRecord> _record;

        public DoctorController(IMedAppRepository<Doctor> doctor, IMedAppRepository<DoctorAvailableTimes> doctorAvailableTimes, IMedAppRepository<Patient> patient, IMedAppRepository<Prescription> prescription, IMedAppRepository<Bill> bill, IMedAppRepository<MedicalRecord> record)
        {
            _doctor = doctor;
            _doctorAvailableTimes = doctorAvailableTimes;
            _patient = patient;
            _prescription = prescription;
            _bill = bill;
            _record = record;
        }



        // GET: DoctorController
        public ActionResult Index()
        {
            int getId = HttpContext.Session.GetInt32("Id")??0;
            Doctor doc = _doctor.GetById(getId);
            return View(doc);
        }
        public IActionResult AddDataToSession(Doctor doctor)
        {
            HttpContext.Session.SetInt32("Id", doctor.Id);
            // Redirect to another action or return a view
            return RedirectToAction("Index");
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
		public ActionResult MyPatients()
		{
			return View();
		}
		public ActionResult Appointments()
		{
			return View();
		}
        
		public ActionResult AddAvailableTimes(int id)
		{
            var doctor = _doctor.GetById(id);
			return View(doctor);
		}
        [HttpPost]
        public ActionResult AddAvailableTimes([FromForm]string duration , [FromForm]string time)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            Doctor doc = _doctor.GetById(getId);
            DoctorAvailableTimes docAv = new DoctorAvailableTimes { Doctor = doc, Time = DateTime.Parse(time) };
            List<DoctorAvailableTimes> da = _doctorAvailableTimes.GetAll().Where(a => a.Time == DateTime.Parse(time)).ToList();
            if (da.Count==0)
            {
                _doctorAvailableTimes.Add(docAv);
            }
            
            return RedirectToAction("AddAvailableTimes");
        }
        public ActionResult DeleteAvailableTimes(int avId)
        {
            var av = _doctorAvailableTimes.GetById(avId);
            _doctorAvailableTimes.Delete(av);
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            return RedirectToAction("AddAvailableTimes",new {id = getId});
        }
        public ActionResult PatientProfile(int paId)
		{
            var pa = _patient.GetById(paId);
            
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            pa.DoctorAppointments = pa.DoctorAppointments.Where(d => d.Doctor.Id.Equals(getId)).ToList();
            pa.Prescription = pa.Prescription.Where(d => d.Doctor.Id.Equals(getId)).ToList();
            var doc = _doctor.GetById(getId);
            PatientProfileForDoc vm = new PatientProfileForDoc
            {
                Patient = pa,
                Doctor = doc
            };
			return View(vm);
		}
		public ActionResult AddPrescription(int patId)
		{

            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var doc = _doctor.GetById(getId);
            var pa = _patient.GetById(patId);
            PatientProfileForDoc pado = new PatientProfileForDoc
            {
                Doctor = doc,
                Patient = pa
            };
			return View(pado);
		}
		[HttpPost]
        public ActionResult AddPrescription([FromForm] string paId , [FromForm] string Name)
        {
            
            
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var doc = _doctor.GetById(getId);
            var pa = _patient.GetById(int.Parse(paId));
            Prescription pre = new Prescription
            {
                Doctor =doc,
                Patient = pa,
                MedName = Name,
                Location = "ramtha"
            };
            _prescription.Add(pre);
            return RedirectToAction("PatientProfile",new {paId=int.Parse(paId)});
        }
        public ActionResult AddBill(int patId)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var doc = _doctor.GetById(getId);
            var pa = _patient.GetById(patId);
            PatientProfileForDoc pado = new PatientProfileForDoc
            {
                Doctor = doc,
                Patient = pa
            };
            return View(pado);
        }
        [HttpPost]
        public ActionResult AddBill([FromForm] string paId , [FromForm] string amount)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var doc = _doctor.GetById(getId);
            var pa = _patient.GetById(int.Parse(paId));
            Bill bill = new Bill
            {
                Doctor=doc,
                Patient = pa,
                PaidOn = DateTime.Now.Date,
                Amount = double.Parse(amount)
            };
            _bill.Add(bill);
            return RedirectToAction("PatientProfile", new { paId = int.Parse(paId) });
        }
        [HttpPost]
        public ActionResult AddMedicalRecords([FromForm] string descreption , [FromForm] string date , [FromForm] string paId)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var doc = _doctor.GetById(getId);
            var pa = _patient.GetById(int.Parse(paId));
            MedicalRecord record = new MedicalRecord
            {
                Descreption = descreption,
                Date = DateTime.Parse(date),
                Doctor = doc,
                Patient = pa
            };
            _record.Add(record);
            return RedirectToAction("PatientProfile", new { paId = int.Parse(paId) });
        }
        // GET: DoctorController/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult spesificDate([FromForm]string date, Doctor model)
        {
            var doc = _doctor.GetAll();
            
            return View("~/Views/Doctor/TestPage.cshtml");
        }

        // POST: DoctorController/Create
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

        // GET: DoctorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoctorController/Edit/5
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

        // GET: DoctorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoctorController/Delete/5
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
