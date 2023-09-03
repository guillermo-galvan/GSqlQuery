using GSqlQuery.Runner;
using GSqlQuery.Runner.Transforms;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GSqlQuery
{
    public class DatabaseManagementEvents
    {
        public bool IsTraceActive { get; set; } = false;

        public virtual Func<Type, IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>> OnGetParameter { get; set; } = (t, p) => Enumerable.Empty<IDataParameter>();

        public virtual Action<bool, ILogger, string, object[]> OnWriteTrace { get; set; } = (isTraceActive, logger, message, param) =>
        {
            if (isTraceActive)
            {
                logger?.LogInformation(message, param);
            }
        };

        public virtual IEnumerable<IDataParameter> GetParameter<T>(IEnumerable<ParameterDetail> parameters) => OnGetParameter(typeof(T), parameters);

        public virtual void WriteTrace(ILogger logger, string message, object[] param) => OnWriteTrace(IsTraceActive, logger, message, param);

        public virtual ITransformTo<T> GetTransformTo<T>(ClassOptions classOptions, IQuery<T> query, ILogger logger)
           where T : class, new()
        {
            if (query is JoinQuery<T> joinQuery)
            {
                return new JoinTransformTo<T>(classOptions.PropertyOptions.Count());
            }
            else if (!classOptions.IsConstructorByParam)
            {
                logger?.LogWarning("{0} constructor with properties {1} not found", classOptions.Type.Name,
                string.Join(", ", classOptions.PropertyOptions.Select(x => $"{x.PropertyInfo.Name}")));
                return new TransformToByField<T>(classOptions.PropertyOptions.Count());
            }
            else
            {
                return new TransformToByConstructor<T>(classOptions.PropertyOptions.Count());
            }
        }
    }
}