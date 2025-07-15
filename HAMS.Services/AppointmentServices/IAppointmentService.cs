using HAMS.Domain.Models.AppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.AppointmentServices
{
    public interface IAppointmentService
    {
        Task<bool> BookAsync(AddAppointmentModel model);
        Task<bool> CancelAsync(int appointmentId);
        Task<bool> RescheduleAsync(int appointmentId, RescheduleAppointmentModel model);
        Task<bool> CompleteAsync(int appointmentId);
        Task<IEnumerable<ReadAppointmentModel>> GetAllAppointmentsAsync();
    }
}
