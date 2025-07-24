using HAMS.Domain.Enums;
using HAMS.Domain.Models.DoctorModels;
using HAMS.Domain.Models.DoctorScheduleModels;
using HAMS.Services.AppointmentServices;
using HAMS.Services.DoctorScheduleServices;
using HAMS.Services.DoctorServices;
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
        public async Task<IActionResult> GetAll()
        {
            var data = await service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await service.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound("Doctor not found");
            }
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateDoctor model)
        {
            var updated = await service.UpdateAsync(id, model);
            if (!updated)
            {
                return NotFound("Doctor not found");
            }
            return Ok("Doctor updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await service.DeleteAsync(id);
            {
                if (!deleted)
                {
                    return NotFound("Doctor not found");
                }
                return Ok("Doctor deleted successfully");
            }
        }

        [HttpGet("{id}/schedules")]
        public async Task<IActionResult> GetDoctorSchedule(Guid id)
        {
            var data = await scheduleService.GetSchedulesByDoctorAsync(id);
            return Ok(data);
        }

        [HttpGet("{doctorId:guid}/appointments")]
        public async Task<IActionResult> GetAppointments(Guid doctorId)
        {
            var data = await service.GetAppointmentByDoctorAsync(doctorId);
            return Ok(data);
        }


        [HttpPut("{id}/schedules")]
        public async Task<IActionResult> UpdateDoctorSchedule(Guid id, [FromQuery] WeekDay day, [FromBody] UpdateDoctorSchedule model)
        {
            var updated = await scheduleService.UpdateScheduleAsync(id, day, model);
            if (!updated)
            {
                return NotFound("No Such Schedule Exists");
            }
            return Ok("Schedule Updated Succesfully");
        }

        [HttpDelete("{id}/schedules")]
        public async Task<IActionResult> DeleteDoctorSchedule(Guid id, [FromQuery] WeekDay day)
        {
            var deleted = await scheduleService.DeleteScheduleAsync(id, day);
            if (!deleted)
            {
                return NotFound("No Such Schedule Exists");
            }
            return Ok("Schedule deleted successfully");
        }
    } 
}
