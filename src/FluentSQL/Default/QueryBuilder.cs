using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("FluentSQLTest")]

namespace FluentSQL.Default
{
    /// <summary>
    /// Query Builder
    /// </summary>
    internal class QueryBuilder : IQueryBuilder
    {
        private readonly ClassOptions _options;
        private readonly IStatements _statements;
        private IEnumerable<ColumnAttribute> _columns;

        /// <summary>
        /// Get Query
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Get Columns of the query
        /// </summary>
        public IEnumerable<ColumnAttribute> Columns => _columns;

        /// <summary>
        /// Create QueryBuilder object with default declarations
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBuilder(ClassOptions options, IEnumerable<string> selectMember) : this(options, selectMember, new Statements())
        {
        }

        /// <summary>
        /// Create QueryBuilder object with default declarations
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public QueryBuilder(ClassOptions options, IEnumerable<string> selectMember, IStatements statements)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            selectMember = selectMember ?? throw new ArgumentNullException(nameof(selectMember));
            _statements = statements ?? throw new ArgumentNullException(nameof(selectMember));
            Text = string.Format(_statements.SelectText, GetColumnsQuery(selectMember), GetTable());
        }

        private string GetColumnsQuery(IEnumerable<string> selectMember)
        {
            _columns = (from prop in _options.PropertyOptions
                        join sel in selectMember on prop.PropertyInfo.Name equals sel
                        select prop.ColumnAttribute).ToArray();

            return string.Join(",", _columns.Select(x => string.Format(_statements.Format, x.Name)));
        }

        private string GetTable()
        {
            return string.IsNullOrWhiteSpace(_options.Table.Scheme) ? string.Format(_statements.Format, _options.Table.TableName) :
                   $"{string.Format(_statements.Format, _options.Table.Scheme)}.{string.Format(_statements.Format, _options.Table.TableName)}";
        }
    }
}
