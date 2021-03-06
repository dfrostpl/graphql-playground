﻿using System;
using CMS.Base.Abstractions;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.GraphQL.Types;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Fields
{
    public static class DefinitionFields
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
                    return repository.Definitions.SingleAsync(id);
                });
        }

        public static void UseDefinitionsQuery(this ObjectGraphType query)
        {
            query.Field<ListGraphType<DefinitionType>>("definitions",
                resolve: context =>
                {
                    var repository = (IRepository)context.UserContext;
                    return repository.Definitions.ManyAsync();
                });
        }
    }
}