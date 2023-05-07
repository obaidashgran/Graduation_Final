﻿using MedAppProject.Models;
using MedAppProject.Data;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
    public class DoctorAppointmentRepository : IMedAppRepository<DoctorAppointment>
    {
        ApplicationDbContext _db;

        public DoctorAppointmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(DoctorAppointment entity)
        {
            throw new NotImplementedException();
        }

        public Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DoctorAppointment> GetAll()
        {
            var ap = _db.DoctorAppointments.Include(a => a.patient).Include(a => a.Doctor).ToList();
            return ap;
        }

        public DoctorAppointment GetById(int id)
        {
            var ap = _db.DoctorAppointments.Include(a => a.patient).Include(a => a.Doctor).SingleOrDefault(x => x.Id == id);
            return ap;
        }

        public void Update(DoctorAppointment entity)
        {
            throw new NotImplementedException();
        }
    }
}