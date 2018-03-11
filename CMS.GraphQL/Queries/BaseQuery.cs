using System;
using CMS.Base.Data;
using CMS.GraphQL.ScalarTypes;
using CMS.GraphQL.Types;
using GraphQL.Types;

namespace CMS.GraphQL.Queries
{
    public class BaseQuery : ObjectGraphType
    {
        public BaseQuery(IRepository repository)
        {
            Field<EntityType>("entity",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<GuidGraphType>> {Name = "id"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return repository.Entities.Single(id);
                });
            Field<ListGraphType<EntityType>>("entities",
                arguments: new QueryArguments(
                  new QueryArgument<GuidGraphType>{ Name = "definitionId"},
                  new QueryArgument<StringGraphType> { Name = "definition"}
                ),
                resolve: context =>
                {
                    var definitionId = context.GetArgument<Guid?>("definitionId");
                    if(definitionId.HasValue && definitionId != Guid.Empty)
                        return repository.Entities.Many(definitionId.Value);
                    var definitionName = context.GetArgument<string>("definition");
                    return repository.Entities.Many(definitionName);
                });

            Field<DefinitionType>("definition",
                arguments: new QueryArguments(
                    new QueryArgument<GuidGraphType>
                    {
                        Name = "id"
                    }
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    return repository.Definitions.Single(id);
                });

            Field<ListGraphType<DefinitionType>>("definitions",
                resolve: context => repository.Definitions.Many()
            );
        }
    }
}