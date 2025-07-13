using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAMS.Domain.Entities
{
    public class Doctor
    {
        [Key, ForeignKey(nameof(User))]
        public Guid DoctorId { get; set; }
        public User User { get; set; }

        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public int DeptartmentId { get; set; }
        public Department Department { get; set; }
    } 
}
