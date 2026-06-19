using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Specialty(Guid id, string name, string description) : base(id)
        {
           
            Name = name;
            Description = description;
        }
    }
}
