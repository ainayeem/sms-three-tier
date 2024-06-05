using SMS.Persistence.Domain;
using SMS.Persistence.Repositories;
using SMS.Service.DTOs;
using SMS.Service.Interfaces;
using SMS.Service.Mappers;
using static SMS.Persistence.Domain.Enums.Enum;

namespace SMS.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _studentRepository;
        private readonly Course[] _availableCourses;

        public StudentService(StudentRepository studentRepository, Course[] availableCourses)
        {
            _studentRepository = studentRepository;
            _availableCourses = availableCourses;
        }






        //get student by  id
        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }

        //get student by id
        public Student GetStudentById(string studentId)
        {
            var student = _studentRepository.GetAllStudents().FirstOrDefault(s => s.StudentId == studentId);
            return student;
        }

        //add student
        public void AddStudent(string firstName, string middleName, string lastName, string studentId, Persistence.Domain.Enums.Enum.Department department, Persistence.Domain.Enums.Enum.Degree degree)
        {
            AddStudentDTO newStudent = new AddStudentDTO()
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                StudentId = studentId,
                Department = (Department)Enum.Parse(typeof(Department), department.ToString()),
                Degree = (Degree)Enum.Parse(typeof(Degree), degree.ToString()),

            };

            Student studentToAdd = AddStudentMapper.MapToStudent(newStudent);

            _studentRepository.AddStudent(studentToAdd);

        }

        //delete student
        public void DeleteStudent(Student student)
        {
            _studentRepository.DeleteStudent(student.StudentId);
        }

        public void AddCoursesInSemister(string studentId, Persistence.Domain.Enums.Enum.SemesterCode semesterCode, string year)
        {
            var student = _studentRepository.GetStudentById(studentId);

            bool isSemesterAttended = student.SemestersAttended.Any(semester =>
            (SemesterCode)semester.SemesterCode == semesterCode && semester.Year == year);

            if (isSemesterAttended)
            {
                Console.WriteLine("Already enrolled the course");
            }
            else
            {

                Console.WriteLine("\nAvailable Courses:\n");

                var availableCourses = _availableCourses.Where(c => !student.AttendedCourse.Any(x => x.CourseId == c.CourseId)).ToList();

                foreach (var course in availableCourses)
                {
                    Console.WriteLine($"{course.CourseName}");
                }

                Console.WriteLine("\nTotal courses you want to take? :\n");

                int num = Convert.ToInt32(Console.ReadLine());

                List<Course> courses = new List<Course>();

                for (int i = 0; i < num; i++)
                {
                    Console.Write("\nEnter Course ID to add (XXX YYY): ");

                    string? selectedCourseID = Console.ReadLine();

                    var course = availableCourses.Find(x => x.CourseId == selectedCourseID);

                    if (course != null)
                    {
                        courses.Add(course);
                        student.AttendedCourse.Add(course);
                        course = null;
                    }
                    else
                    {
                        Console.WriteLine("\n This course is not available\n");
                    }
                }

                Semester joiningSemester = new Semester(semesterCode, year);

                joiningSemester.Courses = courses;
                student.JoiningBatch = joiningSemester;
                student.SemestersAttended.Add(joiningSemester);

                _studentRepository.UpdateStudent(student);
            }
        }
    }
}
