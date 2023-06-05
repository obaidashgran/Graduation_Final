using MedAppProject.Repositories;
using MedAppProject.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MedAppProject.AutoRunningClasses
{
    public class AppointmentCleanupService : BackgroundService
    {
        private readonly IMedAppRepository<DoctorAppointment> _doctorAppointments;
        private readonly TimeSpan _checkInterval;

        public AppointmentCleanupService(IMedAppRepository<DoctorAppointment> doctorAppointments, TimeSpan checkInterval)
        {
            _doctorAppointments = doctorAppointments;
            _checkInterval = checkInterval;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CleanupAppointments();

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private void CleanupAppointments()
        {
            DateTime dayBeforeNow = DateTime.Now.AddHours(-24);
            var appointments = _doctorAppointments.GetAll().Where(x => x.bookTime < dayBeforeNow).ToList();

            foreach (var appointment in appointments)
            {
                _doctorAppointments.Delete(appointment);
            }
        }
    }
}
