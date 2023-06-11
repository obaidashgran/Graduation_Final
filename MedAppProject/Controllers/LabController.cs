using Microsoft.AspNetCore.Mvc;
using MedAppProject.Models;
using MedAppProject.ViewModels;
using MedAppProject.Repositories;
using Microsoft.AspNetCore.Http;
namespace MedAppProject.Controllers
{
    public class LabController : Controller
    {
        private readonly IMedAppRepository<Lab> _lab;
        private readonly IMedAppRepository<Patient> _patient;
        private readonly IMedAppRepository<LabBills> _labBill;
        private readonly IMedAppRepository<Test> _test;
        private readonly IMedAppRepository<TestInfo> _testInfo;

		public LabController(IMedAppRepository<Lab> lab, IMedAppRepository<Patient> patient, IMedAppRepository<LabBills> labBill, IMedAppRepository<Test> test, IMedAppRepository<TestInfo> testInfo)
		{
			_lab = lab;
			_patient = patient;
			_labBill = labBill;
			_test = test;
			_testInfo = testInfo;
		}

		public IActionResult AddDataToSession(Lab lab)
        {
            HttpContext.Session.SetInt32("Id", lab.Id);
            // Redirect to another action or return a view
            return RedirectToAction("index");
        }
        public IActionResult Index()
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            Lab lab = _lab.GetById(getId);
            return View(lab);
        }
        public ActionResult PatientProfile(int paId)
        {
            var pa = _patient.GetById(paId);

            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            pa.LabAppointments = pa.LabAppointments.Where(p=>p.Lab.Id.Equals(getId)).ToList();
            //pa.Prescription = pa.Prescription.Where(d => d.Doctor.Id.Equals(getId)).ToList();
            var lab = _lab.GetById(getId);
			PatientProfileForLabViewModel vm = new PatientProfileForLabViewModel
            {
				Patient = pa,
				Lab = lab
			};
			return View(vm);
        }
        public ActionResult AddBill(int patId)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var lab = _lab.GetById(getId);
            var pa = _patient.GetById(patId);
			PatientProfileForLabViewModel pado = new PatientProfileForLabViewModel
			{
				Lab = lab,
				Patient = pa
			};
			return View(pado);
        }
        [HttpPost]
        public ActionResult AddBill([FromForm] string paId, [FromForm] string amount)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var lab = _lab.GetById(getId);
            var pa = _patient.GetById(int.Parse(paId));
            LabBills bill = new LabBills
            {
                Lab = lab,
                Patient = pa,
                PaidOn = DateTime.Now.Date,
                Amount = double.Parse(amount)
            };
            _labBill.Add(bill);
            return RedirectToAction("PatientProfile", new { paId = int.Parse(paId) });
        }

        [HttpPost]
        public ActionResult AddTestResult([FromForm] string descreption, [FromForm] string date, [FromForm] string paId, [FromForm] IFormFile file)
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            var lab = _lab.GetById(getId);
            var pa = _patient.GetById(int.Parse(paId));
            //var testInfo = _testInfo.GetAll();
            byte[] fileData = null;
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    fileData = memoryStream.ToArray();
                }
            }
            Test test = new Test
            {
                Description = descreption,
                Date = DateTime.Parse(date),
                Lab = lab,
                Patient = pa,
                ResultFile = fileData,
                //TestInfo = testInfo

            };
            _test.Add(test);
            return RedirectToAction("PatientProfile", new { paId = int.Parse(paId) });
        }
    }
}
