using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Fields
{
    public static class DefaultFields
    {
        public static void UseDbObjectFields<T>(this ObjectGraphType<T> query) where T : DbObject
        {
            query.Field(e => e.Id, type: typeof(GuidGraphType)).Description("The id of the record");
            query.Field(e => e.CreatedAt).Description("Date of record creation");
            query.Field(e => e.ModifiedAt, true).Description("Date of last record modification");
        }
    }
}