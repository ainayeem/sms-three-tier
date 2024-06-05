using SMS.Persistence.Domain;
using static SMS.Persistence.Domain.Enums.Enum;

namespace SMS.Service.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        Student GetStudentById(string studentId);
        void AddStudent(string firstName, string middleName, string lastName, string studentId, Department department, Degree degree);
        //void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        void AddCoursesInSemister(string studentId, SemesterCode semesterCode, string year);
    }
}
