using GSqlQuery.Queries;
using GSqlQuery.Runner.Test.Models;
using System;

namespace GSqlQuery.Runner.Test
{
    internal static class DynamicQueryCreate
    {
        public static DynamicQuery Create<TProperties>(Func<Test1, TProperties> func)
        {
            return new DynamicQuery(typeof(Test1), typeof(TProperties));
        }
    }
}