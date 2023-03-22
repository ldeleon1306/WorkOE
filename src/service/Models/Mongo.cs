using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using static WorkerOE.Models.CollectionMongo;


namespace WorkerOE.Models
{
    public class Mongo
    {     

        internal static async Task<List<CollectionMongo>> GetMongoCollectionAsync(string MongoconnectionString)
        {         
            List<CollectionMongo> listRange = new List<CollectionMongo>();
            try
            {
                var client = new MongoClient(MongoconnectionString);
                List<string> NombrebaseDatos = client.ListDatabaseNames().ToList();
                var database = client.GetDatabase("APIAlmacenes");

                var collection = database.GetCollection<CollectionMongo>("TransaccionesPedidos");



                //foreach (var item in resultssss)
                //{

                //}
                var Request = database.GetCollection<CollectionMongo.Request>("TransaccionesPedidos");
                var respone = database.GetCollection<CollectionMongo.Response>("Id.Pedido.propietario");
                var root = database.GetCollection<CollectionMongo.Root>("TransaccionesPedidos");
                var resultsss = Request.Find(x => x.pedido.remito == "").FirstOrDefault();


                //    CollectionMongo results =
                //from movie in Request.AsQueryable()
                //where movie.almacen.Contains("Ryan Reynolds")
                //from cast in Pedido.AsQueryable()
                //where cast.propietario == "NARANJA"
                //group movie by cast into g
                //select new { Cast = g.Key, Sum = g.Sum(x => x.pedido) };



                //var q = from channel in Request.AsQueryable()
                //        from episode in channel.
                //        .Where(x => x.propietario == "NARANJA")
                //        select new requesta()
                //        {
                //            propietario = episode.propietario
                //        };

                //var min = new DateTime(2023, 01, 01, 22, 0, 0);
                //var max = (new DateTime(2023, 01, 20, 23, 0, 0));

                //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
                string dateTime = "2023-03-22T00:00:00.000+00:00";
                DateTime dt = Convert.ToDateTime(dateTime);
                //string dateTimes = "2023-02-01";
                //DateTime dts = Convert.ToDateTime(dateTimes);
                string nuevo = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                DateTime s = Convert.ToDateTime(nuevo);

                //var t= root.AsQueryable().Where(x => x.fechacreacion > dt && x.estado != "PENDIENTE")
                //    .GroupBy(a => a.request.pedido.remito).Select(a => a.First());


                //.Select(x => x.request.pedido.remito).Distinct().Select(x => new SelectListItem { Text = x });



                var tweetsFiltrados = (from c in root.AsQueryable()
                                       where c.fechacreacion > dt && c.request.pedido.propietario.Contains("NARANJA") && c.estado != "PENDIENTE"
                                       //where c.fechacreacion > dt && c.estado != "PENDIENTE"
                                       //where c.fechacreacion > dt && c.request.pedido.propietario !="POMELO"  && c.estado != "PENDIENTE"
                                       //orderby c.fechacreacion
                                       select new
                                       {
                                           idtransaccion = c.response.idtransaccion,
                                           estado = c.estado,
                                           propietario = c.request.pedido.propietario,
                                           razon = c.razon,
                                            fecha = c.fechacreacion

                                       })
                                        .Distinct()
        .ToList();
                //int a = tweetsFiltrados.Count();
                Console.WriteLine("Cantidad de registros filtrados: " + tweetsFiltrados.Count());
                foreach (var item in tweetsFiltrados)
                {
                    Console.WriteLine("{0}", item.idtransaccion);
                    listRange.Add(new CollectionMongo() { idtransaccion = item.idtransaccion, propietario = Convert.ToString(item.propietario), estado = Convert.ToString(item.estado), razon = Convert.ToString(item.razon), fechacreacion = Convert.ToDateTime(item.fecha) });
                }

                //var builder = Builders<CollectionMongo>.Filter;
             
                //var pro = IMongoCollection<Request>(collection);
                //where pro.fechacreacion > dt && pro.fechacreacion < dts
                //var filter = builder.Eq("request.pedido.propietario", "NARANJA");
                //filter &= builder.Gt("fechacreacion", "2023-01-01T00:00:00.000+00:00");
                //var filter = builder.Gt("fechacreacion", "2023-03-06T00:00:00.000+00:00");
                //filter &=builder.Eq("request.pedido.propietario", "NARANJA"); 
           


                //find the ID of the Godfather movie...

               // dynamic theGodfather = await collection.FindAsync(filter);

               // var options = new FindOptions<BsonDocument>
               // {
               //     // Get 100 docs at a time
               //     BatchSize = 200
               // };
               // int count = 0;
               // using (var cursor = await collection.FindAsync<BsonDocument>(filter))

               // {

               //     while (await cursor.MoveNextAsync())

               //     {

               //         var batch = cursor.Current;
                    
               //         foreach (var document in batch.AsEnumerable())

               //         {
               //             var idtransaccion = (document[2][0]);
               //             var propietario = (document[1][4][0]);
               //             var estado = (document[3]);
               //             var razon = (document[4]);
               //             var fecha = (document[5]);
               //             listRange.Add(new CollectionMongo() { idtransaccion =Convert.ToInt32(idtransaccion), propietario = Convert.ToString(propietario), estado =Convert.ToString(estado), razon = Convert.ToString(razon), fechacreacion = Convert.ToDateTime(fecha) });
               //             Console.WriteLine("{0},{1},{2},{3},{4}", idtransaccion,propietario, estado,fecha,razon);

               //             count++;

               //         }

               //     }

               // }
          
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("exepcion en mongp");
                throw ex;
            }            

            return listRange;
        }

        private static object IMongoCollection<T>(IMongoCollection<CollectionMongo> collection)
        {
            throw new NotImplementedException();
        }

        //internal static List<string> GetMongoCollection()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
