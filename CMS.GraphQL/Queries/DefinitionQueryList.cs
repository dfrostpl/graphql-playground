using System;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.GraphQL.Types;
using CMS.Base.ProviderContracts;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Queries
{
    public static class DefinitionQueryList
    {
        public static void UseDefinitionQuery(this ObjectGraphType query)
        {
            query.Field<DefinitionType>("definition",
                arguments: new QueryArguments(
                    new QueryArgument<GuidGraphType>
                    {
                        Name = "id"
                    }
                ),
                resolve: context =>
                {
                    var repository = (IRepository)context.UserContext;
                    var id = context.GetArgument<Guid>("id");
                    return repository.Definitions.Single(id);
                });
        }

        public static void UseDefinitionsQuery(this ObjectGraphType query)
        {
            query.Field<ListGraphType<DefinitionType>>("definitions",
                resolve: context =>
                {
                    var repository = (IRepository)context.UserContext;
                    return repository.Definitions.Many();
                });
        }
    }
}