using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerOE.Models
{
    public class CollectionMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? _id { get; set; }   // this maps to `_id` in the document       
        public int idtransaccion { get; set; }
        public string estado { get; set; }
        public string propietario { get; set; }
        public string Razon { get; set; }
        public string remito { get; set; }
        //public DateTime fechacreacion { get; set; }
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        //public DateTime CreatedOn { get; set; }
        //public string Description { get; set; }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        [BsonDateTimeOptions]
        public DateTime fechacreacion { get; set; }
        public object request { get; set; }
        public object response { get; set; }
        public string razon { get; set; }
        public DateTime fechamodificacion { get; set; }
        //public object[] request { get; set; }
        //public object[] response { get; set; }
   

        public class Articulo
                
        {
            public string codigo { get; set; }
            public string propietario { get; set; }
            public Lote lote { get; set; }
        }

        public class Destinatario
        {
            public string numerodedocumento { get; set; }
            public string iddestinatario { get; set; }
            public string idinternocliente { get; set; }
            public string nombrecompleto { get; set; }
            public string email { get; set; }
            public List<Telefono> telefonos { get; set; }
            public string tipodedocumento { get; set; }
            public string contacto { get; set; }
        }

        public class Detalle
        {
            public Articulo articulo { get; set; }
            public string numeropedido { get; set; }
            public string unidadmedida { get; set; }
            public string lineaexterna { get; set; }
            public int unidades { get; set; }
            public object datosadicionales { get; set; }
        }

        public class Detalleordendecompra
        {
            public string gs1numerodelineadecliente { get; set; }
            public string gs1ordendecompra { get; set; }
            public string gs1fechaordendecompra { get; set; }
            public string gs1numerolinea { get; set; }
            public string gs1tipomaterial { get; set; }
            public string gs1cantidadpedida { get; set; }
        }

        public class Direccion
        {
            public string calle { get; set; }
            public string numero { get; set; }
            public string codigopostal { get; set; }
            public string localidad { get; set; }
            public string provincia { get; set; }
            public string pais { get; set; }
            public string piso { get; set; }
            public string departamento { get; set; }
            public string referenciadedomicilio { get; set; }
            public object componentesdedireccion { get; set; }
        }

        public class Fechacreacion
        {      
            public DateTime date { get; set; }
        }

        public class Fechamodificacion
        {
            [JsonProperty("$date")]
            public DateTime date { get; set; }
        }

        public class Id
        {
            [JsonProperty("$oid")]
            public string oid { get; set; }
        }

        public class Idtransaccion
        {
            [JsonProperty("$numberLong")]
            public string numberLong { get; set; }
        }

        public class Lote
        {
            public string codigo { get; set; }
            public string lotedefabricante { get; set; }
            public string fechadevencimiento { get; set; }
            public Otrosdatos otrosdatos { get; set; }
        }

        public class Otrosdatos
        {
            public object metadatos { get; set; }
        }

        public class Pedido
        {
            public string propietario { get; set; }
            public string clientepadre { get; set; }
            public string idpedido { get; set; }
            public string numero { get; set; }
            public string tipo { get; set; }
            public string prioridaddepreparacion { get; set; }
            public string fechapedido { get; set; }
            public string fechaentrega { get; set; }
            public string remito { get; set; }
            public string idcliente { get; set; }
            public string referenciacliente { get; set; }
            public string codigotransportista { get; set; }
            public string acondicionamientosecundario { get; set; }
            public string motivo { get; set; }
            public string ramodeldestinatario { get; set; }
            public string idejecucion { get; set; }
            public string embalaje { get; set; }
            public object notas { get; set; }
            public string admitecambiolotedirigido { get; set; }
            public string admitepickingparcial { get; set; }
            public bool imprimedocumentacion { get; set; }
            public string nrovale { get; set; }
            public string valordeclarado { get; set; }
            public object camposlibres { get; set; }
            public string sociocomercial { get; set; }
            public string mododetransporte { get; set; }
            public Destinatario destinatario { get; set; }
            public Direccion direccion { get; set; }
            public List<Detalle> detalles { get; set; }
            public object datosadicionales { get; set; }
            public Detalleordendecompra detalleordendecompra { get; set; }
        }

        public class Request
        {
            public string planta { get; set; }
            public string almacen { get; set; }
            public string almacensap { get; set; }
            public string contrato { get; set; }
            public Pedido pedido { get; set; }
        }

        public class Response
        {
            public int idtransaccion { get; set; }
        }

        public class Root
        {
            public Id _id { get; set; }
            public Request request { get; set; }
            public Response response { get; set; }
            public string estado { get; set; }
            public string razon { get; set; }
            public DateTime fechacreacion { get; set; }
            public Fechamodificacion fechamodificacion { get; set; }
        }

        public class Telefono
        {
            public int tipo { get; set; }
            public string numero { get; set; }
        }



    }
}
