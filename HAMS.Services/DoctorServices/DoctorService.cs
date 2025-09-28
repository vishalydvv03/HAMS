using HAMS.Data;
using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.Doctor;
using HAMS.Domain.Models.DoctorModels;
using HAMS.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAMS.Services.DoctorServices
{
    public class DoctorService : IDoctorService
    {
        private readonly HamsDbContext context;

        public DoctorService(HamsDbContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResult<List<DoctorDetails>>> GetAllAsync()
        {
            var result = new ServiceResult<List<DoctorDetails>>();

            var data = await context.Doctors
                .Include(d => d.Department)
                .Where(x => x.User.IsActive)
                .Select(d => new DoctorDetails
                {
                    DoctorId = d.DoctorId,
                    Name = d.DoctorName,
                    Specialization = d.Specialization,
                    Email = d.User.Email,
                    Contact = d.User.ContactNo,
                    DepartmentName = d.Department.DeptName
                }).ToListAsync();

            result.SetSuccess(data);
            return result;
        }

        public async Task<ServiceResult<DoctorDetails>> GetByIdAsync(Guid id)
        {
            var result = new ServiceResult<DoctorDetails>();

            var doc = await context.Doctors
                .Include(d => d.Department)
                .FirstOrDefaultAsync(d => d.DoctorId == id && d.User.IsActive);

            if (doc != null)
            {
                var doctorDetails = new DoctorDetails
                {
                    DoctorId = doc.DoctorId,
                    Name = doc.DoctorName,
                    Specialization = doc.Specialization,
                    Email = doc.User.Email,
                    Contact = doc.User.ContactNo,
                    DepartmentName = doc.Department.DeptName
                };

                result.SetSuccess(doctorDetails);
            }

            return result;
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, UpdateDoctor model)
        {
            var result = new ServiceResult();

            var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.DoctorId == id && d.User.IsActive);
            if (doctor != null)
            {
                doctor.DoctorName = model.Name;
                doctor.Specialization = model.Specialization;
                doctor.User.Email = model.Email;
                doctor.User.ContactNo = model.Contact;
                doctor.DepartmentId = model.DepartmentId;

                await context.SaveChangesAsync();
                result.SetSuccess();
            }
            return result;
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            var result = new ServiceResult();

            var user = await context.Users.FirstOrDefaultAsync(d => d.UserId == id && d.IsActive);
            if (user != null)
            {
                user.IsActive = false;
                await context.SaveChangesAsync();
                result.SetSuccess();
            }
            return result;
        }

        public async Task<ServiceResult<List<ReadAppointmentByDoctor>>> GetAppointmentByDoctorAsync(Guid docId)
        {
            var result = new ServiceResult<List<ReadAppointmentByDoctor>>();

            var data = await context.Appointments
                .Where(a => a.DoctorId == docId && a.Doctor.User.IsActive)
                .Select(x => new ReadAppointmentByDoctor
                {
                    PatientName = x.Patient.PatientName,
                    AppointmentDate = x.AppointmentTime
                }).ToListAsync();

            result.SetSuccess(data);
            return result;
        }
    }
}
