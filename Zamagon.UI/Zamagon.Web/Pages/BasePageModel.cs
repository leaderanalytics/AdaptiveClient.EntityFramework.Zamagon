using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

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
    }
}
