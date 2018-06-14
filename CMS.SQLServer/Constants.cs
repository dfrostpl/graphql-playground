namespace CMS.Providers.SQL
{
    public static class Constants
    {
        public static class Queries
        {
            public const string GetDefinitionById = "GetDefinitionById";
            public const string GetDefinitionByName = "GetDefinitionByName";
        }

        public static class Parameters
        {
            public const string DefinitionId = "definitionId";
            public const string DefinitionName = "definitionName";
            public const string ProcedureName = "procedureName";
        }
    }
}