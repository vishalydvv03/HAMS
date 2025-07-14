using HAMS.Domain.Models.AuthenticationModel;


namespace HAMS.Services.Authentication
{
    public interface IAuthService
    {
        Task<bool> RegisterPatientAsync(RegisterPatientModel model);
        Task<bool> RegisterDoctorAsync(RegisterDoctorModel model);
        Task<bool> ValidateCredentialsAsync(UserLoginModel model);
    }
}
