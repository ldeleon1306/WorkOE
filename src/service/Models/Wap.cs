using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkerOE.Models
{
    public class Wap
    {
        internal static (int, string, string, string, int, string) GetWap(string idtransaccion, Wap_IngresosPedidosContext db)
        {
            WAP_INGRESOPEDIDOS wp = new WAP_INGRESOPEDIDOS();
            int a = 0;
            try
            {
                var idlist = new int[] { 2, 3 };
                //Console.WriteLine("antes  from p in db.WAP_INGRESOPEDIDOS");
                var estados = from p in db.WAP_INGRESOPEDIDOS
                              where p.IdTransacción == idtransaccion && idlist.Contains(p.Estado)
                              select new WAP_INGRESOPEDIDOS()
                              {
                                  IdTransacción = p.IdTransacción,
                                  Estado = p.Estado,
                                  Propietario = p.Propietario,
                                  RazonFalla = p.RazonFalla,
                                  Almacen = p.Almacen,
                                  OrdenExterna1 = p.OrdenExterna1
                              };
                //Console.WriteLine("despues  from p in db.WAP_INGRESOPEDIDOS");
                //Console.WriteLine(estados.Count());
                try
                {
                    a = estados.Count();
                    if (estados.Count() > 0)//busco si quedaron solo con error
                    {
                        foreach (var item in estados)
                        {
                            wp.OrdenExterna1 = item.OrdenExterna1; wp.Almacen = item.Almacen; wp.RazonFalla = item.RazonFalla; wp.Estado = item.Estado; wp.Propietario = item.Propietario;

                            if (item.Estado == 2) { break; };//corto el foreach si encuentro el estado aceptado
                                                             //if(item.Estado == 3) { }
                        }                        
                    }
                    else
                    {
                       //agregar lista reprocesar back
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("ex 57 wap");
                Console.WriteLine(ex.Message);
                //throw;
            }
            return (a, wp.OrdenExterna1, wp.Almacen, wp.RazonFalla, wp.Estado,wp.Propietario);


        }
    }
}

