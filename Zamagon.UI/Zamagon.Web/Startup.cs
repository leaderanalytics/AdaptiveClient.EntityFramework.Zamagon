using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
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
        private Process WebAPIProcess;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StartWebAPI();
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
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
            appLifetime.ApplicationStopping.Register(OnShutdown);
        }

        public static IEnumerable<IEndPointConfiguration> ReadEndPointsFromDisk()
        {
            // Please see ConnectionstringUtility class to add your login credentials for MySQL

            IEnumerable<IEndPointConfiguration> endPoints = EndPointUtilities.LoadEndPoints("bin\\debug\\netcoreapp2.1\\EndPoints.json");
            IEndPointConfiguration backOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL);
            IEndPointConfiguration frontOffice = endPoints.FirstOrDefault(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL);

            if (backOffice != null)
                backOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);

            if (frontOffice != null)
                frontOffice.ConnectionString = ConnectionstringUtility.BuildConnectionString(endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);

            return endPoints;
        }

        private void StartWebAPI()
        {
            System.IO.DirectoryInfo currentDir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.Parent;
            string apiDir = System.IO.Path.Combine(currentDir.FullName, "Zamagon.API");
            Process p = new Process();
            p.StartInfo.FileName = "dotnet";
            p.StartInfo.Arguments = $"run -p {apiDir} --launch - profile Zamagon.API";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            WebAPIProcess = p;
        }


        private void OnShutdown()
        {
            WebAPIProcess?.Kill();
        }
    }
}
