using System;

namespace CMS.Providers.SQL.Exceptions
{
    public class SqlProviderConfigurationException : Exception
    {
        public SqlProviderConfigurationException(string message) : base(message)
        {
        }
    }
}