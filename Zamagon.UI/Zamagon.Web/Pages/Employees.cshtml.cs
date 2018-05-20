using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;
using Zamagon.Domain.BackOffice;
using Zamagon.Model;

namespace Zamagon.Web.Pages
{
    public class EmployeesModel : BasePageModel
    {
        public List<Employee> Employees { get; set; }
        private IAdaptiveClient<IBOServiceManifest> serviceClient;

        public EmployeesModel(IAdaptiveClient<IBOServiceManifest> serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        public override async Task OnGetAsync()
        {
            await base.OnGetAsync();
            await GetEmployees();
        }

        public override async Task OnPostAsync()
        {
            await base.OnPostAsync();
            await GetEmployees();
        }

        private async Task GetEmployees()
        {
            CurrentEndPoint = GetEndPoints().FirstOrDefault(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataSource);

            if (CurrentEndPoint != null)
                Employees = await serviceClient.CallAsync(async x => await x.EmployeesService.GetEmployees(), CurrentEndPoint.Name);
        }
    }
}