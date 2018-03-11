using CMS.Models.Definition;
using GraphQL.Types;

namespace CMS.GraphQL.Types
{
    public class DefinitionMemberType : ObjectGraphType<DefinitionMember>
    {
        public DefinitionMemberType()
        {
            Field("name", dm => dm.Name).Description("Name of property");
        }
    }
}