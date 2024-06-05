using SMS.Persistence.Domain;
using static SMS.Persistence.Domain.Enums.Enum;

namespace SMS.Persistence.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();
        Student GetStudentById(string studentId);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(string studentId);
        void AddCoursesInSemister(string studentId, SemesterCode semesterCode, string year);


    }
}
