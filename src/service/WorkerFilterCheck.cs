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

namespace WorkerOE
{
    public class WorkerFilterCheck : BackgroundService
    {
        private readonly ILogger<WorkerFilterCheck> _logger;
        private readonly IConfiguration _configuration;
        public string _Mongo { get; set; }
        public string _connectionStringWap_Int { get; set; }
        public string _connectionStringDBSCEE { get; set; }

        public WorkerFilterCheck(ILogger<WorkerFilterCheck> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _Mongo = _configuration["Mongo:ConnectionString"];
            _connectionStringWap_Int = _configuration["Wap_Int:ConnectionString"];
            _connectionStringDBSCEE = _configuration["SCE:ConnectionString"];
            _logger.LogInformation("inicio: {time}", DateTimeOffset.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("ANTES DE COMPARAR: {time}", DateTimeOffset.Now);
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
                    List<CollectionMongo> ListPedMongo = await Mongo.GetMongoCollectionAsync(_Mongo);
                    WAP_INGRESOPEDIDOS wp = new WAP_INGRESOPEDIDOS();
                    List<WAP_INGRESOPEDIDOS> listWapReprocesar = new List<WAP_INGRESOPEDIDOS>();
                    List<CollectionMongo> listpApiProcesar = new List<CollectionMongo>(); int a = 0;
                    try
                    {
                        foreach (var listpedmongo in ListPedMongo)
                        {
                            a++;
                            /////////FIN MONGO
                            /////////WAP         
                            Wap_IngresosPedidosContext db = new Wap_IngresosPedidosContext(_connectionStringWap_Int);
                            
                                var encontroWap = Wap.GetWap(Convert.ToString(listpedmongo.idtransaccion),db);//PONER ALMACEN
                                if (encontroWap.Item1 > 0)
                                {
                                    wp.OrdenExterna1 = encontroWap.Item2; wp.Almacen = encontroWap.Item3; wp.RazonFalla = encontroWap.Item4; wp.Estado = encontroWap.Item5; wp.Propietario = encontroWap.Item6;

                                    /////////FIN WAP
                                    ///////SCE
                                    int encontroSCE = Sce.conectarSce(wp.OrdenExterna1, wp.Almacen, _connectionStringDBSCEE);
                                    if (encontroSCE == 0)//no encontro en sce pero si en wap
                                    {
                                        //reprocesar back
                                        listWapReprocesar.Add(new WAP_INGRESOPEDIDOS() { OrdenExterna1 = wp.OrdenExterna1, Almacen = wp.Almacen, Estado = wp.Estado, RazonFalla = wp.RazonFalla, Propietario = wp.Propietario });
                                    }
                                }
                                /////////FIN SCE}  
                                else
                                {
                                    listpApiProcesar.Add(new CollectionMongo() { idtransaccion = listpedmongo.idtransaccion, estado = listpedmongo.estado, razon = listpedmongo.razon });
                                    //reprocesar API o CLIENTE
                                }
                            }
                        
                        Console.WriteLine(a);
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("---------------Reprocesar API--------------------");
                        if (File.Exists("output.txt")) File.Delete("output.txt");
                        using StreamWriter streamwriterAPI = File.AppendText("output.txt");
                        foreach (var line in listpApiProcesar)
                        {
                            streamwriterAPI.WriteLine(line.estado, line.idtransaccion);
                            Console.WriteLine(line.idtransaccion + " " + line.estado + " " + line.razon);
                        }
                        streamwriterAPI.Close();
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine("---------------Reprocesar Back--------------------");
                        if (File.Exists("output.txt")) File.Delete("output.txt");
                        using StreamWriter streamwriterBack = File.AppendText("output.txt");
                        foreach (var line in listWapReprocesar)
                        {

                            streamwriterBack.WriteLine(line.OrdenExterna1, line.Almacen, line.Estado, line.RazonFalla);
                            Console.WriteLine("OrdenExterna: " + line.OrdenExterna1 + "  Propietario: " + line.Propietario + "  Almacen: " + line.Almacen + "  RazonFalla: " + line.RazonFalla);
                        }
                        streamwriterBack.Close();
                        //foreach (var item in listWapReprocesar)
                        //{
                        //    Console.WriteLine(item.OrdenExterna1,item.Almacen,item.Estado,item.RazonFalla);
                        //}
                        //Mail.Mail m = new Mail.Mail(_configuration);
                        //m.SendEmail(listWapReprocesar, "cliente");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
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
