using FluentSQL.Default;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Extensions
{
    public static class QueryExtension
    {
        public static IEnumerable<T> Exec<T>(this SelectQuery<T> query) where T : class, new()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            query.ConnectionOptions.DatabaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(query.ConnectionOptions.DatabaseManagment));
            query.ConnectionOptions.DatabaseManagment.Events.NullValidate(ErrorMessages.ParameterNotNull, nameof(query.ConnectionOptions.DatabaseManagment.Events));
#pragma warning restore CS8604 // Possible null reference argument.

            return query.ConnectionOptions.DatabaseManagment.ExecuteReader(query, ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,query.GetParameters());
        }

        public static int Exec<T>(this UpdateQuery<T> query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static int Exec<T>(this DeleteQuery<T> query) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public static T Exec<T>(this InsertQuery<T> query) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
