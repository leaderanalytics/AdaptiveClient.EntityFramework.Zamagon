using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFramework;
using Zamagon.Domain;

namespace Zamagon.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            // Autofac & AdaptiveClient
            IEnumerable<IEndPointConfiguration> EndPoints = EndPointUtilities.LoadEndPoints("bin\\debug\\netcoreapp2.0\\EndPoints.json");
            EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);

            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFramework.AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(builder);

            registrationHelper
                .RegisterEndPoints(EndPoints)
                .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());

            var container = builder.Build();


            // Create all databases or apply migrations
            foreach (IEndPointConfiguration ep in EndPoints.Where(x => x.EndPointType == EndPointType.DBMS))
            {
                //
                // Always resolve a new instance of databaseUtilties for each endPoint!
                //
                using (ILifetimeScope scope = container.BeginLifetimeScope())
                {
                    IDatabaseUtilities databaseUtilities = scope.Resolve<IDatabaseUtilities>();
                    databaseUtilities.CreateOrUpdateDatabase(ep).Wait();
                }
            }


            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
