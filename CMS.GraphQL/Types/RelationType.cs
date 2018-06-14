using System.Linq;
using CMS.Base.Abstractions;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models.Entity;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class RelationType : ObjectGraphType<Relation>
    {
        public RelationType()
        {
            Field("name", r => r.Name).Description("Name of relation");
            Field(name: "relatedEntitiesIds", type: typeof(ListGraphType<GuidGraphType>), resolve: context => context.Source.ParentIds, description: "Related entities IDs");
            Field(name: "entities", type: typeof(ListGraphType<EntityType>), resolve: context =>
            {
                var repository = (IRepository)context.UserContext;
                return repository.Entities.ManyAsync(context.Source.RelatedDefinitionId, context.Source.ParentIds).Result;
            }, description: "Related entities");
        }
    }
}