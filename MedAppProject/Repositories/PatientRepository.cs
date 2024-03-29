﻿using MedAppProject.Data;
using MedAppProject.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAppProject.Repositories
{
    public class PatientRepository : IMedAppRepository<Patient>
    {
        ApplicationDbContext _db;

        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Patient entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public Task<VMLogin> CheckCredentialsAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(Patient entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAll()
        {
            var pa = _db.Patients.Include(l =>l.LabAppointments).ThenInclude(l=>l.Lab).Include(p => p.DoctorAppointments).ThenInclude(d => d.Doctor).ToList();
            return pa;
        }

        public Patient GetById(int id)
        {
            var pa = _db.Patients.Include(t=>t.Tests).Include(m=>m.MedicalRecords).Include(b=>b.Bills).Include(lb=>lb.LabBills).Include(p=>p.Prescription).Include(l=>l.LabAppointments).ThenInclude(a=>a.Lab).Include(p => p.DoctorAppointments).ThenInclude(d=>d.Doctor).ThenInclude(s=>s.DoctorSpecialization).SingleOrDefault(a=>a.Id==id);
            return pa;
        
        }

        public void Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}
