using HAMS.Domain.Enums;
using HAMS.Domain.Models.DoctorScheduleModels;
using HAMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.DoctorScheduleServices
{
    public interface IDoctorScheduleService
    {
        Task<ServiceResult<List<ReadDoctorSchedule>>> GetSchedulesByDoctorAsync(Guid doctorId);
        Task<ServiceResult> AddScheduleAsync(AddDoctorSchedule model);
        Task<ServiceResult> UpdateScheduleAsync(Guid doctorId, WeekDay day, UpdateDoctorSchedule model);
        Task<ServiceResult> DeleteScheduleAsync(Guid doctorId, WeekDay day);
        Task<ServiceResult<ReadDoctorSchedule>> GetScheduleByIdAsync(int scheduleId);
        Task<ServiceResult<List<ReadDoctorSchedule>>> GetAllDoctorsScheduleAsync();
        Task<ServiceResult> UpdateScheduleByIdAsync(int scheduleId, AddDoctorSchedule model);
        Task<ServiceResult> DeleteScheduleByIdAsync(int scheduleId);
    }
}
