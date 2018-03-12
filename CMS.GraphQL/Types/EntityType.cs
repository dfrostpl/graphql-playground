using System.Collections.Generic;
using System.Linq;
using CMS.Base.GraphQL.ScalarTypes;
using CMS.Base.Models.Entity;
using GraphQL.Types;

namespace CMS.Base.GraphQL.Types
{
    public class EntityType : ObjectGraphType<Entity>
    {
        public EntityType()
        {
            Name = "Entity";
            Field(e => e.Id, type: typeof(GuidGraphType)).Description("The id of the record");
            Field(e => e.CreatedAt).Description("Date of record creation");
            Field(e => e.ModifiedAt, true).Description("Date of last record modification");
            Field(e => e.DefinitionId, type: typeof(GuidGraphType)).Description("The id of definition");
            Field(
                name: "properties",
                type: typeof(ListGraphType<PropertyType>),
                description: "Properties",
                arguments: new QueryArguments(new QueryArgument<ListGraphType<StringGraphType>>
                {
                    Name = "load",
                    DefaultValue = new List<string>()
                }),
                resolve: context =>
                {
                    var propertiesToLoad = context.GetArgument<List<string>>("load");
                    return propertiesToLoad != null && propertiesToLoad.Any()
                        ? context.Source.Properties.Where(p => propertiesToLoad.Contains(p.Name))
                        : context.Source.Properties;
                });
            Field(
                name: "relations",
                type: typeof(ListGraphType<RelationType>),
                description: "Relations",
                arguments:new QueryArguments(new QueryArgument<ListGraphType<StringGraphType>>
                {
                    Name = "load",
                    DefaultValue = new List<string>()
                }),
                resolve: context =>
                {
                    var relationsToLoad = context.GetArgument<List<string>>("load");
                    return relationsToLoad != null && relationsToLoad.Any()
                        ? context.Source.Relations.Where(r => relationsToLoad.Contains(r.Name))
                        : context.Source.Relations;
                }
            );
        }
    }
}