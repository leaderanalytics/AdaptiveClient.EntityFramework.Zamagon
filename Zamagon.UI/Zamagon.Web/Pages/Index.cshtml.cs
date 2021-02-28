using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zamagon.Web.Pages
{
    public class IndexModel : PageModel
    {
        public virtual async Task OnGetAsync()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("dataSource")))
                HttpContext.Session.SetString("dataSource", "MSSQL");
        }
    }
}
