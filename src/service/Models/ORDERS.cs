using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorkerOE.Models
{
    class Orders
    {
        public string IdTransacción { get; set; }
        [Key]
        public string EXTERNORDERKEY { get; set; }
        public string WHSEID { get; set; }
        public int STATUS { get; set; }
    }
}
