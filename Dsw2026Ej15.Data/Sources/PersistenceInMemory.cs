using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Sources
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors;
        private List<Specialty> _specialties;

        public PersistenceInMemory()
        {
            _doctors = new List<Doctor>();
            _specialties = new List<Specialty>();
            LoadSpecialties();
        }

        //Al ser las operaciones todas con listas usaremos los metodos de busqueda
        public Specialty? GetSpecialty(Guid id) => _specialties.FirstOrDefault(p => p.Id == id);
        public List<Doctor>? GetActiveDoctors() => _doctors.Where(d => d.IsActive).ToList();
        public Doctor? GetActiveDoctorById(Guid id) => _doctors.FirstOrDefault(d => d.IsActive && d.Id == id);
        public void AddDoctor(Doctor doctor) => _doctors.Add(doctor);
        public void UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = _doctors.FirstOrDefault(d => (d.Id == doctor.Id) && (d is not null));
            existingDoctor?.IsActive = doctor.IsActive;
            existingDoctor?.LicenseNumber = doctor.LicenseNumber;
            existingDoctor?.Name = doctor.Name;
            existingDoctor?.Specialty = doctor.Specialty;
        } 

        private void LoadSpecialties() //Desarmo mi json y obtengo los datos
        {
            try
            {
                var json = File.ReadAllText("specialities.json");
                _specialties = JsonSerializer.Deserialize<List<Specialty>>(json) ?? new List<Specialty>();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"ERROR: Archivo no encontrado, {ex.Message}");
                _specialties = new List<Specialty>();
            }
        }
    }
}
