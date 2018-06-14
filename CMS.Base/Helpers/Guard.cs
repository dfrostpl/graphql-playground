using System;

namespace CMS.Base.Helpers
{
    public static class Guard
    {
        public static void NotNull<T>(T obj, Exception ex) where T : new()
        {
            if (obj == null) throw ex;
        }

        public static void NotNull(string str, Exception ex)
        {
            if (string.IsNullOrEmpty(str)) throw ex;
        }

        public static void NotEmptyString(string str, Exception ex)
        {
            if (string.IsNullOrWhiteSpace(str)) throw ex;
        }
    }
}