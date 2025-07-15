using HAMS.Data;
using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.Doctor;
using HAMS.Domain.Models.DoctorModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<DoctorDetailsModel>> GetAllAsync()
        {
            var data= await context.Doctors.Include(d => d.Department).Where(x => x.User.IsActive)
                .Select(d => new DoctorDetailsModel
                {
                    DoctorId = d.DoctorId,
                    Name = d.DoctorName,
                    Specialization = d.Specialization,
                    Email = d.User.Email,
                    Contact = d.User.ContactNo,
                    DepartmentName = d.Department.DeptName
                }).ToListAsync();

            return data;
        }

        public async Task<DoctorDetailsModel> GetByIdAsync(Guid id)
        {
            var doc = await context.Doctors.Include(d => d.Department).FirstOrDefaultAsync(d => d.DoctorId == id && d.User.IsActive);

            if (doc == null) 
            {
                return null;
            } 

            return new DoctorDetailsModel
            {
                DoctorId = doc.DoctorId,
                Name = doc.DoctorName,
                Specialization = doc.Specialization,
                Email = doc.User.Email,
                Contact = doc.User.ContactNo,
                DepartmentName = doc.Department.DeptName
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateDoctorModel model)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.DoctorId == id && d.User.IsActive);
            if (doctor == null)
            {
                return false;
            }

            doctor.DoctorName = model.Name;
            doctor.Specialization = model.Specialization;
            doctor.User.Email = model.Email;
            doctor.User.ContactNo = model.Contact;
            doctor.DepartmentId = model.DepartmentId;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.DoctorId == id && d.User.IsActive);
            if (doctor == null) 
            {
                return false;
            }
            doctor.User.IsActive = false;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ReadAppointmentByDoctorModel>> GetAppointmentByDoctorAsync(Guid docId)
        {
            var data = await context.Appointments
                        .Where(a => a.DoctorId == docId && a.Doctor.User.IsActive)
                        .Select(x => new ReadAppointmentByDoctorModel()
                        {
                            PatientName = x.Patient.PatientName,
                            AppointmentDate = x.AppointmentTime
                        }).ToListAsync();
            return data;
        }
    }
}
