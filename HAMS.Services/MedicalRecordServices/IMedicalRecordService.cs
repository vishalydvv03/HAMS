using HAMS.Domain.Models.MedicalRecordModels;
using HAMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.MedicalRecordServices
{
    public interface IMedicalRecordService
    {
        Task<ServiceResult> AddAsync(AddMedicalRecord model);
        Task<ServiceResult> UpdateAsync(int id, UpdateMedicalRecord model);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<ReadMedicalRecord>>> GetAllAsync();
        Task<ServiceResult<List<ReadMedicalRecordByPatient>>> GetRecordsForPatientAsync(Guid patId);

    }
}
