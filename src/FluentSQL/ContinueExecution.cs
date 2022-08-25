using FluentSQL.Extensions;
using FluentSQL.Internal;
using FluentSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL
{
    public sealed class ContinueExecution<TResult>
    {
        private readonly ConnectionOptions _connectionOptions;
        internal readonly ContinueExecutionResult _continueExecutionResult;

        internal ContinueExecution(ConnectionOptions connectionOptions, ContinueExecutionResult continueExecution)
        {
            _connectionOptions = connectionOptions;
            _continueExecutionResult = continueExecution;
        }

        public ContinueExecution<TNewResult> ContinueWith<TNewResult>(Func<ConnectionOptions, TResult, IExecute<TNewResult>> exec)
        {
            exec.NullValidate(ErrorMessages.ParameterNotNull, nameof(exec));
            _continueExecutionResult.Add(new ContinueExecutionResult<TResult, TNewResult>(_connectionOptions, exec));
            return new ContinueExecution<TNewResult>(_connectionOptions, _continueExecutionResult);
        }

        private void ValidateDatabaseManagment()
        {
            _connectionOptions.DatabaseManagment.ValidateDatabaseManagment();
        }

        public TResult? Start()
        {
            ValidateDatabaseManagment();
            
            using DbConnection connection = _connectionOptions.DatabaseManagment.GetConnection();

            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            TResult? result = Start(connection);
            connection.Close();
            return result;
        }

        public TResult? StartWithTransaction()
        {
            ValidateDatabaseManagment();
            TResult? result = default;
            using DbConnection connection = _connectionOptions.DatabaseManagment.GetConnection();
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            using DbTransaction transaction = connection.BeginTransaction();
            
            if (transaction != null && transaction.Connection != null)
            {
                result = Start(transaction.Connection);
                transaction.Commit();
            }

            
            connection.Close();
            return result;
        }

        public TResult? Start(DbConnection connection)
        {
            TResult? result = (TResult?)_continueExecutionResult.Start(connection);            
            return result;
        }
    }
}
