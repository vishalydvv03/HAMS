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
        Task<IEnumerable<ReadDoctorScheduleModel>> GetSchedulesByDoctorIdAsync(Guid doctorId);
        Task<bool> AddScheduleAsync(AddDoctorScheduleModel model);
        Task<bool> UpdateScheduleAsync(Guid doctorId, WeekDay day, UpdateDoctorScheduleModel model);
        Task<bool> DeleteScheduleAsync(Guid doctorId, WeekDay day);
    }
}
