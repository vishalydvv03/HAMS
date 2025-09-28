using HAMS.Data;
using HAMS.Domain.Models.Department;
using HAMS.Domain.Models.DepartmentModels;
using HAMS.Services.DepartmentServices;
using HAMS.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HAMS.API.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService service;

        public DepartmentsController(IDepartmentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ServiceResult<List<ReadDepartment>>> GetAllDepartments()
        {
            return await service.GetAllDepartmentAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ServiceResult<ReadDepartment>> GetDepartmentById(int id)
        {
            return await service.GetDepartmentByIdAsync(id);
        }

        [HttpPost]
        public async Task<ServiceResult> CreateDepartment(AddDepartment dept)
        {
            return await service.AddDepartmentAsync(dept);
        }

        [HttpPut("{id:int}")]
        public async Task<ServiceResult> UpdateDepartment(int id, AddDepartment dept)
        {
            return await service.UpdateDepartmentAsync(id, dept);
        }

        [HttpDelete("{id:int}")]
        public async Task<ServiceResult> DeleteDepartment(int id)
        {
            return await service.DeleteDepartmentAsync(id);
        }
    }
}
