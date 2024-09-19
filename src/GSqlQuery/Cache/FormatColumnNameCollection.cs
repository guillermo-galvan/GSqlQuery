using GSqlQuery.Cache;
using System.Collections.Concurrent;

namespace GSqlQuery
{
    public sealed class FormatColumnNameCollection
    {
        private readonly ConcurrentDictionary<ColumnIdentity, string> _names;
        private readonly ColumnAttribute _column;

        public FormatTableNameCollection FormatTableName { get; }

        internal FormatColumnNameCollection(ColumnAttribute column, FormatTableNameCollection formatTableNameCollection)
        {
            _names = new ConcurrentDictionary<ColumnIdentity, string>();
            _column = column;
            FormatTableName = formatTableNameCollection;
        }

        public string GetColumnName<TFormats>(TFormats formats, QueryType queryType)
             where TFormats : IFormats
        {
            ColumnIdentity identity = new ColumnIdentity(formats.GetType(), queryType);
            return _names.GetOrAdd(identity, (newType) =>
            {
                return formats.GetColumnName(FormatTableName.GetTableName(formats), _column, queryType);
            });
        }
    }
}