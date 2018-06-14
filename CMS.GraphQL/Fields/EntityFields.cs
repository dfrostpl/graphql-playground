using System;
using CMS.Base.Abstractions;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.GraphQL.Types;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Fields
{
    public static class EntityFields
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
                    return repository.Entities.SingleAsync(id);
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
                        return repository.Entities.ManyAsync(definitionId.Value);
                    var definitionName = context.GetArgument<string>("definition");
                    return repository.Entities.ManyAsync(definitionName);
                });
        }
    }
}