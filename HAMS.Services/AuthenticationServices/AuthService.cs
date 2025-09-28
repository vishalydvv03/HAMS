using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Enums;
using HAMS.Domain.Models.AuthenticationModel;
using HAMS.Utility;
using HAMS.Utility.UtilityHelpers.JwtToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace HAMS.Services.AuthenticationServices
{
    public class AuthService : IAuthService
    {
        private readonly HamsDbContext context;
        private readonly IPasswordHasher<User> hasher; 
        private readonly IJwtTokenService tokenService;

        public AuthService(HamsDbContext context, IPasswordHasher<User> hasher, IJwtTokenService tokenService)
        {
            this.context = context;
            this.hasher = hasher;
            this.tokenService = tokenService;
        }

        public async Task<ServiceResult> RegisterPatientAsync(RegisterPatient model)
        {
            var result = new ServiceResult();
            var userExists = await context.Users.AnyAsync(u => u.Email == model.Email || u.ContactNo == model.ContactNo);
            if (!userExists)
            {
                var id = Guid.NewGuid();

                var user = new User
                {
                    UserId = id,
                    Email = model.Email,
                    ContactNo = model.ContactNo,
                    Role = UserRole.Patient,
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null, model.Password)
                };

                var patient = new Patient
                {
                    PatientId = id,
                    PatientName = model.PatientName,
                    Gender = model.Gender,
                    BloodGroup = model.BloodGroup,
                    Address = model.Address,
                    DOB = model.DOB
                };

                context.Users.Add(user);
                context.Patients.Add(patient);
                await context.SaveChangesAsync();
                result.SetSuccess();
            }
            else
            {
                result.SetConflict();
            }
            return result;
        }
        public async Task<ServiceResult> RegisterDoctorAsync(RegisterDoctor model)
        {
            var result = new ServiceResult();
            var userExists = await context.Users.AnyAsync(u => u.Email == model.Email || u.ContactNo == model.ContactNo);
            if (userExists)
            {
                result.SetConflict();
            }
            var deptExists = await context.Departments.AnyAsync(d => d.DepartmentId == model.DepartmentId );
            if (!deptExists)
            {
                result.SetBadRequest("No Department Exists");
            }

            var id = Guid.NewGuid();

            var user = new User
            {
                UserId = id,
                Email = model.Email,
                ContactNo = model.ContactNo,
                Role = UserRole.Doctor,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = hasher.HashPassword(null, model.Password)
            };

            var doctor = new Doctor
            {
                DoctorId = id,
                DoctorName = model.DoctorName,
                Specialization = model.Specialization,
                DepartmentId = model.DepartmentId
            };

            context.Users.Add(user);
            context.Doctors.Add(doctor);
            await context.SaveChangesAsync();
            result.SetSuccess();
            return result;
        }
        public async Task<ServiceResult> ValidateCredentialsAsync(UserLogin model)
        {
            var result = new ServiceResult();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                var verifyUser = hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                if (verifyUser == PasswordVerificationResult.Success)
                {
                    var token = tokenService.GenerateToken(user);
                    result.SetSuccess(token);
                }
            }
            else
            {
                result.SetUnAuthorized();
            }  
            return result;
        }
    }
}

