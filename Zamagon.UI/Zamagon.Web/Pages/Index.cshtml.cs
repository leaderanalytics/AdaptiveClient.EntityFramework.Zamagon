using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zamagon.Web.Pages
{
    public class IndexModel : BasePageModel
    {
        public override async Task OnGetAsync()
        {
            await base.OnGetAsync();
        }


        public override async Task OnPostAsync()
        {
            await base.OnPostAsync();
        }
    }
}
