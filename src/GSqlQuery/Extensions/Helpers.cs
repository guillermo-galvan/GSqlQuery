namespace GSqlQuery.Extensions
{
    internal static class Helpers
    {
        private static ulong _idParam = 0;

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
