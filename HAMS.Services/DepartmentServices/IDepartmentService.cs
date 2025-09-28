using HAMS.Domain.Entities;
using HAMS.Domain.Models.Department;
using HAMS.Domain.Models.DepartmentModels;
using HAMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<ServiceResult<List<ReadDepartment>>> GetAllDepartmentAsync();
        Task<ServiceResult<ReadDepartment>> GetDepartmentByIdAsync(int id);
        Task<ServiceResult> AddDepartmentAsync(AddDepartment dept);
        Task<ServiceResult> UpdateDepartmentAsync(int id, AddDepartment dept);
        Task<ServiceResult> DeleteDepartmentAsync(int id);

    }
}
