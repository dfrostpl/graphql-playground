using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models.Definition;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class PropertyDefinitionType : ObjectGraphType<PropertyDefinition>
    {
        public PropertyDefinitionType()
        {
            Field("name", dm => dm.Name).Description("Name of property");
            Field(name: "type", type: typeof(TypeGraphType), resolve: context => context.Source.Type, description: "Type");
        }
    }
}