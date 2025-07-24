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
        Task<bool> BookAppointmentAsync(AddAppointment model);
        Task<bool> CancelAppointmentAsync(int appointmentId);
        Task<bool> RescheduleAppointmentAsync(int appointmentId, RescheduleAppointment model);
        Task<bool> CompleteAppointmentAsync(int appointmentId);
        Task<IEnumerable<ReadAppointment>> GetAllAppointmentsAsync();
        Task<IEnumerable<ReadAppointmentByPatient>> GetAppointmentByPatientAsync(Guid patientId);
    }
}
