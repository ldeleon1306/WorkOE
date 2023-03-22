using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkerOE.Models
{
    public class WAP_INGRESOPEDIDOS
    {
        public string IdTransacción  { get; set; }
        [Key]
        public string OrdenExterna1 { get; set; }
        public string Propietario { get; set; }
        public string RazonFalla { get; set; }
        public string Almacen { get; set; }
        public int Estado { get; set; }
    }
}
