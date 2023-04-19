using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerOE;
using WorkerOE.Models;
using WorkerOE.Mail;
using static WorkerOE.Models.CollectionMongo;

namespace WorkerOE
{
    public class WorkerFilterCheck : BackgroundService
    {
        private readonly ILogger<WorkerFilterCheck> _logger;
        private readonly IConfiguration _configuration;
        public string _Mongo { get; set; }
        public string _connectionStringWap_Int { get; set; }
        public string _connectionStringDBSCEE { get; set; }
        private string _smtpServer { get; set; }
        private string _mailTo { get; set; }
        private string _mailFrom { get; set; }
        Mongo m = new Mongo();
        Functions.SCEandWap functions = new Functions.SCEandWap();
        WAP_INGRESOPEDIDOS wp = new WAP_INGRESOPEDIDOS();
        List<WAP_INGRESOPEDIDOS> listWapReprocesar = new List<WAP_INGRESOPEDIDOS>();
        List<CollectionMongo> listpApiProcesar = new List<CollectionMongo>();

        public WorkerFilterCheck(ILogger<WorkerFilterCheck> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _Mongo = _configuration["Mongo:ConnectionString"];
            _connectionStringWap_Int = _configuration["Wap_Int:ConnectionString"];
            _connectionStringDBSCEE = _configuration["SCE:ConnectionString"];
            _logger.LogInformation("inicio: {time}", DateTimeOffset.Now);
            _smtpServer = _configuration["MailSmtp:smtp"];
            _mailTo = _configuration["MailSmtp:mailTo"];
            _mailFrom = _configuration["MailSmtp:mailFrom"];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {                    
                    CompareAsync();
                    await Task.Delay(3000000, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                    throw;
                }

            }
        }

        public async Task CompareAsync()
        {
            try
            {
                try
                {
                    /////////MONGO             
                    m.ListaMongo(_Mongo);   //cargo mongo   
                    functions.buscarSceWap(_connectionStringWap_Int, _connectionStringDBSCEE);                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                throw;
            }
        }       
    }
}
