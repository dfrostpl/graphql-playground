using System;
using CMS.Base.Data;
using CMS.GraphQL.Queries;
using CMS.GraphQL.Schemas;
using CMS.GraphQL.Types;
using CMS.Models.Definition;
using CMS.SQLServer;
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
            services.AddSingleton<EntityMemberType>();
            services.AddSingleton<BaseSchema>();
            services.AddSingleton<BaseQuery>();

            var repository = new SqlServerRepository();
            var userDefinition = repository.Definitions.Create("User");
            userDefinition.Properties.Add(new DefinitionMember
            {
                Name = "Username",
                Type = typeof(string)
            });
            userDefinition.Properties.Add(new DefinitionMember
            {
                Name = "DateOfBirth",
                Type = typeof(DateTime)
            });
            userDefinition.Properties.Add(new DefinitionMember
            {
                Name = "Score",
                Type = typeof(int)
            });
            repository.Definitions.Update(userDefinition);

            var userEntity = repository.Entities.Create(userDefinition);
            userEntity.SetProperty("Username", "Dmytro");
            userEntity.SetProperty("DateOfBirth", new DateTime(1996, 1, 10));
            userEntity.SetProperty("Score", 22);
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