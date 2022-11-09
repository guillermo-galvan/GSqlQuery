using FluentSQL.Helpers;
using FluentSQL.SearchCriteria;

namespace FluentSQL.Default
{
    /// <summary>
    /// Update where 
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class UpdateWhere<T> : BaseWhere<T, UpdateQuery<T>>, ISearchCriteriaBuilder<T, UpdateQuery<T>>, IWhere<T, UpdateQuery<T>>, IAndOr<T, UpdateQuery<T>> where T : class, new()
    {
        private readonly UpdateQueryBuilder<T> _queryBuilder;

        /// <summary>
        /// Initializes a new instance of the UpdateWhere class.
        /// </summary>
        /// <param name="queryBuilder">UpdateQueryBuilder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UpdateWhere(UpdateQueryBuilder<T> queryBuilder) : base()
        {
            _queryBuilder = queryBuilder ?? throw new ArgumentNullException(nameof(queryBuilder));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override UpdateQuery<T> Build()
        {
            return _queryBuilder.Build();
        }
    }

    internal class UpdateWhere<T, TDbConnection> : BaseWhere<T, UpdateQuery<T, TDbConnection>>, ISearchCriteriaBuilder<T, UpdateQuery<T, TDbConnection>>,
        IWhere<T, UpdateQuery<T, TDbConnection>>,
        IAndOr<T,UpdateQuery<T, TDbConnection>> where T : class, new()
    {
        private readonly UpdateQueryBuilder<T, TDbConnection> _queryBuilder;

        public UpdateWhere(UpdateQueryBuilder<T, TDbConnection> queryBuilder) : base()
        {
            _queryBuilder = queryBuilder;
        }

        public override UpdateQuery<T, TDbConnection> Build()
        {
            return _queryBuilder.Build();
        }
    }
}
