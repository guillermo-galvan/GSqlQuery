using System.Data;
using System.Data.Common;

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

        public abstract object GetValue(TDbDataReader reader, DataReaderPropertyDetail dataReaderPropertyDetail);
    }
}