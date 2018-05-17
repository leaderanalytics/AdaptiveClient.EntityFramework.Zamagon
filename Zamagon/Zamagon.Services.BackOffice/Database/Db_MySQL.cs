using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFramework;

namespace Zamagon.Services.BackOffice.Database
{
    public class Db_MySQL : Db, IMigrationContext
    {
        public Db_MySQL(DbContextOptions options) : base(options)
        {

        }
    }
}
