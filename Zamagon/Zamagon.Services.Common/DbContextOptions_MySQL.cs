﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LeaderAnalytics.AdaptiveClient.EntityFramework;

namespace Zamagon.Services.Common
{
    public class DbContextOptions_MySQL : IDbContextOptions
    {
        public DbContextOptions Options { get; set; }

        public DbContextOptions_MySQL(string connectionString)
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseMySQL(connectionString);
            Options = builder.Options;
        }
    }
}