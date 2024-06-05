using SMS.Persistence.Domain.Enums;
using SMS.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS.Persistence.Domain.Enums.Enum;

namespace SMS.Service.DTOs
{
    public class AddStudentDTO
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; }
        public Department Department { get; set; } = Department.NotAssigned;
        public Degree Degree { get; set; } = Degree.NotAssigned;
    }
}
