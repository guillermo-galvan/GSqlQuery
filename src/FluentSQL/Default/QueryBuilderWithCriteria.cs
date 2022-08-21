using FluentSQL.Models;

namespace FluentSQL.Default
{
    internal abstract class QueryBuilderWithCriteria<T, TReturn> : QueryBuilderBase where T : class, new() where TReturn : IQuery<T>
    {
        protected IEnumerable<CriteriaDetail>? _criteria = null;
        protected IAndOr<T, TReturn>? _andOr;

        protected QueryBuilderWithCriteria(ClassOptions options, IEnumerable<string> selectMember, IStatements statements, QueryType queryType) :
            base(options, selectMember, statements, queryType)
        { }

        protected string GetCriteria()
        {
            string result = string.Empty;
            if (_andOr != null)
            {
                _criteria ??= _andOr.BuildCriteria();
                return string.Join(" ", _criteria.Select(x => x.QueryPart));
            }

            return result;
        }
    }
}
