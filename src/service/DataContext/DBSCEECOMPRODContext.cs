using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerOE.Models;

namespace WorkerOE.DataContext
{
    class DBSCEECOMPRODContext: DbContext
    {
        private readonly ILogger<DBSCEECOMPRODContext> _logger;
        private readonly IConfiguration _configuration;
        public string _connectionString { get; set; }        

        public DBSCEECOMPRODContext(ILogger<DBSCEECOMPRODContext> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration["SCE:ConnectionString"];
            _logger.LogInformation("inicio: {time}", DateTimeOffset.Now);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);           
        }

        public DbSet<Orders> ORDERS { get; set; }
    }
}
