using FluentSQL.Extensions;
using FluentSQL.Models;
using System.Linq.Expressions;

namespace FluentSQL.Default
{
    /// <summary>
    /// Base class to generate the set query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    internal class Set<T> : ISet<T, UpdateQuery<T>> where T : class, new()
    {
        private readonly Dictionary<ColumnAttribute, object?> _columnValues;
        private readonly ClassOptions _options;
        private readonly IStatements _statements;
        private readonly object? _entity;

        /// <summary>
        /// Get column values
        /// </summary>
        public IDictionary<ColumnAttribute, object?> ColumnValues => _columnValues;

        public Set(ClassOptions options,IEnumerable<string> selectMember, IStatements statements, object? value)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            selectMember = selectMember ?? throw new ArgumentNullException(nameof(selectMember));
            _statements = statements ?? throw new ArgumentNullException(nameof(selectMember));
            _columnValues = new Dictionary<ColumnAttribute, object?>();
            foreach (ColumnAttribute item in _options.GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        public Set(object? entity,ClassOptions options, IEnumerable<string> selectMember, IStatements statements)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            selectMember = selectMember ?? throw new ArgumentNullException(nameof(selectMember));
            _statements = statements ?? throw new ArgumentNullException(nameof(selectMember));
            _columnValues = new Dictionary<ColumnAttribute, object?>();
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            foreach (var item in from prop in _options.PropertyOptions
                                 join sel in selectMember on prop.PropertyInfo.Name equals sel
                                 select new { prop.ColumnAttribute , prop.PropertyInfo})
            {
                _columnValues.Add(item.ColumnAttribute, item.PropertyInfo.GetValue(entity));
            };
        }

        /// <summary>
        /// Build Query
        /// </summary>
        /// <returns>Implementation of the IQuery interface</returns>
        public UpdateQuery<T> Build()
        {
            return new UpdateQueryBuilder<T>(_options, Enumerable.Empty<string>(),_statements,  _columnValues).Build();
        }

        /// <summary>
        /// Add where statement in query
        /// </summary>
        /// <returns>Implementation of the IWhere interface</returns>
        public IWhere<T, UpdateQuery<T>> Where()
        {
            return new UpdateQueryBuilder<T>(_options, Enumerable.Empty<string>(), _statements, _columnValues).Where();
        }

        /// <summary>
        /// add to query update another column with value
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property</param>
        /// <param name="value"></param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>> Add<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            var (options, memberInfos) = expression.GetOptionsAndMember();
            var column = memberInfos.ValidateMemberInfo(options).ColumnAttribute;
            _columnValues.TryAdd(column, value);
            return this;
        }

        /// <summary>
        /// add to query update another column
        /// </summary>
        /// <typeparam name="TProperties">The property or properties for the query</typeparam>
        /// <param name="expression">The expression representing the property or properties</param>
        /// <returns>Instance of ISet</returns>
        public ISet<T, UpdateQuery<T>> Add<TProperties>(Expression<Func<T, TProperties>> expression)
        {
            if (_entity == null)
            {
                throw new InvalidOperationException(ErrorMessages.EntityNotFound);
            }

            var (options, memberInfos) = expression.GetOptionsAndMembers();
            memberInfos.ValidateMemberInfos($"Could not infer property name for expression. Please explicitly specify a property name by calling {options.Type.Name}.Update(x => x.{options.PropertyOptions.First().PropertyInfo.Name}) or {options.Type.Name}.Update(x => new {{ {string.Join(",", options.PropertyOptions.Select(x => $"x.{x.PropertyInfo.Name}"))} }})");

            foreach (var item in memberInfos)
            {
                var propertyOptions = item.ValidateMemberInfo(options);
                _columnValues.TryAdd(propertyOptions.ColumnAttribute, propertyOptions.GetValue(_entity));
            }

            return this;
        }
    }
}
