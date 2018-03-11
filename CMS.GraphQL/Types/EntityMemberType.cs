using System;
using CMS.GraphQL.ScalarTypes;
using CMS.Models.Entity;
using GraphQL.Types;

namespace CMS.GraphQL.Types
{
    public class EntityMemberType : ObjectGraphType<EntityMember>
    {
        public EntityMemberType()
        {
            Field("name", em => em.Name).Description("Name of property");
            Field(
                name: "value",
                resolve: context => Convert.ChangeType(context.Source.Value, context.Source.Type),
                description: "Value of property",
                type: typeof(EntityMemberGraphType));
            Field(name: "type", resolve: context => context.Source.Type, description: "Type of property", type: typeof(TypeGraphType));
        }
    }
}