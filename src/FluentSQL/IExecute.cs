using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    public interface IExecute
    { 
        object? Exec();

        object? Exec(DbConnection connection);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IExecute<TResult> : IExecute
    {
        new TResult Exec();

        new TResult Exec(DbConnection connection);
    }
}
