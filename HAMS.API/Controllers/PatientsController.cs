using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.PatientModels;
using HAMS.Services.AppointmentServices;
using HAMS.Services.MedicalRecordServices;
using HAMS.Services.PatientServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAMS.API.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IAppointmentService appointmentService;
        private readonly IMedicalRecordService recordService;

        public PatientsController(IPatientService patientService, IAppointmentService appointmentService, IMedicalRecordService recordService)
        {
            this.patientService = patientService;
            this.appointmentService = appointmentService;
            this.recordService = recordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await patientService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await patientService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound("Patient not found");
            }
            return Ok(data);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdatePatient model)
        {
            var updated = await patientService.UpdateAsync(id, model);
            if (!updated) 
            {
                return NotFound("Patient not found");
            } 
            return Ok("Patient updated successfully");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await patientService.DeleteAsync(id);
            if (!deleted) 
            {
                return NotFound("Patient not found");
            }
            return Ok("Patient deleted successfully");
        }

        [HttpGet("{patientId:guid}/appointments")]
        public async Task<IActionResult> GetAllAppointmentsByPatient(Guid patientId)
        {
            var data = await appointmentService.GetAppointmentByPatientAsync(patientId);
            return Ok(data);
        }

        [HttpGet("{patientId:guid}/medical-records")]
        public async Task<IActionResult> GetMedicalRecords(Guid patientId)
        {
            var data = await recordService.GetRecordsForPatientAsync(patientId);
            return Ok(data);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPatients([FromQuery] string? name, [FromQuery] string? email, [FromQuery] string? mobile)
        {
            var result = await patientService.SearchPatientsAsync(name, email, mobile);
            return Ok(result);
        }
    }

}
