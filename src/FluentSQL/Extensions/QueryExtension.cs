using FluentSQL.Default;

namespace FluentSQL.Extensions
{
    public static class QueryExtension
    {
        public static int Exec<T>(this UpdateQuery<T> query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static int Exec<T>(this DeleteQuery<T> query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<T> Exec<T>(this SelectQuery<T> query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static T Exec<T>(this InsertQuery<T> query) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
