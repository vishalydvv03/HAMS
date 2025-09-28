using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Enums;
using HAMS.Domain.Models.MedicalRecordModels;
using HAMS.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.MedicalRecordServices
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly HamsDbContext context;
        public MedicalRecordService(HamsDbContext context)
        { 
            this.context = context;
        }

        public async Task<ServiceResult> AddAsync(AddMedicalRecord model)
        {
            var result = new ServiceResult();
            var appt = await context.Appointments.Include(a => a.Patient)
                                                 .Include(a => a.Doctor)
                                                 .FirstOrDefaultAsync(a => a.AppointmentId == model.AppointmentId);

            if (appt == null || appt.Status != AppointmentStatus.Completed) 
            {
                result.SetBadRequest("Appointment Dosesn't Exist or not completed");
                return result;
            }

            bool recordExists = await context.MedicalRecords.AnyAsync(r => r.AppointmentId == model.AppointmentId);
            if (recordExists)
            {
                result.SetConflict();
            }

            var record = new MedicalRecord()
                        {
                            AppointmentId = model.AppointmentId,
                            VisitNotes = model.VisitNotes,
                            Prescription = model.Prescription,
                            FollowUpInstructions = model.FollowUpInstructions
                        };
            await context.MedicalRecords.AddAsync(record);
            await context.SaveChangesAsync();
            result.SetSuccess();
            return result;
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateMedicalRecord model)
        {
            var result = new ServiceResult();
            var record = await context.MedicalRecords.FindAsync(id);
            if (record != null)
            {
                record.VisitNotes = model.VisitNotes;
                record.Prescription = model.Prescription;
                record.FollowUpInstructions = model.FollowUpInstructions;

                await context.SaveChangesAsync();
                result.SetSuccess();
            }
            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();
            var rec = await context.MedicalRecords.FindAsync(id);
            if (rec != null)
            {
                context.MedicalRecords.Remove(rec);
                await context.SaveChangesAsync();
                result.SetSuccess();
                
            }
            return result;
        }
        public async Task<ServiceResult<List<ReadMedicalRecord>>> GetAllAsync()
        {
            var result = new ServiceResult<List<ReadMedicalRecord>>();
            var data = await context.MedicalRecords
                .Include(r => r.Appointment).ThenInclude(a => a.Patient)
                .Include(r => r.Appointment).ThenInclude(a => a.Doctor)
                .Select(r => new ReadMedicalRecord()
                {
                    RecordId = r.RecordId,
                    AppointmentId = r.AppointmentId,
                    PatientId = r.Appointment.PatientId,
                    PatientName = r.Appointment.Patient.PatientName,
                    DoctorName = r.Appointment.Doctor.DoctorName,
                    AppointmentTime = r.Appointment.AppointmentTime,
                    VisitNotes = r.VisitNotes,
                    Prescription = r.Prescription,
                    FollowUpInstructions = r.FollowUpInstructions,
                    CreatedAt = r.CreatedAt
                }).ToListAsync();

            result.SetSuccess(data);
            return result;
        }
        public async Task<ServiceResult<List<ReadMedicalRecordByPatient>>> GetRecordsForPatientAsync(Guid patId)
        {
            var result = new ServiceResult<List<ReadMedicalRecordByPatient>>();
            var data = await context.MedicalRecords
                .Include(r => r.Appointment).ThenInclude(a => a.Patient)
                .Include(r => r.Appointment).ThenInclude(a => a.Doctor)
                .Where(r => r.Appointment.PatientId == patId)
                .Select(r => new ReadMedicalRecordByPatient()
                {
                    RecordId = r.RecordId,
                    AppointmentId = r.AppointmentId,
                    DoctorId = r.Appointment.DoctorId,
                    DoctorName = r.Appointment.Doctor.DoctorName,
                    AppointmentTime = r.Appointment.AppointmentTime,
                    VisitNotes = r.VisitNotes,
                    Prescription = r.Prescription,
                    FollowUpInstructions = r.FollowUpInstructions,
                    CreatedAt = r.CreatedAt
                }).ToListAsync();

            result.SetSuccess(data);
            return result;
        }
    }
}
