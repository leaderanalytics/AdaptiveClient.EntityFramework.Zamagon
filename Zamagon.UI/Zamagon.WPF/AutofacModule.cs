using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Zamagon.WPF 
{
    public class AutofacModule: Module
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
}
