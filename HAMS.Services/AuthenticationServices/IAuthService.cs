using HAMS.Domain.Entities;
using HAMS.Domain.Models.AuthenticationModel;
using HAMS.Utility;


namespace HAMS.Services.AuthenticationServices
{
    public interface IAuthService
    {
        Task<ServiceResult> RegisterPatientAsync(RegisterPatient model);
        Task<ServiceResult> RegisterDoctorAsync(RegisterDoctor model);
        Task<ServiceResult> ValidateCredentialsAsync(UserLogin model);
    }
}
