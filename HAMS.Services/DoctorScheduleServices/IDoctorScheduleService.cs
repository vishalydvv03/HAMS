using HAMS.Domain.Enums;
using HAMS.Domain.Models.DoctorScheduleModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.DoctorScheduleServices
{
    public interface IDoctorScheduleService
    {
        Task<IEnumerable<ReadDoctorSchedule>> GetSchedulesByDoctorAsync(Guid doctorId);
        Task<bool> AddScheduleAsync(AddDoctorSchedule model);
        Task<bool> UpdateScheduleAsync(Guid doctorId, WeekDay day, UpdateDoctorSchedule model);
        Task<bool> DeleteScheduleAsync(Guid doctorId, WeekDay day);
        Task<ReadDoctorSchedule?> GetScheduleByIdAsync(int scheduleId);
        Task<List<ReadDoctorSchedule>> GetAllDoctorsScheduleAsync();
        Task<bool> UpdateScheduleByIdAsync(int scheduleId, AddDoctorSchedule model);
        Task<bool> DeleteScheduleByIdAsync(int scheduleId);
    }
}
