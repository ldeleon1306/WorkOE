using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkerOE.Models;
using static WorkerOE.Models.CollectionMongo;
using static WorkerOE.Models.Orders;
using static WorkerOE.Models.WAP_INGRESOPEDIDOS;

namespace WorkerOE.Functions
{
    public class SCEandWap
    {
        public void buscarSceWap(string _connectionStringWap_Int, string _connectionStringDBSCEE)
        {
            Wap_IngresosPedidosContext db = new Wap_IngresosPedidosContext(_connectionStringWap_Int);
            WAP_INGRESOPEDIDOS wp = new WAP_INGRESOPEDIDOS();
            ListaWap lwap = new ListaWap();
            ListaWap listawpElastic = new ListaWap();
            ListaMongo lmpp = new ListaMongo();
            LogElastic logElastic = new LogElastic();
            int a = 0;
            List<ListaWap> listafinalElastic = new List<ListaWap>();
            List<string> lista = new List<string>();
            try
            {
                foreach (var listpedmongo in Listas.ListaM)//recorro lista mongo
                {
                    a++;
                    var encontroWap = Wap.GetWap(Convert.ToString(listpedmongo.idtransaccion), db);//PONER ALMACEN
                    if (encontroWap.Item1 > 0)
                    {
                        ListasWapPedidos.ListaWapElas.Add(new ListaWap { OrdenExterna1 = encontroWap.Item2,IdTransacción=encontroWap.Item7, Almacen = encontroWap.Item3, RazonFalla = encontroWap.Item4, Estado = encontroWap.Item5,Propietario= encontroWap.Item6 });
                        List<Order> encontroSCE = Sce.ListarSce(encontroWap.Item2, encontroWap.Item3, _connectionStringDBSCEE);
                        if (encontroSCE != null && encontroSCE.Count > 0)//para el elastic
                        {
                            foreach (var item in encontroSCE)
                            {
                                ListaSCE.ListaSCEelast.Add(new Order { EXTERNORDERKEY = item.EXTERNORDERKEY, WHSEID = item.WHSEID, STATUS = item.STATUS });
                            }
                        }
                        else
                        {
                            ListasWapPedidos.ListaWapPed.Add(new ListaWap { OrdenExterna1 = encontroWap.Item2, Almacen = encontroWap.Item3, RazonFalla = encontroWap.Item4, Estado = encontroWap.Item5, Propietario = encontroWap.Item6 });
                        }                        
                    }
                    /////////FIN SCE}  
                    else
                    {
                        lmpp.idtransaccion = listpedmongo.idtransaccion; lmpp.estado = listpedmongo.estado; lmpp.razon = listpedmongo.razon;
                        Listas.ListaMrp.Add(lmpp);
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

                foreach (var line in Listas.ListaM)
                {
                    Console.WriteLine(line.idtransaccion + " " + line.estado + " " + line.razon);
                }
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("---------------Reprocesar Back--------------------");
                //if (File.Exists("output.txt")) File.Delete("output.txt");
                //using StreamWriter streamwriterBack = File.AppendText("output.txt");
                foreach (var line in ListasWapPedidos.ListaWapPed)
                {

                    //streamwriterBack.WriteLine(line.OrdenExterna1, line.Almacen, line.Estado, line.RazonFalla);
                    Console.WriteLine("OrdenExterna: " + line.OrdenExterna1 + "  Propietario: " + line.Propietario + "  Almacen: " + line.Almacen + "  RazonFalla: " + line.RazonFalla);
                }
                Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = "logeo-{0:yyyy.MM.dd}",
                    MinimumLogEventLevel = LogEventLevel.Information
                })
                .CreateLogger();
                Log.Information("{@ListaMongo}\n{@ListaWap}\n{@ListaSce}", Listas.ListaM, ListasWapPedidos.ListaWapElas, ListaSCE.ListaSCEelast);
                Log.CloseAndFlush();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
