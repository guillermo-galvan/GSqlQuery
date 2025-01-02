using GSqlQuery.Runner;
using GSqlQuery.Runner.Transforms;
using GSqlQuery.Runner.TypeHandles;
using System;
using System.Data.Common;
using System.Diagnostics;

namespace GSqlQuery
{
    public abstract class DatabaseManagementEvents
    {
        public bool IsTraceActive { get; set; } = false;

        private readonly Type _typeJoinTowTable = typeof(Join<,>);
        private readonly Type _typeJoinThreeTable = typeof(Join<,,>);

        public virtual void WriteTrace(string message, object[] param)
        {
            Debug.WriteLine("Message: {0}, Param {1}", message, param);
        }

        public virtual ITransformTo<T, TDbDataReader> GetTransformTo<T, TDbDataReader>(ClassOptions classOptions)
            where T : class
            where TDbDataReader : DbDataReader
        {
            Type type = typeof(T);
            Type gericDefinition = type.IsGenericType ? type.GetGenericTypeDefinition() : null;

            if (gericDefinition != null &&
                (_typeJoinTowTable == gericDefinition ||
                _typeJoinThreeTable == gericDefinition))
            {
                return new JoinTransformTo<T, TDbDataReader>(classOptions.PropertyOptions.Count, this);
            }
            else if (!classOptions.IsConstructorByParam)
            {
                return new TransformToByField<T, TDbDataReader>(classOptions.PropertyOptions.Count);
            }
            else
            {
                return new TransformToByConstructor<T, TDbDataReader>(classOptions.PropertyOptions.Count);
            }
        }

        public virtual ITypeHandler<TDbDataReader> GetHandler<TDbDataReader>(Type property) where TDbDataReader : DbDataReader
        {
            return GetTypeHandler<TDbDataReader>(property) ?? new DefaultNullableTypeHandler<TDbDataReader>(property);
        }

        protected abstract ITypeHandler<TDbDataReader> GetTypeHandler<TDbDataReader>(Type property) where TDbDataReader : DbDataReader;
    }
}