using System;
using System.Collections.Concurrent;

namespace GSqlQuery.Cache
{
    public sealed class FormatTableNameCollection
    {
        private readonly ConcurrentDictionary<Type, string> _names;
        private readonly TableAttribute _table;

        public TableAttribute Table => _table;

        internal FormatTableNameCollection(TableAttribute table)
        {
            _names = new ConcurrentDictionary<Type, string>();
            _table = table;
        }

        public string GetTableName<TFormats>(TFormats formats) where TFormats : IFormats
        {
            return _names.GetOrAdd(formats.GetType(), (newType) =>
            {
                string tableName = formats.Format.Replace("{0}", _table.Name);

                if (string.IsNullOrWhiteSpace(_table.Scheme))
                {
                    return tableName;
                }

                string schema = formats.Format.Replace("{0}", _table.Scheme) + ".";
                return schema + tableName;
            });
        }
    }
}