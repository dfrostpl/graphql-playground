using GraphQL.Types;

namespace CMS.Base.GraphQL.Queries
{
    public class BaseQuery : ObjectGraphType
    {
        public BaseQuery()
        {
            this.UseEntityQuery();
            this.UseEntitiesQuery();
            this.UseDefinitionQuery();
            this.UseDefinitionsQuery();
        }
    }
}