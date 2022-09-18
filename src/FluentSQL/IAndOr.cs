﻿using FluentSQL.SearchCriteria;

namespace FluentSQL
{
    public interface IAndOr<TReturn> : ISearchCriteriaBuilder, IBuilder<TReturn>
    { 

    }

    /// <summary>
    /// AndOr
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IAndOr<T, TReturn> : IAndOr<TReturn>, IBuilder<TReturn> where T : class, new() where TReturn : IQuery
    {
    }

    public interface IAndOr<T, TReturn, TDbConnection> : IAndOr<T, TReturn>, IBuilder<TReturn> 
        where T : class, new() where TReturn : IQuery
    {
    }
}
