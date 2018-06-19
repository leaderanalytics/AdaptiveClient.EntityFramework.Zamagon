using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.BackOffice;
using Zamagon.Domain.StoreFront;
using Zamagon.Model;

namespace Zamagon.WPF.Views
{
    public class EmployeesViewModel : BaseViewModel<Employee>
    {
        public EmployeesViewModel(IAdaptiveClient<ISFServiceManifest> storeFrontClient, IAdaptiveClient<IBOServiceManifest> backOfficeClient) :base(storeFrontClient, backOfficeClient)
        {
            EndPoints = new ObservableCollection<IEndPointConfiguration>(LoadEndPoints(API_Name.BackOffice));
        }

        public override async Task GetData(object arg)
        {
            await base.GetData(API_Name.BackOffice);
            BackOfficeServiceClient = Container.Resolve<IAdaptiveClient<IBOServiceManifest>>();
            List<Employee> emps = await BackOfficeServiceClient.TryAsync(async x => await x.EmployeesService.GetEmployees());
            emps.ForEach(x => Entities.Add(x));
            LogMessages.Add($"{emps.Count} rows retrieved from {BackOfficeServiceClient.CurrentEndPoint.Name}.");
            
        }
    }
}
