using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using WorkerOE.Models;

namespace WorkerOE
{
    class Wap_IngresosPedidosContext : DbContext
    {
        string _connectionString;
        public Wap_IngresosPedidosContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<WAP_INGRESOPEDIDOS> WAP_INGRESOPEDIDOS { get; set; }
    }
}
