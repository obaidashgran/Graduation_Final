using MedAppProject.Models;
using MedAppProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedAppProject.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IMedAppRepository<Doctor> _doctor;

        public DoctorController(IMedAppRepository<Doctor> doctor)
        {
            _doctor = doctor;
        }



        // GET: DoctorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DoctorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
