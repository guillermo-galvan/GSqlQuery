using GSqlQuery.Queries;
using GSqlQuery.Test.Models;
using System;

namespace GSqlQuery.Test
{
    internal static class DynamicQueryCreate
    {
        public static DynamicQuery Create<TProperties>(Func<Test1, TProperties> func)
        {
            return new DynamicQuery(typeof(Test1), typeof(TProperties));
        }
    }
}
