using SMS.Persistence.DbContext;
using SMS.Persistence.Domain;
using SMS.Persistence.Interfaces;

namespace SMS.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _filePath;

        public StudentRepository(string filePath)
        {
            _filePath = filePath;
        }


        //get all students
        public List<Student> GetAllStudents()
        {
            string studentData = File.ReadAllText(_filePath);
            return JsonConverter.FromJson<List<Student>>(studentData) ?? new List<Student>();
        }

        //get student by id
        public Student GetStudentById(string studentId)
        {
            List<Student> students = GetAllStudents();
            return students.FirstOrDefault(s => s.StudentId == studentId);

        }

        //add student
        public void AddStudent(Student student)
        {
            List<Student> students = GetAllStudents();
            students.Add(student);
            string studentData = JsonConverter.ToJson(students);
            File.WriteAllText(_filePath, studentData);
        }

        //update student
        public void UpdateStudent(Student student)
        {
            List<Student> students = GetAllStudents();
            Student studentToUpdate = students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if (studentToUpdate != null)
            {
                students.Remove(studentToUpdate);
                students.Add(student);
                string studentData = JsonConverter.ToJson(students);
                File.WriteAllText(_filePath, studentData);
            }

        }

        //delete student
        public void DeleteStudent(string studentId)
        {
            List<Student> students = GetAllStudents();
            Student studentToDelete = students.FirstOrDefault(s => s.StudentId == studentId);
            if (studentToDelete != null)
            {
                students.Remove(studentToDelete);
                string studentData = JsonConverter.ToJson(students);
                File.WriteAllText(_filePath, studentData);
            }
        }

        //add courses in student semester
        public void AddCoursesInSemister(string studentId, Domain.Enums.Enum.SemesterCode semesterCode, string year)
        {
            Student student = GetStudentById(studentId);

            if (student == null)
                throw new ArgumentException("Student not found with ID: " + studentId);

            Semester newSemester = new Semester(semesterCode, year);

            student.SemestersAttended.Add(newSemester);

            AddStudent(student);

        }
    }
}
