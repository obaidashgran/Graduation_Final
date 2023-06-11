using MedAppProject.Models;
using MedAppProject.Repositories;
namespace MedAppProject.ServicesClasses
{
    public class MyBackgroundService : BackgroundService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmailSender _emailSender;
        public MyBackgroundService(ILogger<MyBackgroundService> logger, IServiceProvider serviceProvider, IEmailSender emailSender)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _emailSender = emailSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Hi obaida");

                
                
                using(var scope = _serviceProvider.CreateScope())
                {
                    var docAvailableTimesRepository = scope.ServiceProvider.GetRequiredService<IMedAppRepository<DoctorAvailableTimes>>();
                    var labAvailableTimesRepository = scope.ServiceProvider.GetRequiredService<IMedAppRepository<LabAvailableTimes>>();
                    var docAppointment = scope.ServiceProvider.GetRequiredService<IMedAppRepository<DoctorAppointment>>();
                    var labAppointment = scope.ServiceProvider.GetRequiredService<IMedAppRepository<LabAppointment>>();

                    var lastAv = docAvailableTimesRepository.GetAll().Where(a => a.Time.Date < DateTime.Now.Date);
                    var lastAvLab = labAvailableTimesRepository.GetAll().Where(a => a.Time.Date < DateTime.Now.Date);
                    var docApp = docAppointment.GetAll().Where(a => a.bookTime.Date == DateTime.Now.Date && a.bookTime.AddHours(-1)<=DateTime.Now && a.bookTime>DateTime.Now);

                    string emailSubject = "Appointment Reminder";

                    foreach (var a in docApp)
                    {
                        string emailBody = $"Dear {a.patient.FirstName},\n" +
                   $"Just a quick reminder of your upcoming appointment at MedApp\n\n" +
                   $"Appointment Details:\n" +
                   $"Date: {a.bookTime.ToString("d MMMM yyyy")}\n" +
                   $"Time: {a.bookTime.ToString("HH:mm")}\n" +
                   $"Doctor Name: Dr. {a.Doctor.FirstName}\n\n" +
                   $"Please arrive a few minutes early to ensure a prompt start. If you need to cancel or reschedule, kindly let us know at least 24 hours in advance.\n" +
                   $"Should you have any questions, feel free to contact us at medappointment2023@gmail.com\n\n" +
                   $"Looking forward to seeing you soon!\n\n" +
                   $"Best regards,\n\n" +
                   $"MedApp Company";


                        await _emailSender.SendEmailAsync(a.patient.Email, emailSubject, emailBody);
                    }

                    foreach (var a in lastAv)
                    {
                        docAvailableTimesRepository.Delete(a);
                    }
                    foreach (var a in lastAvLab)
                    {
                        labAvailableTimesRepository.Delete(a);
                    }
                    
                }
                

                await Task.Delay(TimeSpan.FromHours(1),stoppingToken);
            }
        }
    }
}
