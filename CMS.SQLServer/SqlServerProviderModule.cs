using Autofac;
using CMS.Base.ProviderContracts;
using CMS.Providers.SQLServer.Configuration;
using CMS.Providers.SQLServer.Context;
using Microsoft.Extensions.Configuration;

namespace CMS.Providers.SQLServer
{
    public class SqlServerProviderModule : Module
    {
        private readonly IConfiguration _configuration;

        public SqlServerProviderModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var sqlServerConfiguration = new SqlServerConfiguration();
            _configuration.GetSection("sqlserver").Bind(sqlServerConfiguration);
            var dbContext = new SqlServerContextFactory(sqlServerConfiguration).CreateDbContext();

            builder.RegisterInstance(sqlServerConfiguration).SingleInstance().AsSelf();
            builder.RegisterInstance(dbContext).SingleInstance().AsSelf();
            builder.RegisterType<SqlServerRepository>().SingleInstance().As<IRepository>();

            //seed data
            //var avatarDefinition = repository.Definitions.Create("Avatar");
            //avatarDefinition.Properties.Add(new PropertyDefinition
            //{
            //    Name = "Link",
            //    Type = typeof(string)
            //});
            //repository.Definitions.Update(avatarDefinition);

            //var userDefinition = repository.Definitions.Create("User");
            //userDefinition.Properties.Add(new PropertyDefinition
            //{
            //    Name = "Username",
            //    Type = typeof(string)
            //});
            //userDefinition.Properties.Add(new PropertyDefinition
            //{
            //    Name = "DateOfBirth",
            //    Type = typeof(DateTime)
            //});
            //userDefinition.Properties.Add(new PropertyDefinition
            //{
            //    Name = "Score",
            //    Type = typeof(int)
            //});
            //userDefinition.Relations.Add(new RelationDefinition
            //{
            //    Name = "UserToAvatar",
            //    RelationType = RelationshipType.Parent,
            //    RelatedDefinitionId = avatarDefinition.Id,
            //    Cardinality = RelationCardinality.OneToOne
            //});
            //repository.Definitions.Update(userDefinition);

            //var avatarEntity = repository.Entities.Create(avatarDefinition);
            //avatarEntity.SetProperty("Link", "http://google.com/");
            //repository.Entities.Update(avatarEntity);

            //var userEntity = repository.Entities.Create(userDefinition);
            //userEntity.SetProperty("Username", "Dmytro");
            //userEntity.SetProperty("DateOfBirth", new DateTime(1996, 1, 10));
            //userEntity.SetProperty("Score", 22);
            //userEntity.SetRelations("UserToAvatar", new List<Guid> { avatarEntity.Id });
            //repository.Entities.Update(userEntity);
        }
    }
}