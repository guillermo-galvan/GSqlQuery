﻿using System.Collections.Generic;

namespace GSqlQuery
{
    public interface IQuery
    {
        /// <summary>
        /// Query Text
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Columns of the query
        /// </summary>
        IEnumerable<PropertyOptions> Columns { get; }

        /// <summary>
        /// Query criteria
        /// </summary>
        IEnumerable<CriteriaDetail> Criteria { get; }
    }

    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public interface IQuery<T, TQueryOptions> : IQueryOptions<TQueryOptions>, IQuery 
        where T : class
        where TQueryOptions : QueryOptions
    {
        
    }
}