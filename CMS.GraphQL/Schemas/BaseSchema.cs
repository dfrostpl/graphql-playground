using CMS.Base.GraphQL.Queries;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Schemas
{
    public class BaseSchema : Schema
    {
        public BaseSchema(BaseQuery query)
        {
            Query = query;
        }
    }
}