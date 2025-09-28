using HAMS.Domain.Enums;
using HAMS.Domain.Models.DoctorScheduleModels;
using HAMS.Services.DoctorScheduleServices;
using HAMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAMS.API.Controllers
{
    [Route("api/doctor-schedules")]
    [ApiController]
    public class DoctorSchedulesController : ControllerBase
    {
        private readonly IDoctorScheduleService service;

        public DoctorSchedulesController(IDoctorScheduleService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ServiceResult> AddDoctorSchedule([FromBody] AddDoctorSchedule model)
        {
            return await service.AddScheduleAsync(model);
        }

        [HttpGet]
        public async Task<ServiceResult<List<ReadDoctorSchedule>>> GetAllDoctorSchedules()
        {
            return await service.GetAllDoctorsScheduleAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResult<ReadDoctorSchedule>> GetScheduleById(int id)
        {
            return await service.GetScheduleByIdAsync(id);

        }


        [HttpPut("{id:int}")]
        public async Task<ServiceResult> UpdateScheduleById(int id, [FromBody] AddDoctorSchedule model)
        {
            return await service.UpdateScheduleByIdAsync(id, model);
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResult> DeleteScheduleById(int id)
        {
            return await service.DeleteScheduleByIdAsync(id);
        }
    }
}
