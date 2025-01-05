using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner
{
    public interface ITypeHandler
    {
        void SetValueDataParameter(IDataParameter dataParameter, ParameterDetail parameterDetail);
    }

    public interface ITypeHandler<TDbDataReader> : ITypeHandler
        where TDbDataReader : DbDataReader
    {
        object GetValue(TDbDataReader reader, int ordinal);

        Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken);
    }
}