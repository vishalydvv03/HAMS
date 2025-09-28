using HAMS.Domain.Models.AppointmentModels;
using HAMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.AppointmentServices
{
    public interface IAppointmentService
    {
        Task<ServiceResult> BookAppointmentAsync(AddAppointment model);
        Task<ServiceResult> CancelAppointmentAsync(int appointmentId);
        Task<ServiceResult> RescheduleAppointmentAsync(int appointmentId, RescheduleAppointment model);
        Task<ServiceResult> CompleteAppointmentAsync(int appointmentId);
        Task<ServiceResult<List<ReadAppointment>>> GetAllAppointmentsAsync();
        Task<ServiceResult<List<ReadAppointmentByPatient>>> GetAppointmentByPatientAsync(Guid patientId);
    }
}
