using Autofac;
using CMS.Base.GraphQL.Queries;
using CMS.Base.GraphQL.Schemas;
using CMS.Base.GraphQL.Types;
using GraphQL;
using GraphQL.Http;

namespace CMS.Base.GraphQL
{
    public class GraphQLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>();
            builder.RegisterType<DocumentWriter>().As<IDocumentWriter>();
            builder.RegisterType<EntityType>();
            builder.RegisterType<DefinitionType>();
            builder.RegisterType<PropertyType>();
            builder.RegisterType<RelationType>();
            builder.RegisterType<RelationDefinitionType>();
            builder.RegisterType<PropertyDefinitionType>();
            builder.RegisterType<BaseSchema>();
            builder.RegisterType<BaseQuery>();
        }
    }
}