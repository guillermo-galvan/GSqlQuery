using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace GSqlQuery.Runner.TypeHandles
{
    public abstract class TypeHandler<TDbDataReader> : ITypeHandler<TDbDataReader>, ITypeHandler
        where TDbDataReader : DbDataReader
    {
        public virtual void SetValueDataParameter(IDataParameter dataParameter, ParameterDetail parameterDetail)
        {
            dataParameter.ParameterName = parameterDetail.Name;
            dataParameter.Value = parameterDetail.Value;
            SetDataType(dataParameter);
        }

        protected abstract void SetDataType(IDataParameter dataParameter);

        public abstract object GetValue(TDbDataReader reader, int ordinal);

        public abstract Task<object> GetValueAsync(TDbDataReader reader, int ordinal, CancellationToken cancellationToken);
    }
}