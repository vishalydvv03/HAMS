using HAMS.Data;
using HAMS.Domain.Entities;
using HAMS.Domain.Models.Department;
using HAMS.Domain.Models.DepartmentModels;
using HAMS.Domain.Models.Doctor;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace HAMS.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HamsDbContext context;
        public DepartmentService(HamsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ReadDepartmentModel>> GetAllDepartmentAsync()
        {
            var data = await context.Departments.AsNoTracking().Where(x=>x.IsActive).Select(x=>new ReadDepartmentModel()
            {
                DepartmentId = x.DepartmentId,
                DeptName = x.DeptName,
                Description = x.Description,
                Doctors = x.Doctors.Select(d => new ReadDoctorModel()
                {
                    DoctorId = d.DoctorId,
                    DoctorName = d.DoctorName,
                    Specialization = d.Specialization,
                }).ToList()
            }).ToListAsync();
            return data;
        }
        public async Task<ReadDepartmentModel?> GetDepartmentByIdAsync(int id)
        {
            var data = await context.Departments.FirstOrDefaultAsync(x=>x.DepartmentId==id && x.IsActive);

            if (data!=null)
            {
                var dept = new ReadDepartmentModel()
                {
                    DepartmentId = data.DepartmentId,
                    DeptName = data.DeptName,
                    Description = data.Description,
                    Doctors = data.Doctors.Select(d => new ReadDoctorModel()
                    {
                        DoctorId = d.DoctorId,
                        DoctorName = d.DoctorName,
                        Specialization = d.Specialization,
                    }).ToList(),
                };
                return dept;
            }
            return null;
        }
        public async Task<bool> AddDepartmentAsync(AddDepartmentModel dept)
        {
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

                return true;
            }
            return false;
        }
        public async Task<bool> UpdateDepartmentAsync(int id, AddDepartmentModel dept)
        {
            var entity = await context.Departments.FirstOrDefaultAsync(x=>x.DepartmentId==id && x.IsActive);
            if (entity != null)
            {
                entity.DeptName = dept.DeptName;
                entity.Description = dept.Description;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var entity = await context.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id && x.IsActive );
            if (entity != null)
            {
                entity.IsActive = false;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
