using CMS.GraphQL.ScalarTypes;
using CMS.Models.Definition;
using GraphQL.Types;

namespace CMS.GraphQL.Types
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
            Field(name: "properties", type: typeof(ListGraphType<DefinitionMemberType>), resolve: context=>context.Source.Properties);
        }
    }
}