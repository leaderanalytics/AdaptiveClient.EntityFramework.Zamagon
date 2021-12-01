namespace Zamagon.Web;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddDistributedMemoryCache();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.EnvironmentName == "Development")
            app.UseDeveloperExceptionPage();
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseSession();
        app.UseEndpoints(endpoints => endpoints.MapRazorPages());
    }


    // https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html
    // ConfigureContainer is where you can register things directly
    // with Autofac. This runs after ConfigureServices so the things
    // here will override registrations made in ConfigureServices.
    // Don't build the container; that gets done for you by the factory.
    public void ConfigureContainer(ContainerBuilder builder)
    {
        // Register your own things directly with Autofac here. Don't
        // call builder.Populate(), that happens in AutofacServiceProviderFactory
        // for you.
        builder.RegisterModule(new AutofacModule());
    }
}
