using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// insert query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class InsertQueryBuilder<T> : QueryBuilderBase<T, InsertQuery<T>>, IQueryBuilder<T, InsertQuery<T>> where T : class, new()
    {
        protected readonly object _entity;
        protected bool _includeAutoIncrementing;
        protected IEnumerable<CriteriaDetail>? _criteria = null;

        /// <summary>
        ///Initializes a new instance of the InsertQueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>        
        /// <param name="statements">Statements to build the query</param>        
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public InsertQueryBuilder(IStatements statements, object entity)
            : base(statements, QueryType.Insert)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        protected string GetInsertQuery()
        {
            Queue<(string columnName, ParameterDetail parameterDetail)> values = GetValues();
            CriteriaDetail criteriaDetail = new(string.Join(",", values.Select(x => x.parameterDetail.Name)), values.Select(x => x.parameterDetail));
            _criteria = new CriteriaDetail[] { criteriaDetail };
            string text = _includeAutoIncrementing ? 
                $"{string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart)} {Statements.ValueAutoIncrementingQuery}"
                : string.Format(Statements.Insert, _tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart);

            return text;
        }

        protected (string columnName, ParameterDetail parameterDetail) GetParameterValue(ColumnAttribute column)
        {
            PropertyOptions options = Columns.First(x => x.ColumnAttribute.Name == column.Name);
            return (column.GetColumnName(_tableName, Statements), new ParameterDetail($"@PI{DateTime.Now.Ticks}", options.GetValue(_entity), options));
        }

        protected Queue<(string columnName, ParameterDetail parameterDetail)> GetValues()
        {
            Queue<(string columnName, ParameterDetail parameterDetail)> values = new();
            _includeAutoIncrementing = false;
            foreach (PropertyOptions item in Columns)
            {
                if (!item.ColumnAttribute.IsAutoIncrementing)
                {
                    values.Enqueue(GetParameterValue(item.ColumnAttribute));
                }
                else
                {
                    _includeAutoIncrementing = true;
                }
            }
            return values;
        }

        /// <summary>
        /// Build Insert Query
        /// </summary>
        public override InsertQuery<T> Build()
        {
            return new InsertQuery<T>(GenerateQuery(), Columns.Select(x => x.ColumnAttribute), _criteria, Statements, _entity);
        }
       
        protected override string GenerateQuery()
        {
            return GetInsertQuery();
        }
    }
}
