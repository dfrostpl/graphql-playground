using CMS.GraphQL.Queries;
using GraphQL.Types;

namespace CMS.GraphQL.Schemas
{
    public class BaseSchema : Schema
    {
        public BaseSchema(BaseQuery query)
        {
            Query = query;
        }
    }
}