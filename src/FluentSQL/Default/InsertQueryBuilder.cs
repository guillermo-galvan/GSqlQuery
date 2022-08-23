using FluentSQL.Extensions;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// insert query builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class InsertQueryBuilder<T> : QueryBuilderWithCriteria<T, InsertQuery<T>>, IQueryBuilder<T, InsertQuery<T>> where T : class, new()
    {
        private readonly object _entity;

        /// <summary>
        ///Initializes a new instance of the InsertQueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>        
        /// <param name="entity">Entity</param>
        /// <exception cref="ArgumentNullException"></exception>
        public InsertQueryBuilder(ClassOptions options, IEnumerable<string> selectMember, ConnectionOptions connectionOptions, object entity)
            : base(options, selectMember, connectionOptions, QueryType.Insert)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

        private string GetInsertQuery()
        {
            if (_entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }
            List<(string columnName, ParameterDetail parameterDetail)> values = GetValues();
            CriteriaDetail criteriaDetail = new(string.Join(",", values.Select(x => x.parameterDetail.Name)), values.Select(x => x.parameterDetail));
            _criteria = new CriteriaDetail[] { criteriaDetail };
            return string.Format(_connectionOptions.Statements.Insert, _tableName, string.Join(",", values.Select(x => x.columnName)), criteriaDetail.QueryPart);
        }

        private (string columnName, ParameterDetail parameterDetail) GetParameterValue(ColumnAttribute column)
        {
            PropertyOptions options = _options.PropertyOptions.First(x => x.ColumnAttribute.Name == column.Name);
            return (column.GetColumnName(_tableName, _connectionOptions.Statements), new ParameterDetail($"@PI{options.PropertyInfo.Name}", options.GetValue(_entity), options));
        }

        private List<(string columnName, ParameterDetail parameterDetail)> GetValues()
        {
            List<(string columnName, ParameterDetail parameterDetail)> values = new();
            foreach (var item in _columns)
            {
                if (!item.IsAutoIncrementing)
                {
                    values.Add(GetParameterValue(item));
                }
            }
            return values;
        }

        /// <summary>
        /// Build Insert Query
        /// </summary>
        public InsertQuery<T> Build()
        {
            return new InsertQuery<T>(GenerateQuery(), _columns, _criteria, _connectionOptions, _entity);
        }
       
        protected override string GenerateQuery()
        {
            return GetInsertQuery();
        }
    }
}
