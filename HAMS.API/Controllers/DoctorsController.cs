using HAMS.Domain.Enums;
using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.DoctorModels;
using HAMS.Domain.Models.DoctorScheduleModels;
using HAMS.Services.AppointmentServices;
using HAMS.Services.DoctorScheduleServices;
using HAMS.Services.DoctorServices;
using HAMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAMS.API.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService service;
        private readonly IDoctorScheduleService scheduleService;

        public DoctorsController(IDoctorService service, IDoctorScheduleService scheduleService)
        {
            this.service = service;
            this.scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<ServiceResult<List<DoctorDetails>>> GetAll()
        {
            return await service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ServiceResult<DoctorDetails>> GetById(Guid id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ServiceResult> Update(Guid id, UpdateDoctor model)
        {
            return await service.UpdateAsync(id, model);
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResult> Delete(Guid id)
        {
            return await service.DeleteAsync(id);
        }

        [HttpGet("{id}/schedules")]
        public async Task<ServiceResult<List<ReadDoctorSchedule>>> GetDoctorSchedule(Guid id)
        {
            return await scheduleService.GetSchedulesByDoctorAsync(id);

        }

        [HttpGet("{doctorId:guid}/appointments")]
        public async Task<ServiceResult<List<ReadAppointmentByDoctor>>> GetAppointments(Guid doctorId)
        {
            return await service.GetAppointmentByDoctorAsync(doctorId);

        }


        [HttpPut("{id}/schedules")]
        public async Task<ServiceResult> UpdateDoctorSchedule(Guid id, [FromQuery] WeekDay day, [FromBody] UpdateDoctorSchedule model)
        {
            return await scheduleService.UpdateScheduleAsync(id, day, model);
        }

        [HttpDelete("{id}/schedules")]
        public async Task<ServiceResult> DeleteDoctorSchedule(Guid id, [FromQuery] WeekDay day)
        {
            return await scheduleService.DeleteScheduleAsync(id, day);
        }
    } 
}
