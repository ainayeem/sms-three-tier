using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMS.Persistence.Domain.Enums.Enum;

namespace SMS.Persistence.Domain
{
    public class Student
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string StudentId { get; set; } 
        public Semester? JoiningBatch { get; set; }
        public Department Department { get; set; } = Department.NotAssigned;
        public Degree Degree { get; set; } = Degree.NotAssigned;
        public List<Course> AttendedCourse { get; set; }
        public List<Semester> SemestersAttended { get; set; }

        public Student()
        {
            JoiningBatch = null;
            AttendedCourse = [];
            SemestersAttended = [];
        }

        //implement polymorphism
        public override string ToString()
        {
            if (MiddleName != null)
            {
                return $"Name: {FirstName} {MiddleName} {LastName} , Id: {StudentId}, Degree: {(Degree)Degree}, Department: {(Department)Department}";
            }

            return $"Name: {FirstName} {LastName}, Id: {StudentId}, Degree: {(Degree)Degree}, Department: {(Department)Department}";


        }
    }
}
