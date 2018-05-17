using System;
using System.Collections.Generic;
using System.Text;

namespace Zamagon.Domain.BackOffice
{
    public interface IBOServiceManifest: IDisposable
    {
        IEmployeesService EmployeesService { get; }
        ITimeCardsService TimeCardsService { get; }
    }
}
