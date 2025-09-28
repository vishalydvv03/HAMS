using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Models.Department;
using HAMS.Domain.Models.DepartmentModels;
using HAMS.Domain.Models.Doctor;
using HAMS.Utility;
using Microsoft.EntityFrameworkCore;

namespace HAMS.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HamsDbContext context;
        public DepartmentService(HamsDbContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResult<List<ReadDepartment>>> GetAllDepartmentAsync()
        {
            var result = new ServiceResult<List<ReadDepartment>>();

            var data = await context.Departments
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Select(x => new ReadDepartment()
                {
                    DepartmentId = x.DepartmentId,
                    DeptName = x.DeptName,
                    Description = x.Description,
                    Doctors = x.Doctors.Select(d => new ReadDoctor()
                    {
                        DoctorId = d.DoctorId,
                        DoctorName = d.DoctorName,
                        Specialization = d.Specialization,
                    }).ToList()
                }).ToListAsync();

            result.SetSuccess(data);
            return result;
        }

        public async Task<ServiceResult<ReadDepartment>> GetDepartmentByIdAsync(int id)
        {
            var result = new ServiceResult<ReadDepartment>();

            var data = await context.Departments
                .FirstOrDefaultAsync(x => x.DepartmentId == id && x.IsActive);

            if (data != null)
            {
                var dept = new ReadDepartment()
                {
                    DepartmentId = data.DepartmentId,
                    DeptName = data.DeptName,
                    Description = data.Description,
                    Doctors = data.Doctors.Select(d => new ReadDoctor()
                    {
                        DoctorId = d.DoctorId,
                        DoctorName = d.DoctorName,
                        Specialization = d.Specialization,
                    }).ToList(),
                };

                result.SetSuccess(dept);
            }
            return result;
        }

        public async Task<ServiceResult> AddDepartmentAsync(AddDepartment dept)
        {
            var result = new ServiceResult();

            var deptExists = await context.Departments.AnyAsync(d => dept.DeptName == d.DeptName);
            if (!deptExists)
            {
                var entity = new Department()
                {
                    DeptName = dept.DeptName,
                    Description = dept.Description,
                    IsActive = true,
                };

                await context.Departments.AddAsync(entity);
                await context.SaveChangesAsync();

                result.SetSuccess();
            }
            else
            {
                result.SetConflict();
            }
            return result;
        }

        public async Task<ServiceResult> UpdateDepartmentAsync(int id, AddDepartment dept)
        {
            var result = new ServiceResult();

            var entity = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id && x.IsActive);
            if (entity != null)
            {
                entity.DeptName = dept.DeptName;
                entity.Description = dept.Description;
                await context.SaveChangesAsync();

                result.SetSuccess();
            }
            return result;
        }

        public async Task<ServiceResult> DeleteDepartmentAsync(int id)
        {
            var result = new ServiceResult();

            var entity = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id && x.IsActive);
            if (entity != null)
            {
                entity.IsActive = false;
                await context.SaveChangesAsync();

                result.SetSuccess();
            }
            return result;
        }
    }
}
