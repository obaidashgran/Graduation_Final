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

        public LabController(IMedAppRepository<Lab> lab)
        {
            _lab = lab;
        }

        public IActionResult AddDataToSession(Lab lab)
        {
            HttpContext.Session.SetInt32("Id", lab.Id);
            // Redirect to another action or return a view
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            int getId = HttpContext.Session.GetInt32("Id") ?? 0;
            Lab lab = _lab.GetById(getId);
            return View(lab);
        }
    }
}
