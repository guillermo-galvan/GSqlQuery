using System.Threading;

namespace GSqlQuery.Extensions
{
    /// <summary>
    /// Helper to obtain the ids for the parameters
    /// </summary>
    internal static class Helpers
    {
        private static int _idParam = 0;

        /// <summary>
        /// Get parameter id
        /// </summary>
        /// <returns>Parameter Id</returns>
        internal static int GetIdParam()
        {
            var result = Interlocked.Increment(ref _idParam);

            if (result > int.MaxValue - 3000)
            {
                result = Interlocked.Exchange(ref _idParam, 0);
            }

            return result;
        }
    }
}
