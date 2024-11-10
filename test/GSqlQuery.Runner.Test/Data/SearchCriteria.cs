using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;

namespace GSqlQuery.Runner.Test.Data
{
    public class SearchCriteria : ISearchCriteria
    {
        public ColumnAttribute Column { get; }

        public TableAttribute Table { get; }

        public IFormats Formats { get; }

        public SearchCriteria(IFormats formats, TableAttribute table, ColumnAttribute columnAttribute)
        {
            Column = columnAttribute;
            Table  = table;
            Formats = formats;
        }

        public CriteriaDetail GetCriteria(IFormats formats, IEnumerable<PropertyOptions> propertyOptions)
        {
            return new CriteriaDetail(this, "SELECT COUNT([Test1].[Id]) FROM [Test1];", Array.Empty<ParameterDetail>());
        }
    }
}
