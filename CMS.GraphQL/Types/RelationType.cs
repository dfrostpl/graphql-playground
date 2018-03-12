using System.Linq;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models.Entity;
using CMS.Base.ProviderContracts;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class RelationType : ObjectGraphType<Relation>
    {
        public RelationType()
        {
            Field("name", r => r.Name).Description("Name of relation");
            Field(name: "relatedEntitiesIds", type: typeof(ListGraphType<GuidGraphType>), resolve: context => context.Source.RelatedEntitiesIds, description: "Related entities IDs");
            Field(name: "entities", type: typeof(ListGraphType<EntityType>), resolve: context =>
            {
                var repository = (IRepository)context.UserContext;
                return repository.Entities.Many(context.Source.RelateDefinitionId.Value).Where(entity => context.Source.RelatedEntitiesIds.Contains(entity.Id));
            }, description: "Related entities");
        }
    }
}