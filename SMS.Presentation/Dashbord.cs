using SMS.Presentation.ConsoleInputManager;

namespace SMS.Presentation
{
    public class Dashbord
    {
        public delegate void DisplayOptionsDelegate(string message);
        public delegate string DisplayMessageDelegate();



        private readonly StudentConsole _studentConsole;

        public Dashbord(StudentConsole studentConsole)
        {
            _studentConsole = studentConsole;
        }

        public static void ShowOption(string dashbord)
        {
            Console.WriteLine(dashbord);
        }

        //Show dashbord useing params
        public static void Show(DisplayOptionsDelegate showDelegate, params string[] options)
        {
            foreach (string option in options)
                showDelegate(option);
        }

        //Display Menu Method
        public void DisplayDashbord()
        {
            DisplayMessageDelegate greetings = delegate ()
            {
                return "\nWelcome to Student Management System";
            };

            string GreetingsMessage = greetings.Invoke();

            Console.WriteLine(GreetingsMessage);
            Console.WriteLine("-------------------------------------");

            DisplayOptionsDelegate optionDelegate = ShowOption;

            Show(optionDelegate, "1. Display All Studnets", "2. View Student", "3. Add New Student", "4. Add Semester and Course", "5. Delete Student", "6. Exit (press 6 to exit)");


            Console.WriteLine("---------------------------");

            string? choice = Console.ReadLine();

            UserInput(choice);
        }
        //
        private void UserInput(string choice)
        {
            switch (choice)
            {
                case "1":
                    _studentConsole.ViewAllStudents();
                    break;
                case "2":
                    _studentConsole.ViewStudentDetails();
                    break;
                case "3":
                    _studentConsole.AddStudent();
                    break;
                case "4":
                    _studentConsole.AddSemesterAndCourses();
                    break;
                case "5":
                    _studentConsole.DeleteStudent();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

    }
}
