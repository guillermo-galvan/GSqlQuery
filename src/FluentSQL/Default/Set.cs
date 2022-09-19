using FluentSQL.Extensions;
using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Linq.Expressions;

namespace FluentSQL.Default
{
    internal abstract class Set<T, TReturn> : ISet<T, TReturn> where T : class, new() where TReturn : IQuery
    {
        protected readonly Dictionary<ColumnAttribute, object?> _columnValues;
        protected readonly object? _entity;

        public IDictionary<ColumnAttribute, object?> ColumnValues => _columnValues;

        public Set(IEnumerable<string> selectMember, object? value)
        {
            selectMember = selectMember ?? throw new ArgumentNullException(nameof(selectMember));
            _columnValues = new Dictionary<ColumnAttribute, object?>();
            foreach (ColumnAttribute item in ClassOptionsFactory.GetClassOptions(typeof(T)).GetColumnsQuery(selectMember))
            {
                _columnValues.Add(item, value);
            };
        }

        public Set(object? entity, IEnumerable<string> selectMember)
        {
            selectMember = selectMember ?? throw new ArgumentNullException(nameof(selectMember));
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _columnValues = new Dictionary<ColumnAttribute, object?>();
            foreach (var item in from prop in ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions
                                 join sel in selectMember on prop.PropertyInfo.Name equals sel
                                 select new { prop.ColumnAttribute, prop.PropertyInfo })
            {
                _columnValues.Add(item.ColumnAttribute, item.PropertyInfo.GetValue(entity));
            };
        }

        public ISet<T, TReturn> Add<TProperties>(Expression<Func<T, TProperties>> expression, TProperties value)
        {
            var (options, memberInfos) = expression.GetOptionsAndMember();
            var column = memberInfos.ValidateMemberInfo(options).ColumnAttribute;
            _columnValues.TryAdd(column, value);
            return this;
        }

        public ISet<T, TReturn> Add<TProperties>(Expression<Func<T, TProperties>> expression)
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

        public abstract TReturn Build();

        public abstract IWhere<T, TReturn> Where();
    }

    /// <summary>
    /// Base class to generate the set query
    /// </summary>
    /// <typeparam name="T">The type of object from which the query is generated</typeparam>
    internal class Set<T> : Set<T, UpdateQuery<T>> where T : class, new()
    {
        private readonly IStatements _statements;

        public Set(IEnumerable<string> selectMember, IStatements statements, object? value) : base(selectMember, value)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public Set(object? entity, IEnumerable<string> selectMember, IStatements statements) : base(entity, selectMember)
        {
            _statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }

        public override UpdateQuery<T> Build()
        {
            return new UpdateQueryBuilder<T>(_statements, ColumnValues).Build();
        }

        public override IWhere<T, UpdateQuery<T>> Where()
        {
            return new UpdateQueryBuilder<T>(_statements, ColumnValues).Where();
        }

    }

    internal class SetExecute<T, TDbConnection> : Set<T, UpdateQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly ConnectionOptions<TDbConnection> _connectionOptions;

        public SetExecute(IEnumerable<string> selectMember, ConnectionOptions<TDbConnection> connectionOptions, object? value) : base(selectMember, value)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        public SetExecute(object? entity, IEnumerable<string> selectMember, ConnectionOptions<TDbConnection> connectionOptions) : base(entity, selectMember)
        {
            _connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
        }

        public override UpdateQuery<T, TDbConnection> Build()
        {
            return new UpdateQueryBuilder<T,TDbConnection>(_connectionOptions, ColumnValues).Build();
        }

        public override IWhere<T, UpdateQuery<T, TDbConnection>> Where()
        {
            return new UpdateQueryBuilder<T,TDbConnection>(_connectionOptions, ColumnValues).Where();
        }
    }
}
