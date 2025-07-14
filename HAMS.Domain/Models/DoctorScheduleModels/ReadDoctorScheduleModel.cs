using HAMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Domain.Models.DoctorScheduleModels
{
    public class ReadDoctorScheduleModel
    {
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public WeekDay Day { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public bool IsOnLeave { get; set; }
    }
}
