using System.Data;
using System.Data.Common;

namespace GSqlQuery.Runner
{
    public interface ITypeHandler
    {
        void SetValueDataParameter(IDataParameter dataParameter, ParameterDetail parameterDetail);
    }

    public interface ITypeHandler<TDbDataReader> : ITypeHandler
        where TDbDataReader : DbDataReader
    {
        object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail);
    }
}