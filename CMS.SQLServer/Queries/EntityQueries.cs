namespace CMS.Providers.SQL.Queries
{
    public static class EntityQueries
    {
        public const string EntityByIdQuery = "SELECT * FROM Entities WHERE Id = @Id";
        public const string PropertiesByEntityIdQuery = "SELECT * FROM Properties WHERE EntityId = @Id";
        public const string FilteredPropertiesByEntityIdQuery = "SELECT * FROM Properties WHERE EntityId = @Id AND Name in @Names";
        public const string RelationsByEntityIdQuery = "SELECT * FROM Relations WHERE EntityId = @Id";
        public const string FilteredRelationsByEntityIdQuery = "SELECT * FROM Relations WHERE EntityId = @Id AND Name in @Names";
        public const string EntitiesByIdsQuery = "SELECT * FROM Entities WHERE Id in @Ids";
        public const string EntitiesByDefinitionIdQuery = "SELECT * FROM Entities WHERE DefinitionId = @DefinitionId";
    }
}