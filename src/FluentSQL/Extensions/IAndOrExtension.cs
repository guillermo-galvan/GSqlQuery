namespace FluentSQL.Extensions
{
    internal static class IAndOrExtension
    {
        internal static string GetCliteria<TReturn>(this IAndOr<TReturn> andOr, IStatements statements, ref IEnumerable<CriteriaDetail>? criterias)
        {
            if (andOr != null)
            {
                criterias ??= andOr.BuildCriteria(statements);
                return string.Join(" ", criterias.Select(x => x.QueryPart));
            }

            return string.Empty;
        }
    }
}
