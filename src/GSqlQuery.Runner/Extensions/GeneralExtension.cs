using System;

namespace GSqlQuery.Runner
{
    public static class GeneralExtension
    {
        public static object ConvertToValue(Type type, object value)
        {
            Type typeTmp = Nullable.GetUnderlyingType(type) ?? type;
            return Convert.ChangeType(value, typeTmp);
        }
    }
}