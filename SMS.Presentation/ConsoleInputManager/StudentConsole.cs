using SMS.Persistence.Domain;
using SMS.Service.Services;
using static SMS.Persistence.Domain.Enums.Enum;
using System.Text.RegularExpressions;
using SMS.Service.Extensions;

namespace SMS.Presentation.ConsoleInputManager
{
    public delegate string EventDelegate(string str);
    public class StudentConsole
    {
        private readonly StudentService _studentService;
        public event EventDelegate StudentEvent;

        public StudentConsole(StudentService studentService)
        {
            _studentService = studentService;
            this.StudentEvent += new EventDelegate(this.SuccessMessage);
        }

        //Event Message 
        public string SuccessMessage(string message)
        {
            return message;
        }

        //--
        public void ViewAllStudents()
        {
            List<Student> students = _studentService.GetAllStudents();

            if (students.Count == 0)
            {
                string notFound = StudentEvent("\nThere are currently no student\n");
                Console.WriteLine(notFound);
            }
            else
            {
                Console.WriteLine("\nAll students List: \n");

                foreach (Student student in students)
                {
                    Console.WriteLine(student.ToString());
                }
            }
        }

        //View Student By ID
        public void ViewStudentDetails()
        {
            try
            {
                Console.Write("Enter Student ID: ");

                string? studentId = Console.ReadLine();

                var student = _studentService.GetStudentById(studentId);

                if (student != null)
                {

                    Console.WriteLine("\nStudent Details:\n");
                    Console.WriteLine($"Name: {student.FirstName} {student.MiddleName} {student.LastName}");
                    Console.WriteLine($"Student ID: {student.StudentId}");
                    if (student.JoiningBatch != null)
                    {
                        Console.WriteLine($"{student.JoiningBatch.SemesterCode} {student.JoiningBatch.Year}");
                    }


                    Console.WriteLine($"Department: {(Department)student.Department}");
                    Console.WriteLine($"Degree: {(Department)student.Degree}");

                    if (student.SemestersAttended.Count == 0)
                    {
                        Console.WriteLine("\nThe student is currently in no semester\n");
                    }

                    foreach (var semester in student.SemestersAttended)
                    {
                        Console.WriteLine($"Semester: {semester.SemesterCode} {semester.Year}");
                        Console.WriteLine("\nAttended Courses Are:\n");

                        if (student.AttendedCourse.Count == 0)
                        {
                            Console.WriteLine("\nThe Student hasn't taken any course yet\n");
                        }
                        foreach (var course in student.AttendedCourse)
                        {
                            Console.WriteLine($"Course ID: {course.CourseId}, Course Name: {course.CourseName}, Instructor: {course.InstructorName}, Credits: {course.NumberOfCredits}\n");
                        }
                    }
                }
                else
                {
                    string notFound = StudentEvent($"\nStudent with ID {studentId} not found.");
                    Console.WriteLine(notFound);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        //Add Student
        public void AddStudent()
        {
            try
            {
                Console.Write("First Name : ");
                dynamic? firstName = Console.ReadLine();

                Console.Write("Middle Name : ");
                dynamic? middleName = Console.ReadLine();

                Console.Write("Last Name : ");
                dynamic? lastName = Console.ReadLine();

            idInput:
                Console.Write("Student ID (format XXX-XXX-XXX) : ");
                dynamic? studentID = Console.ReadLine();

                string StudentIdPattern = @"^\d{3}-\d{3}-\d{3}$";

                if (!Regex.IsMatch(studentID, StudentIdPattern))
                {
                    Console.WriteLine("Invalid student ID format. Please enter in the format XXX-XXX-XXX.");
                    goto idInput;
                }

            departmentInput:
                Console.Write("Department Number (1 for CSE , 2 for BBA, 3 for English) : ");
                int department = Convert.ToInt32(Console.ReadLine());

                int departmentEnumLength = typeof(Department).GetEnumLength();

                if (department > departmentEnumLength)
                {
                    Console.WriteLine("Invalid enum value ... please enter value between 1 and 3");
                    goto departmentInput;
                }

            degreeInput:
                Console.Write("Degree Number (1 for BSC, 2 for BBA, 3 for BA, 4 for MSC, 5 for MBA, 6 for MA ): ");
                int degree = Convert.ToInt32(Console.ReadLine());

                int degreeEnumLength = typeof(Degree).GetEnumLength();

                if (degree > degreeEnumLength)
                {
                    Console.WriteLine("Invalid enum value ... please enter value between 1 and 6");
                    goto departmentInput;
                }

                _studentService.AddStudent(firstName, middleName, lastName, studentID, (Department)department, (Degree)degree);

                string successResult = StudentEvent("\nStudent Added Successfully\n");

                Console.WriteLine(successResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Add Semester and Course
        public void AddSemesterAndCourses()
        {
            try
            {
                Console.Write("Enter Student ID: ");

                string studentId = Console.ReadLine();

                var student = _studentService.GetStudentById(studentId);

                if (student != null)
                {
                semesterCodeInput:
                    Console.Write("\nEnter Semester Code (1: Summer, 2: Fall, 3: Spring): ");

                    int semCode = Convert.ToInt32(Console.ReadLine());
                    var semesterCode = (SemesterCode)Enum.Parse(typeof(SemesterCode), semCode.ToString());

                    int semesterCodeEnumLength = typeof(SemesterCode).GetEnumLength();

                    if (semCode > semesterCodeEnumLength)
                    {
                        Console.WriteLine("Invalid enum value ... please enter value between 1 and 3");
                        goto semesterCodeInput;
                    }

                    Console.Write("Enter Year (YYYY): ");
                    string? year = Console.ReadLine();

                    _studentService.AddCoursesInSemister(studentId, semesterCode, year);

                    string successResult = StudentEvent("\nSemester and courses added successfully.\n");

                    Console.WriteLine(successResult);
                }
                else
                {
                    string notFound = StudentEvent($"\nStudent with ID {studentId} not found.");

                    Console.WriteLine(notFound);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Delete Student 
        public void DeleteStudent()
        {
            try
            {
                Console.Write("Enter Student ID: ");

                string? studentId = Console.ReadLine();

                var student = _studentService.GetStudentById(studentId);

                if (student != null)
                {
                    _studentService.DeleteStudent(student);
                    string successResult = StudentEvent("\nStudent Deleted Successfully\n");

                    Console.WriteLine(successResult);
                }
                else
                {
                    string notFound = StudentEvent($"\nStudent with ID {studentId} not found.");

                    Console.WriteLine(notFound);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
