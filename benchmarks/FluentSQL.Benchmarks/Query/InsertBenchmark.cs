﻿using BenchmarkDotNet.Attributes;
using FluentSQL.Benchmarks.Data;

namespace FluentSQL.Benchmarks.Query
{
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class Insert : CreateStaments
    {
        protected User _user;

        public Insert() : base()
        {
            _user = new User();
        }

        [Benchmark]
        public IQuery GenerateQuery()
        {
            return _user.Insert(_statements).Build();
        }
    }
}