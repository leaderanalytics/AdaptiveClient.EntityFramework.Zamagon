using System;
using System.Collections.Generic;
using System.Text;
using LeaderAnalytics.AdaptiveClient.EntityFramework;
using Zamagon.Domain.BackOffice;

namespace Zamagon.Services.BackOffice
{
    public class BOServiceManifest : ServiceManifestFactory, IBOServiceManifest
    {
        public IEmployeesService EmployeesService { get => Create<IEmployeesService>(); }
        public ITimeCardsService TimeCardsService { get => Create<ITimeCardsService>(); }
    }
}
