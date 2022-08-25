using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    public class Execute
    {
        public static ContinuousExecution ContinuousExecutionFactory(ConnectionOptions connectionOptions)
        {
            return new ContinuousExecution(connectionOptions);
        }
    }
}
