using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    public abstract class QueryBuilderWithCriteria<T, TReturn> : QueryBuilderBase where T : class, new() where TReturn : IQuery<T>
    {
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected IAndOr<T, TReturn>? _andOr;

        protected QueryBuilderWithCriteria(IStatements statements, QueryType queryType) :
            base(ClassOptionsFactory.GetClassOptions(typeof(T)).Table.GetTableName(statements), ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions,
                statements, queryType)
        { }

        protected string GetCriteria()
        {
            string result = string.Empty;
            if (_andOr != null)
            {
                _criteria ??= _andOr.BuildCriteria(Statements);
                return string.Join(" ", _criteria.Select(x => x.QueryPart));
            }

            return result;
        }
    }
}
