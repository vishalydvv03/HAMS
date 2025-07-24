using HAMS.Domain.Enums;
using HAMS.Domain.Models.DoctorScheduleModels;
using HAMS.Services.DoctorScheduleServices;
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
        public async Task<IActionResult> AddDoctorSchedule([FromBody] AddDoctorSchedule model)
        {
            var added = await service.AddScheduleAsync(model);
            if (!added)
            {
                return BadRequest("Schedule Already Exists");
            }
            return Ok("Schedule Added Succesfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorSchedules()
        {
            var schedules = await service.GetAllDoctorsScheduleAsync();
            return Ok(schedules);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            var schedule = await service.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound("Schedule not found");
            }
            return Ok(schedule);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateScheduleById(int id, [FromBody] AddDoctorSchedule model)
        {
            var updated = await service.UpdateScheduleByIdAsync(id, model);
            if (!updated)
            {
                return NotFound("Schedule not found");
            }
                
            return Ok("Schedule updated successfully");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteScheduleById(int id)
        {
            var deleted = await service.DeleteScheduleByIdAsync(id);
            if (!deleted)
            {
                return NotFound("Schedule not found");
            }
                
            return Ok("Schedule deleted successfully");
        }
    }
}
