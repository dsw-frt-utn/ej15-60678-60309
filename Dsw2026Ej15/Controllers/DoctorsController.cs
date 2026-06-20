using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Entities;


namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistencia;
        public DoctorsController(IPersistence persistence)
        {
            _persistencia = persistence;
        }

        //PRIMER ENDPOINT -> POST
        [HttpPost]
        public IActionResult CreateDoctor([FromBody] CreateDoctorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name)) throw new ValidationException("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber)) throw new ValidationException("El numero de licencia es requerido");

            var specialty = _persistencia.GetSpecialty(request.SpecialtyId);

            if (specialty is null) throw new ValidationException("La especialidad ingresada no existe");

            Doctor newDoctor = new Doctor(Guid.NewGuid(), request.Name, request.LicenseNumber, specialty);

            return Created($"/api/doctors/{newDoctor.Id}", newDoctor);
        }



        //SEGUNDO ENDPOINT -> GET
        [HttpGet]
        public IActionResult GetActiveDoctors() => Ok(_persistencia.GetActiveDoctors());

        //TERCER ENDPOINT -> GET by Id
        [HttpGet("{id}")]
        public IActionResult GetDoctorById(Guid id)
        {
            Doctor? doctor = _persistencia.GetActiveDoctorById(id);
            if (doctor is null) return NotFound(new { message = "Medico no encontrado o no esta activo" });
            var respuesta = new { DoctorName = doctor.Name, DoctorLicense = doctor.LicenseNumber, DoctorSpecialtyName = doctor.Specialty.Name};
            return Ok(respuesta);
        }


        //CUARTO ENDPOINT -> DELETE by Id
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctorById(Guid id)
        {
            Doctor? doctor = _persistencia.GetActiveDoctorById(id);
            if (doctor is null) return NotFound(new { message = "Medico no encontrado o no esta activo" });
            doctor.IsActive = false;
            _persistencia.UpdateDoctor(doctor);
            return NoContent();
        }

        //REQUEST
        public class CreateDoctorRequest
        {
            public string Name { get; set; } = string.Empty;
            public string LicenseNumber { get; set; } = string.Empty;
            public Guid SpecialtyId { get; set; }
        }
    }
}
