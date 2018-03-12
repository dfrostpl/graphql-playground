using CMS.Base.GraphQL.Fields;
using CMS.Base.Models.Definition;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class DefinitionType : ObjectGraphType<Definition>
    {
        public DefinitionType()
        {
            Name = "Definition";
            this.UseDbObjectFields();
            Field(d => d.Name).Description("Definition name");
            Field(name: "properties", type: typeof(ListGraphType<PropertyDefinitionType>), resolve: context => context.Source.Properties);
            Field(name: "relations", type: typeof(ListGraphType<RelationDefinitionType>), resolve: context => context.Source.Relations);
        }
    }
}