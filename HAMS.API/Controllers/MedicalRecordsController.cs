using HAMS.Data;
using HAMS.Domain.Models.MedicalRecordModels;
using HAMS.Services.MedicalRecordServices;
using HAMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HAMS.API.Controllers
{
    [Route("api/medical-records")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService service;
        public MedicalRecordsController(IMedicalRecordService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ServiceResult> AddRecord(AddMedicalRecord model)
        {
            return await service.AddAsync(model);                   
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResult> UpdateRecord(int id, UpdateMedicalRecord model) 
        {
            return await service.UpdateAsync(id, model);
        }
            
        [HttpDelete("{id:int}")]
        public async Task<ServiceResult> DeleteRecord(int id)
        {
            return await service.DeleteAsync(id);
        }
            
        [HttpGet]
        public async Task<ServiceResult<List<ReadMedicalRecord>>> GetAllRecords() 
        {
            return await service.GetAllAsync();

        }
    }
}
