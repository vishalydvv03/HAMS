using HAMS.Domain.Enums;
using HAMS.Domain.Models.DoctorScheduleModels;
using HAMS.Services.DoctorScheduleServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAMS.API.Controllers
{
    [Route("api/doctor/schedule")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IDoctorScheduleService service;

        public SchedulesController(IDoctorScheduleService service)
        {
            this.service = service;
        }


        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetDoctorSchedule(Guid doctorId)
        {
            var data = await service.GetSchedulesByDoctorIdAsync(doctorId);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctorSchedule([FromBody] AddDoctorScheduleModel model)
        {
            var added = await service.AddScheduleAsync(model);
            if (!added)
            {
                return BadRequest("Schedule Already Exists");
            }
            return Ok("Schedule Added Succesfully");
        }

        [HttpPut("{doctorId}")]
        public async Task<IActionResult> UpdateDoctorSchedule(Guid doctorId, [FromQuery] WeekDay day, [FromBody] UpdateDoctorScheduleModel model)
        {
            var updated = await service.UpdateScheduleAsync(doctorId, day, model);
            if (!updated)
            {
                return NotFound("No Such Schedule Exists");
            }
            return Ok("Schedule Updated Succesfully");
        }

        [HttpDelete("{doctorId}")]
        public async Task<IActionResult> DeleteDoctorSchedule(Guid doctorId, [FromQuery] WeekDay day)
        {
            var deleted = await service.DeleteScheduleAsync(doctorId, day);
            if (!deleted)
            {
                return NotFound("No Such Schedule Exists");
            }
            return Ok("Schedule deleted successfully");
        }
    }
}
