using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GSqlQuery
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseManagmentEvents
    {
        public bool IsTraceActive { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public virtual Func<Type, IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>> OnGetParameter { get; set; } = (t, p) => Enumerable.Empty<IDataParameter>();

        public virtual Action<bool, ILogger, string, object[]> OnWriteTrace { get; set; } = (isTraceActive, logger, message, param) =>
        {
            if (isTraceActive)
            {
                logger?.LogInformation(message, param);
            }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<IDataParameter> GetParameter<T>(IEnumerable<ParameterDetail> parameters) => OnGetParameter(typeof(T), parameters);

        public virtual void WriteTrace<T>(bool isTraceActive, ILogger logger, string message, object[] param) => OnWriteTrace(isTraceActive, logger, message, param);


    }
}
