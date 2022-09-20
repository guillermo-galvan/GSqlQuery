using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    public interface IBuilder<TReturn> where TReturn : IQuery
    {
        TReturn Build();
    }
}
