using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using GraphQLDotNetCore.GraphQL.GraphQLSchema;
using GraphQLDotNetCore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace GraphQLDotNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("sqlConString")));

            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            #region GraphQL
            //dependencyresolver service
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<AppSchema>();

            //expose exceptions oluşan hataları expose etmeyecek sanırsam , addGraphTypes graphQL tipleri scoped 1 request süresince eklendi // graphQL servisi containera eklend
            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //request pipeline'a uygulama şeması middleware eklenir 
            //sanırsam şu şekil  gelen query schemaya göre resolve edilecek şuan şemada appQuery.cs ' e göre gelen queryler resolve ediliyor appQuery.cs de eklenen fieldlardaki query'e göre result döncek "owners" querysine bağlı olarak ownerlar dönecek. Dönen resultlar graphql typelarına resolve edilerek dönecek normal classlara karşılık gelen type classları bu yüzden var bunların hepsi IGraphQLType'ı implement ettiği için hangi entity kullanılırsa kullanılsın aynı obje tip dönecek
            //AppSchema => query resolve => AppQuery => OwnerType return resolved result to AppSchema query nibbas
            app.UseGraphQL<AppSchema>();
            //graphql kullanımı için arayüz bir nevi swagger 
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app.UseMvc();
        }
    }
}
