namespace GSqlQuery.Extensions
{
    /// <summary>
    /// Helper to obtain the ids for the parameters
    /// </summary>
    internal static class Helpers
    {
        private static ulong _idParam = 0;

        /// <summary>
        /// Get parameter id
        /// </summary>
        /// <returns>Parameter Id</returns>
        internal static ulong GetIdParam()
        {
            if (_idParam > ulong.MaxValue - 2100)
            {
                _idParam = 0;
            }

            return _idParam++;
        }
    }
}
