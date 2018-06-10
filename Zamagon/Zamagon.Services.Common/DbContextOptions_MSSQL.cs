using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFrameworkCore;

namespace Zamagon.Services.Common
{
    public class DbContextOptions_MSSQL : IDbContextOptions
    {
        public DbContextOptions Options { get; set; }

        public DbContextOptions_MSSQL(string connectionString)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(connectionString);
            Options = builder.Options;
        }
    }
}
