namespace CMS.Providers.SQL.Queries
{
    public static class DefinitionQueries
    {
        public const string DefinitionByIdQuery = "SELECT * FROM Definitions WHERE Id = @Id";
        public const string DefinitionByNameQuery = "SELECT * FROM Definitions WHERE Name = @Name";
        public const string PropertyDefinitionsByDefinitionIdQuery = "SELECT * FROM PropertyDefinitions WHERE DefinitionId = @DefinitionId";
        public const string RelationDefinitionsByDefinitionIdQuery = "SELECT * FROM RelationDefinitions WHERE DefinitionId = @DefinitionId";
        public const string DefinitionsByIdsQuery = "SELECT * FROM Definitions WHERE Id IN @Ids";
        public const string AllDefinitions = "SELECT * FROM Definitions";
    }
}