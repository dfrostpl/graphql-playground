using System;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.GraphQL.Types;
using CMS.Base.ProviderContracts;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Queries
{
    public static class EntityQueryList
    {
        public static void UseEntityQuery(this ObjectGraphType query)
        {
            query.Field<EntityType>("entity",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GuidGraphType>> {Name = "id"}
                ),
                resolve: context =>
                {
                    var repository = (IRepository)context.UserContext;
                    var id = context.GetArgument<Guid>("id");
                    return repository.Entities.Single(id);
                });
        }

        public static void UseEntitiesQuery(this ObjectGraphType query)
        {
            query.Field<ListGraphType<EntityType>>("entities",
                arguments: new QueryArguments(
                    new QueryArgument<GuidGraphType> {Name = "definitionId"},
                    new QueryArgument<StringGraphType> {Name = "definition"}
                ),
                resolve: context =>
                {
                    var repository = (IRepository)context.UserContext;
                    var definitionId = context.GetArgument<Guid?>("definitionId");
                    if (definitionId.HasValue && definitionId != Guid.Empty)
                        return repository.Entities.Many(definitionId.Value);
                    var definitionName = context.GetArgument<string>("definition");
                    return repository.Entities.Many(definitionName);
                });
        }
    }
}