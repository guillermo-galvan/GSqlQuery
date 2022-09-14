using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DatabaseManagmentEvents
    {
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
