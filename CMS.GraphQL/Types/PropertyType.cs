using System;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models.Entity;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class PropertyType : ObjectGraphType<Property>
    {
        public PropertyType()
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