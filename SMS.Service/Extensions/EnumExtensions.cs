namespace SMS.Service.Extensions
{
    public static class EnumExtensions
    {
        public static int GetEnumLength(this Type enumType)
        {
            return Enum.GetNames(enumType).Length;
        }
    }
}
