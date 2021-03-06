﻿using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models.Definition;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class RelationDefinitionType : ObjectGraphType<RelationDefinition>
    {
        public RelationDefinitionType()
        {
            Field("name", r => r.Name).Description("Relation name");
            Field(name: "relatedDefinitionId", type: typeof(GuidGraphType), resolve: context => context.Source.RelatedDefinitionId, description: "Definition ID of related record");
            Field(name: "cardinality", type: typeof(EnumerationGraphType<RelationCardinality>), resolve: context => context.Source.Cardinality, description: "Cardinality");
            Field(name: "role", type: typeof(EnumerationGraphType<RelationRole>), resolve: context => context.Source.Role, description: "Relation role");
        }
    }
}