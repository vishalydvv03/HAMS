using HAMS.Domain.Entities;
using HAMS.Domain.Models.Department;
using HAMS.Domain.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<ReadDepartmentModel>> GetAllDepartmentAsync();
        Task<ReadDepartmentModel> GetDepartmentByIdAsync(int id);
        Task<bool> AddDepartmentAsync(AddDepartmentModel dept);
        Task<bool> UpdateDepartmentAsync(int id, AddDepartmentModel dept);
        Task<bool> DeleteDepartmentAsync(int id);

    }
}
