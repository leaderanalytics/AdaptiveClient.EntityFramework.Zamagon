using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFramework;

namespace Zamagon.Services.BackOffice.Database
{
    public class Db_MSSQL : Db, IMigrationContext
    {
        public Db_MSSQL(DbContextOptions options) : base(options)
        {
         
        }
    }
}
