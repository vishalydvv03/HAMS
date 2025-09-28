using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.MedicalRecordModels;
using HAMS.Domain.Models.PatientModels;
using HAMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.PatientServices
{
    public interface IPatientService
    {
        Task<ServiceResult<List<ReadPatient>>> GetAllAsync();
        Task<ServiceResult<ReadPatient>> GetByIdAsync(Guid id);
        Task<ServiceResult> UpdateAsync(Guid id, UpdatePatient model);
        Task<ServiceResult> DeleteAsync(Guid id);
        Task<ServiceResult<List<ReadPatient>>> SearchPatientsAsync(string? name, string? email, string? mobile);

    }
}
