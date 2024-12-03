using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GSqlQuery.Runner.DataBase
{
    internal class SafeConnectionHandler(DbConnection connection) : SafeHandle(IntPtr.Zero, true)
    {
        protected DbConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));

        public ITransaction Transaction { get; set; }

        public override bool IsInvalid => _connection == null;

        protected override bool ReleaseHandle()
        {
            try
            {
                Transaction?.Dispose();

                if (_connection?.State == ConnectionState.Open)
                {
                    _connection?.Close();
                }
                _connection?.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error releasing handle: {ex.Message}");
            }
            finally
            {
                _connection = null;
                Transaction = null;
            }

            return true;
        }
    }
}
