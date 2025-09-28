using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Models.AppointmentModels;
using HAMS.Domain.Models.MedicalRecordModels;
using HAMS.Domain.Models.PatientModels;
using HAMS.Utility;
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

        public async Task<ServiceResult<List<ReadPatient>>> GetAllAsync()
        {
            var result = new ServiceResult<List<ReadPatient>>();
            var patients = await context.Patients.Where(x => x.User.IsActive)
                .Select(p => new ReadPatient
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

            result.SetSuccess(patients);
            return result;
        }

        public async Task<ServiceResult<ReadPatient>> GetByIdAsync(Guid id)
        {
            var result = new ServiceResult<ReadPatient>();
            var patient = await context.Patients.FirstOrDefaultAsync(x => x.PatientId == id && x.User.IsActive);
            if (patient != null)
            {
                var patientModel = new ReadPatient
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

                result.SetSuccess(patientModel);
            }
            return result;
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, UpdatePatient model)
        {
            var result = new ServiceResult();
            var patient = await context.Patients.FirstOrDefaultAsync(x => x.PatientId == id && x.User.IsActive);
            if (patient != null)
            {
                patient.PatientName = model.Name;
                patient.Gender = model.Gender;
                patient.DOB = model.DateOfBirth;
                patient.User.ContactNo = model.Mobile;
                patient.User.Email = model.Email;
                patient.Address = model.Address;
                patient.BloodGroup = model.BloodGroup;

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
        public async Task<ServiceResult<List<ReadPatient>>> SearchPatientsAsync(string? name, string? email, string? mobile)
        {
            var result = new ServiceResult<List<ReadPatient>>();
            var query = context.Users.AsNoTracking().Include(x => x.Patient).AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(p => p.Patient.PatientName.Contains(name));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(p => p.Email.Contains(email));

            if (!string.IsNullOrWhiteSpace(mobile))
                query = query.Where(p => p.ContactNo.Contains(mobile));

            var patients = await query.Select(p => new ReadPatient
            {
                PatientId = p.Patient.PatientId,
                Name = p.Patient.PatientName,
                Email = p.Email,
                Mobile = p.ContactNo,
                Gender = p.Patient.Gender,
                DateOfBirth = p.Patient.DOB,
                BloodGroup = p.Patient.BloodGroup,
                Address = p.Patient.Address
            }).ToListAsync();

            result.SetSuccess(patients);
            return result;
        }

    }
}
