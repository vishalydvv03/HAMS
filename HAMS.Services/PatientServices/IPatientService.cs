using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.PatientServices
{
    public interface IPatientService
    {
        Task<List<ReadPatientModel>> GetAllAsync();
        Task<ReadPatientModel> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdatePatientModel model);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ReadAppointmentByPatientModel>> GetAppointmentByPatientAsync(Guid patId);
    }
}
