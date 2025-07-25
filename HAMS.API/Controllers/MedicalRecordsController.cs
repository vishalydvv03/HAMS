﻿using HAMS.Data;
using HAMS.Domain.Models.MedicalRecordModels;
using HAMS.Services.MedicalRecordServices;
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
        public async Task<IActionResult> AddRecord(AddMedicalRecord model)
        {
            var result = await service.AddAsync(model);
            if (!result)
            {
                return BadRequest("Appointment not completed or record exists");
            }
            return Ok("Record Added");                          
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRecord(int id, UpdateMedicalRecord model) 
        {
            var result = await service.UpdateAsync(id, model);
            if (!result)
            {
                return NotFound("No Such Record");
            }
            return Ok("Record Updated");
        }
            
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var result = await service.DeleteAsync(id);
            if (!result)
            {
                return NotFound("No Such Record");
            }
            return Ok("Record Deleted");
        }
            
        [HttpGet]
        public async Task<IActionResult> GetAllRecords() 
        {
            var data = await service.GetAllAsync();
            return Ok(data);
        }
    }
}
