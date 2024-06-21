using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
       
        public int IdEstado { get; set; }
        
        public string Descripcion { get; set; }

        public List<object> LisEstado { get; set; }
    }
}
