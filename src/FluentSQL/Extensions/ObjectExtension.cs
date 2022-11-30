namespace FluentSQL.Extensions
{
    public static class ObjectExtension
    {
        public static void NullValidate(this object obj, string message, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}
