using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.PatientModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private readonly HamsDbContext context;
        public PatientService(HamsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ReadPatientModel>> GetAllAsync()
        {
            return await context.Patients.Where(x=>x.User.IsActive)
                .Select(p => new ReadPatientModel
                {
                    PatientId = p.PatientId,
                    Name = p.PatientName,
                    Gender = p.Gender,
                    DateOfBirth = p.DOB,
                    Mobile = p.User.ContactNo,
                    Email = p.User.Email,
                    Address = p.Address,
                    BloodGroup = p.BloodGroup
                }).ToListAsync();
        }

        public async Task<ReadPatientModel> GetByIdAsync(Guid id)
        {
            var patient = await context.Patients.FirstOrDefaultAsync(x=>x.PatientId==id && x.User.IsActive);
            if (patient == null) 
            {
                return null;
            }

            return new ReadPatientModel
            {
                PatientId = patient.PatientId,
                Name = patient.PatientName,
                Gender = patient.Gender,
                DateOfBirth = patient.DOB,
                Mobile = patient.User.ContactNo,
                Email = patient.User.Email,
                Address = patient.Address,
                BloodGroup = patient.BloodGroup
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdatePatientModel model)
        {
            var patient = await context.Patients.FirstOrDefaultAsync(x => x.PatientId == id && x.User.IsActive);
            if (patient == null)
            {
                return false;
            }

            patient.PatientName = model.Name;
            patient.Gender = model.Gender;
            patient.DOB = model.DateOfBirth;
            patient.User.ContactNo = model.Mobile;
            patient.User.Email = model.Email;
            patient.Address = model.Address;
            patient.BloodGroup = model.BloodGroup;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }

            patient.User.IsActive = false;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<ReadAppointmentByPatientModel>> GetAppointmentByPatientAsync(Guid patId)
        {
            var data = await context.Appointments
                        .Where(a => a.PatientId == patId && a.Patient.User.IsActive)
                        .Select(x => new ReadAppointmentByPatientModel()
                        {
                            DoctorName = x.Doctor.DoctorName,
                            AppointmentDate = x.AppointmentTime
                        }).ToListAsync();
            return data;
        }
    }
}
