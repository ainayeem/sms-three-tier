using SMS.Persistence.Domain;
using SMS.Persistence.Repositories;
using SMS.Presentation.Attributes;
using SMS.Presentation.ConsoleInputManager;
using SMS.Service.Services;

namespace SMS.Presentation
{
    [Custom("Greetings! ")]
    internal class Program
    {
        static void Main(string[] args)
        {
            var attributes = Attribute.GetCustomAttributes(typeof(Program));

            var developerAttribute = attributes.OfType<CustomAttribute>().Single();

            Console.WriteLine(developerAttribute.GetMessage());


            Course[] courses = CreateCourses();

            string studentDataPath = "students.json";

            StudentRepository studentRepository = new StudentRepository(studentDataPath);

            StudentService studentService = new StudentService(studentRepository, courses);

            StudentConsole studentController = new StudentConsole(studentService);

            Dashbord menu = new Dashbord(studentController);

            while (true)
            {
                menu.DisplayDashbord();
            }
        }
        private static Course[] CreateCourses()
        {
            Course[] courses = new Course[]
            {
                new Course ( "CSE 101", "C Programming", "Prof. Nasir", 3.5),
                new Course ("CSE 102","Machine Learning","Prof. Motiur", 2.5),
                new Course ("CSE 103", "Java Programming", "Prof. Misbah", 3.5),
                new Course ("CSE 104", "Arcitecture", "Prof. Sultan", 2.5),
                new Course ("CSE 105", "Mathematics I", "Prof. Musa" ,3.0)
            };

            return courses;
        }
    }
}
