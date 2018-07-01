using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using LeaderAnalytics.AdaptiveClient;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;
using Zamagon.Domain;

namespace Zamagon.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
                //options.Cookie.HttpOnly = true;
            });

            // Autofac & AdaptiveClient
            IEnumerable<IEndPointConfiguration> endPoints = ReadEndPointsFromDisk();
            ContainerBuilder builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new LeaderAnalytics.AdaptiveClient.EntityFrameworkCore.AutofacModule());
            RegistrationHelper registrationHelper = new RegistrationHelper(builder);

            registrationHelper
                .RegisterEndPoints(endPoints)
                .RegisterModule(new Zamagon.Services.Common.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.BackOffice.AdaptiveClientModule())
                .RegisterModule(new Zamagon.Services.StoreFront.AdaptiveClientModule());

            
            var container = builder.Build();
            IDatabaseUtilities databaseUtilities = container.Resolve<IDatabaseUtilities>();
            
            // Create all databases or apply migrations
            foreach (IEndPointConfiguration ep in endPoints.Where(x => x.EndPointType == EndPointType.DBMS))
                databaseUtilities.CreateOrUpdateDatabase(ep).Wait();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSession();
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }

        public static IEnumerable<IEndPointConfiguration> ReadEndPointsFromDisk()
        {
            // Please see ConnectionstringUtility class to add your login credentials for MySQL

            IEnumerable<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints("bin\\debug\\netcoreapp2.0\\EndPoints.json");
            endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
            return endPoints;
        }
    }
}
