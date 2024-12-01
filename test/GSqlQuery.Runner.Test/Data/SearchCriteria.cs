using GSqlQuery.Runner.Test.Models;
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

        public object Value => throw new NotImplementedException();

        public ClassOptions ClassOptions => throw new NotImplementedException();

        public SearchCriteria(IFormats formats, TableAttribute table, ColumnAttribute columnAttribute)
        {
            Column = columnAttribute;
            Table  = table;
            Formats = formats;
        }

        public CriteriaDetailCollection GetCriteria(ref uint parameterId)
        {
            var tmp = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            CriteriaDetails criterion = new CriteriaDetails("SELECT COUNT([Test1].[Id]) FROM [Test1];", []);
            return new CriteriaDetailCollection(this, criterion.Criterion, tmp.PropertyOptions.First(), criterion.Parameters);
        }

        public CriteriaDetailCollection ReplaceValue(CriteriaDetailCollection criteriaDetailCollection)
        {
            throw new NotImplementedException();
        }
    }
}
