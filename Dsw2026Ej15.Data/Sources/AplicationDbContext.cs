using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Sources
{
    public class ApplicationDbContext : DbContext
    {
        //Estas propiedades representan las tablas en mi BD
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
