using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Extensions
{
    internal static class IDatabaseManagmentExtension
    {
        internal static void ValidateDatabaseManagment(this IDatabaseManagment databaseManagment)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            databaseManagment.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment));
            databaseManagment.Events.NullValidate(ErrorMessages.ParameterNotNull, nameof(ConnectionOptions.DatabaseManagment.Events));
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
