using HAMS.Domain.Models.AuthenticationModel;
using HAMS.Services.AuthenticationServices;
using HAMS.Utility;
using HAMS.Utility.UtilityHelpers.JwtToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAMS.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService service;

        public AccountsController(IAuthService service)
        {
            this.service = service;
        }

        [HttpPost("register/patient")]
        public async Task<ServiceResult> RegisterPatient(RegisterPatient model)
        {
            return await service.RegisterPatientAsync(model);
            
        }
            
        [HttpPost("register/doctor")]
        public async Task<ServiceResult> RegisterDoctor(RegisterDoctor model)
        {
            return await service.RegisterDoctorAsync(model);
        }

        [HttpPost("login")]
        public async Task<ServiceResult> Login(UserLogin model)
        {
            return await service.ValidateCredentialsAsync(model);
        }

    }
}
