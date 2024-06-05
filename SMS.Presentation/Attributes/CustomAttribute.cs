namespace SMS.Presentation.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class CustomAttribute : Attribute
    {
        private readonly string _msg;

        public CustomAttribute(string msg)
        {
            _msg = msg;
        }

        public string GetMessage()
        {
            return _msg + "from Student Management System";
        }
    }
}
