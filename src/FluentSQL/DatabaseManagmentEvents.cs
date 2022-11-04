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
        public virtual Func<Type,IEnumerable<ParameterDetail>, IEnumerable<IDataParameter>>? OnGetParameter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IEnumerable<IDataParameter> GetParameter<T>(IEnumerable<ParameterDetail> parameters) => OnGetParameter!(typeof(T),parameters);
    }
}
