using FluentSQL.Extensions;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Default
{
    public abstract class QueryBase : IQuery
    {
        private readonly IEnumerable<ColumnAttribute> _columns;
        private readonly IEnumerable<CriteriaDetail>? _criteria;
        private readonly ConnectionOptions _connectionOptions;
        private string _text;

        public string Text { get => _text; set => _text = value; }

        public IEnumerable<ColumnAttribute> Columns => _columns;

        public IEnumerable<CriteriaDetail>? Criteria => _criteria;

        public ConnectionOptions ConnectionOptions => _connectionOptions;

        public QueryBase(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, ConnectionOptions connectionOptions)
        {
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            text.NullValidate("", nameof(text));
            _text = text;
            _criteria = criteria;
        }
    }
}
