using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.Doctor;
using HAMS.Domain.Models.DoctorModels;
using HAMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.DoctorServices
{
    public interface IDoctorService
    {
        Task<ServiceResult<List<DoctorDetails>>> GetAllAsync();
        Task<ServiceResult<DoctorDetails>> GetByIdAsync(Guid id);
        Task<ServiceResult> UpdateAsync(Guid id, UpdateDoctor model);
        Task<ServiceResult> DeleteAsync(Guid id);
        Task<ServiceResult<List<ReadAppointmentByDoctor>>> GetAppointmentByDoctorAsync(Guid docId);
    }
}
