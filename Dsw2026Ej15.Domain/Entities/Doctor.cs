using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Specialty Specialty { get; set; }

        public Doctor(Guid id, string name, string licenseNumber, Specialty specialty) : base(id)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            IsActive = true;
            Specialty = specialty;
        }
    }
}
