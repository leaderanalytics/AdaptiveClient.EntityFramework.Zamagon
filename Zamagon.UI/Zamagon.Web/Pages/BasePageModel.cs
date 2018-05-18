﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using LeaderAnalytics.AdaptiveClient;
using Zamagon.Domain;

namespace Zamagon.Web.Pages
{
    public abstract class BasePageModel : PageModel
    {
        public string DataSource
        {
            get => HttpContext.Session.GetString("dataSource");
            set => HttpContext.Session.SetString("dataSource", value);
        }
        

        public virtual async Task OnGetAsync()
        {
            if (DataSource == null)
                DataSource = "MSSQL";
        }

        public virtual async Task OnPostAsync()
        {
            DataSource = Request.Form["dataSources"];
        }


        public IEnumerable<IEndPointConfiguration> GetEndPoints()
        {
            IEnumerable<IEndPointConfiguration> endPoints = HttpContext.Session.GetObject<IEnumerable<EndPointConfiguration>>("endPoints");

            if (endPoints == null)
            {
                //endPoints = EndPointUtilities.LoadEndPoints("bin\\debug\\netcoreapp2.0\\EndPoints.json");
                //endPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.BackOffice && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
                //endPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString = ConnectionstringUtility.BuildConnectionString(EndPoints.First(x => x.API_Name == API_Name.StoreFront && x.ProviderName == DataBaseProviderName.MySQL).ConnectionString);
                endPoints = Startup.ReadEndPointsFromDisk();
                HttpContext.Session.SetObject("endPoints", endPoints);
            }
            return endPoints;
        }
    }
}
