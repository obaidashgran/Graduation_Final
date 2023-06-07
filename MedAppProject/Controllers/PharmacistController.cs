using Microsoft.AspNetCore.Mvc;
using MedAppProject.Models;
using MedAppProject.ViewModels;
using MedAppProject.Repositories;
using Microsoft.AspNetCore.Http;

namespace MedAppProject.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly IMedAppRepository<Pharmacist> _pharmacist;

        public PharmacistController(IMedAppRepository<Pharmacist> pharmacist)
        {
            _pharmacist = pharmacist;
        }

        public IActionResult AddDataToSession(Pharmacist pharmacist)
        {
            HttpContext.Session.SetInt32("Id", pharmacist.Id);
            // Redirect to another action or return a view
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            Pharmacist pharmacist = _pharmacist.GetById(getId);
            return View(pharmacist);
        }
    }
}
