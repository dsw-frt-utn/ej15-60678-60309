using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Sources
{
    public class PersistenceEf : IPersistence
    {
        private readonly ApplicationDbContext _context;

        public PersistenceEf(ApplicationDbContext context)
        {
            _context = context;
        }

        public Specialty? GetSpecialty(Guid id) => _context.Specialties.Find(id);
        public List<Doctor>? GetActiveDoctors() => _context.Doctors.Include(d => d.Specialty).Where(d => d.IsActive).ToList();
        public Doctor? GetActiveDoctorById(Guid id) => _context.Doctors.Include(d => d.Specialty).FirstOrDefault(d => (d.IsActive) && (d.Id == id));
        public void AddDoctor(Doctor doctor)
        {
            _context.Add(doctor);
            _context.SaveChanges();
        }
        public void UpdateDoctor(Doctor doctor)
        {
            _context.Update(doctor);
            _context.SaveChanges();
        }
    }
}
