using HAMS.Domain.Models.AppointmentModels;
using HAMS.Services.AppointmentServices;
using HAMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace HAMS.API.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService service;
        public AppointmentsController(IAppointmentService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ServiceResult> BookAppointmentForPatient(AddAppointment model)
        {
            return await service.BookAppointmentAsync(model);
        }

        [HttpPut("{id:int}/cancel")]
        public async Task<ServiceResult> Cancel(int id)
        {
            return await service.CancelAppointmentAsync(id);
        }
            
        [HttpPut("{id:int}/reschedule")]
        public async Task<ServiceResult> Reschedule(int id, RescheduleAppointment model)
        {
            return await service.RescheduleAppointmentAsync(id, model);
        }

        [HttpPut("{id:int}/complete")]
        public async Task<ServiceResult> Complete(int id)
        {
            return await service.CompleteAppointmentAsync(id);
        }

        [HttpGet]
        public async Task<ServiceResult<List<ReadAppointment>>> GetAllAppointments()
        {
            return await service.GetAllAppointmentsAsync();
        }
            
    }
}
