using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.MedicalRecordModels;
using HAMS.Domain.Models.PatientModels;
using HAMS.Services.AppointmentServices;
using HAMS.Services.MedicalRecordServices;
using HAMS.Services.PatientServices;
using HAMS.Utility;
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
        public async Task<ServiceResult<List<ReadPatient>>> GetAll()
        {
            return await patientService.GetAllAsync();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ServiceResult<ReadPatient>> GetById(Guid id)
        {
            return await patientService.GetByIdAsync(id);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ServiceResult> Update(Guid id, UpdatePatient model)
        {
            return await patientService.UpdateAsync(id, model);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ServiceResult> Delete(Guid id)
        {
            return await patientService.DeleteAsync(id);
        }

        [HttpGet("{patientId:guid}/appointments")]
        public async Task<ServiceResult<List<ReadAppointmentByPatient>>> GetAllAppointmentsByPatient(Guid patientId)
        {
            return await appointmentService.GetAppointmentByPatientAsync(patientId);
        }

        [HttpGet("{patientId:guid}/medical-records")]
        public async Task<ServiceResult<List<ReadMedicalRecordByPatient>>> GetMedicalRecords(Guid patientId)
        {
            return await recordService.GetRecordsForPatientAsync(patientId);
        }

        [HttpGet("search")]
        public async Task<ServiceResult<List<ReadPatient>>> SearchPatients([FromQuery] string? name, [FromQuery] string? email, [FromQuery] string? mobile)
        {
            return await patientService.SearchPatientsAsync(name, email, mobile);
        }
    }

}
