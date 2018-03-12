using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models.Definition;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class DefinitionType : ObjectGraphType<Definition>
    {
        public DefinitionType()
        {
            Name = "Definition";
            Field(e => e.Id, type: typeof(GuidGraphType)).Description("The id of the record");
            Field(e => e.CreatedAt).Description("Date of record creation");
            Field(e => e.ModifiedAt, true).Description("Date of last record modification");
            Field(d => d.Name).Description("Definition name");
            Field(name: "properties", type: typeof(ListGraphType<PropertyDefinitionType>), resolve: context=>context.Source.Properties);
            Field(name: "relations", type: typeof(ListGraphType<RelationDefinitionType>), resolve: context => context.Source.Relations);
        }
    }
}