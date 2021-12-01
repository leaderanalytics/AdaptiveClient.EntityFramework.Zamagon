namespace Zamagon.WPF;

public class AutofacModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<MainWindow>();
        builder.RegisterType<MainWindowViewModel>();
        builder.RegisterType<Views.OrdersViewModel>();
        builder.RegisterType<Views.HomeViewModel>();
        builder.RegisterType<Views.ProductsViewModel>();
        builder.RegisterType<Views.EmployeesViewModel>();
        builder.RegisterType<Views.TimecardsViewModel>();
        //builder.RegisterType<Views.OrdersView>().UsingConstructor(typeof(Views.OrdersViewModel));
    }
}
