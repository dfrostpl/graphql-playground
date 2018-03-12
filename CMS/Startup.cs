using System;
using System.Collections.Generic;
using CMS.Base.GraphQL.Queries;
using CMS.Base.GraphQL.Schemas;
using CMS.Base.GraphQL.Types;
using CMS.Base.Models.Definition;
using CMS.Base.ProviderContracts;
using CMS.Providers.SQLServer;
using GraphQL;
using GraphQL.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<EntityType>();
            services.AddSingleton<DefinitionType>();
            services.AddSingleton<PropertyType>();
            services.AddSingleton<Base.GraphQL.Types.RelationType>();
            services.AddSingleton<RelationDefinitionType>();
            services.AddSingleton<PropertyDefinitionType>();
            services.AddSingleton<BaseSchema>();
            services.AddSingleton<BaseQuery>();

            var repository = new SqlServerRepository();

            var avatarDefinition = repository.Definitions.Create("Avatar");
            avatarDefinition.Properties.Add(new PropertyDefinition
            {
                Name = "Link",
                Type = typeof(string)
            });
            repository.Definitions.Update(avatarDefinition);

            var userDefinition = repository.Definitions.Create("User");
            userDefinition.Properties.Add(new PropertyDefinition
            {
                Name = "Username",
                Type = typeof(string)
            });
            userDefinition.Properties.Add(new PropertyDefinition
            {
                Name = "DateOfBirth",
                Type = typeof(DateTime)
            });
            userDefinition.Properties.Add(new PropertyDefinition
            {
                Name = "Score",
                Type = typeof(int)
            });
            userDefinition.Relations.Add(new RelationDefinition
            {
                Name = "UserToAvatar",
                RelationType = RelationshipType.Parent,
                RelatedDefinitionId = avatarDefinition.Id,
                Cardinality = RelationCardinality.OneToOne
            });
            repository.Definitions.Update(userDefinition);

            var avatarEntity = repository.Entities.Create(avatarDefinition);
            avatarEntity.SetProperty("Link", "http://google.com/");
            repository.Entities.Update(avatarEntity);

            var userEntity = repository.Entities.Create(userDefinition);
            userEntity.SetProperty("Username", "Dmytro");
            userEntity.SetProperty("DateOfBirth", new DateTime(1996, 1, 10));
            userEntity.SetProperty("Score", 22);
            userEntity.SetRelations("UserToAvatar", new List<Guid>{avatarEntity.Id});
            repository.Entities.Update(userEntity);
            
            services.AddSingleton<IRepository, SqlServerRepository>(c=> repository);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}