using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Enums;
using HAMS.Domain.Models.AppointmentModels;
using HAMS.Utility;
using HAMS.Utility.UtilityHelpers.Email;
using Microsoft.EntityFrameworkCore;


namespace HAMS.Services.AppointmentServices
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HamsDbContext context;
        private readonly IEmailService emailService;
        public AppointmentService(HamsDbContext context, IEmailService emailService)
        {
            this.context = context;
            this.emailService = emailService;
        }
        public async Task<ServiceResult> BookAppointmentAsync(AddAppointment model)
        {
            var result = new ServiceResult();
            var patient = await context.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.PatientId == model.PatientId);
            var doctor = await context.Doctors.Include(x => x.User).FirstOrDefaultAsync(x => x.DoctorId == model.DoctorId);
            if (patient == null || doctor == null)
            {
                result.SetNotFound("Patient or Doctor doesn't exist.");
            }
            else
            {
                var dt = model.AppointmentTime;
                var day = (WeekDay)dt.DayOfWeek;

                var schedule = await context.DoctorSchedules.FirstOrDefaultAsync(x => x.DoctorId == model.DoctorId && x.Day == day);

                if (schedule == null || schedule.IsOnLeave)
                {
                    result.SetBadRequest("Schedule Doesn't Exist");
                }
                else
                {
                    if (schedule.StartTime != null && (dt.TimeOfDay < schedule.StartTime || dt.TimeOfDay >= schedule.EndTime))
                    {
                        result.SetBadRequest("Please book within slot time");
                    }
                    else
                    {
                        var isBooked = await context.Appointments.AnyAsync(x => x.DoctorId == model.DoctorId &&
                        x.AppointmentTime == model.AppointmentTime && x.Status == AppointmentStatus.Scheduled);

                        if (isBooked)
                        {
                            result.SetBadRequest("Slot Already Booked");
                        }
                        else
                        {
                            var appt = new Appointment()
                            {
                                PatientId = model.PatientId,
                                DoctorId = model.DoctorId,
                                AppointmentTime = model.AppointmentTime,
                                Status = AppointmentStatus.Scheduled,
                            };

                            await context.Appointments.AddAsync(appt);
                            await context.SaveChangesAsync();
                            result.SetSuccess();
                            var emailBody = $@"
                            <h3>Appointment Confirmed</h3>
                            <p>Dear {patient.PatientName},</p>
                            <p>Your appointment with Dr. {doctor.DoctorName} has been scheduled on <strong>{model.AppointmentTime:dddd, MMMM dd yyyy hh:mm tt}</strong>.</p>
                            <p>Have a nice day !</p>";

                            await emailService.SendEmailAsync(patient.User.Email, "Appointment Confirmation", emailBody);
                        }
                    }
                }
            }
            return result;
        }


        public async Task<ServiceResult> CancelAppointmentAsync(int id)
        {
            var result = new ServiceResult();
            var appt = await context.Appointments.FindAsync(id);
            if (appt == null || appt.Status == AppointmentStatus.Cancelled)
            {
                result.SetBadRequest("Appointment Doesn't Exist");
            }
            else
            {
                appt.Status = AppointmentStatus.Cancelled;
                await context.SaveChangesAsync();
                result.SetSuccess();
            }
            return result;
        }
        public async Task<ServiceResult> RescheduleAppointmentAsync(int id, RescheduleAppointment model)
        {
            var result = new ServiceResult();
            var current = await context.Appointments.FindAsync(id);
            if (current == null || current.Status != AppointmentStatus.Scheduled)
            {
                result.SetBadRequest("Appointment Not Scheduled");
            }
            else
            {
                var bookingResult = await BookAppointmentAsync(new AddAppointment
                {
                    DoctorId = current.DoctorId,
                    PatientId = current.PatientId,
                    AppointmentTime = model.NewTime
                });

                if (bookingResult.Status != 200)
                {
                    result.SetFailure($"Couldn't Book Appointment. Reason :{bookingResult.Message}");
                }
                else
                {
                    current.Status = AppointmentStatus.Rescheduled;
                    await context.SaveChangesAsync();
                    result.SetSuccess();
                }
            }
            return result;
        }
        public async Task<ServiceResult> CompleteAppointmentAsync(int id)
        {
            var result = new ServiceResult();
            var appt = await context.Appointments.FindAsync(id);
            if (appt == null || appt.Status != AppointmentStatus.Scheduled)
            {
                result.SetBadRequest("Appointment Not Scheduled");
            }
            else
            {
                appt.Status = AppointmentStatus.Completed;
                await context.SaveChangesAsync();
                result.SetSuccess();
            }
            return result;
        }
        public async Task<ServiceResult<List<ReadAppointment>>> GetAllAppointmentsAsync()
        {
            var result = new ServiceResult<List<ReadAppointment>>();
            var data = await context.Appointments
                        .Select(x => new ReadAppointment()
                        {
                            PatientName = x.Patient.PatientName,
                            DoctorName = x.Doctor.DoctorName,
                            AppointmentDate = x.AppointmentTime
                        }).ToListAsync();
            result.SetSuccess(data);
            return result;
        }
        public async Task<ServiceResult<List<ReadAppointmentByPatient>>> GetAppointmentByPatientAsync(Guid patientId)
        {
            var result = new ServiceResult<List<ReadAppointmentByPatient>>();
            var data = await context.Appointments
                        .Where(a => a.PatientId == patientId && a.Patient.User.IsActive)
                        .Select(x => new ReadAppointmentByPatient()
                        {
                            DoctorName = x.Doctor.DoctorName,
                            AppointmentDate = x.AppointmentTime,
                            Status = x.Status,
                        }).ToListAsync();

            result.SetSuccess(data);
            return result;
        }

    }
}