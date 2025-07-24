using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Enums;
using HAMS.Domain.Models.DoctorScheduleModels;
using Microsoft.EntityFrameworkCore;

namespace HAMS.Services.DoctorScheduleServices
{
    public class DoctorScheduleService : IDoctorScheduleService
    {
        private readonly HamsDbContext context;

        public DoctorScheduleService(HamsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ReadDoctorSchedule>> GetSchedulesByDoctorAsync(Guid doctorId)
        {
            var data = await context.DoctorSchedules
                                    .Include(d => d.Doctor).ThenInclude(s => s.Department)
                                    .Where(x=>x.DoctorId==doctorId)
                                    .Select(x => new ReadDoctorSchedule()
                                    {
                                        DoctorName = x.Doctor.DoctorName,
                                        DepartmentName = x.Doctor.Department.DeptName,
                                        Day = x.Day,
                                        StartTime = x.StartTime,
                                        EndTime = x.EndTime,
                                        IsOnLeave = x.IsOnLeave
                                    }).ToListAsync();
            return data;
                
        }

        public async Task<bool> AddScheduleAsync(AddDoctorSchedule model)
        {
            var schedule = await context.DoctorSchedules.FirstOrDefaultAsync(s => s.DoctorId == model.DoctorId && s.Day == model.Day);

            if (schedule != null)
            {
                return false;
            }

            var newSchedule = new DoctorSchedule
            {
                DoctorId = model.DoctorId,
                Day = model.Day,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                IsOnLeave = model.IsOnLeave
            };

            context.DoctorSchedules.Add(newSchedule);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateScheduleAsync(Guid doctorId, WeekDay day, UpdateDoctorSchedule model)
        {
            var schedule = await context.DoctorSchedules.FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.Day == day);

            if (schedule == null) 
            {
                return false;
            }

            schedule.StartTime = model.StartTime;
            schedule.EndTime = model.EndTime;
            schedule.IsOnLeave = model.IsOnLeave;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteScheduleAsync(Guid doctorId, WeekDay day)
        {
            var schedule = await context.DoctorSchedules.FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.Day == day);

            if (schedule == null)
            {
                return false;
            }
            context.DoctorSchedules.Remove(schedule);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<ReadDoctorSchedule>> GetAllDoctorsScheduleAsync()
        {
            var data = await context.DoctorSchedules.Include(d=>d.Doctor).ThenInclude(d=>d.Department)
                .Select(s => new ReadDoctorSchedule
                {
                    ScheduleId = s.ScheduleId,
                    DoctorName = s.Doctor.DoctorName,
                    DepartmentName = s.Doctor.Department.DeptName,
                    Day = s.Day,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    IsOnLeave=s.IsOnLeave,
                })
                .ToListAsync();

            return data;
        }
        public async Task<ReadDoctorSchedule?> GetScheduleByIdAsync(int scheduleId)
        {
            var schedule = await context.DoctorSchedules.Include(d => d.Doctor).ThenInclude(d => d.Department)
                .FirstOrDefaultAsync(x=>x.ScheduleId==scheduleId);
            if (schedule == null)
            {
                return null;
            }
           
            var data = new ReadDoctorSchedule
            {
                ScheduleId = schedule.ScheduleId,
                DoctorName = schedule.Doctor.DoctorName,
                DepartmentName = schedule.Doctor.Department.DeptName,
                Day = schedule.Day,
                StartTime = schedule.StartTime,
                EndTime = schedule.EndTime,
                IsOnLeave = schedule.IsOnLeave
            };

            return data;
        }

        public async Task<bool> UpdateScheduleByIdAsync(int scheduleId, AddDoctorSchedule model)
        {
            var schedule = await context.DoctorSchedules.FindAsync(scheduleId);
            if (schedule == null)
            {
                return false;
            }
            schedule.DoctorId = model.DoctorId;
            schedule.Day = model.Day;
            schedule.StartTime = model.StartTime;
            schedule.EndTime = model.EndTime;
            schedule.IsOnLeave = model.IsOnLeave;

            context.DoctorSchedules.Update(schedule);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteScheduleByIdAsync(int scheduleId)
        {
            var schedule = await context.DoctorSchedules.FindAsync(scheduleId);
            if (schedule == null)
            {
                return false;
            }
            context.DoctorSchedules.Remove(schedule);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
