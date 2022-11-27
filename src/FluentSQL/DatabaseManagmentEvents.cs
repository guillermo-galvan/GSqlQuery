using FluentSQL.Default;
using Microsoft.Extensions.Logging;
using System.Data;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DatabaseManagmentEvents
    {
        public bool IsTraceActive { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public abstract Func<Type, IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>>? OnGetParameter { get; set; }

        public virtual Action<bool,ILogger?,string, object[]> OnWriteTrace { get; set; } = (isTraceActive, logger,message,param) => 
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
        public virtual IEnumerable<IDataParameter> GetParameter<T>(IEnumerable<ParameterDetail> parameters) => OnGetParameter!(typeof(T), parameters);

        public virtual void WriteTrace<T>(bool isTraceActive, ILogger? logger, string message, object[] param) => OnWriteTrace(isTraceActive, logger, message, param);


    }
}
