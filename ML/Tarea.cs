using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Tarea
    {
       
        public int IdTarea { get; set; }
       
        public string Titulo { get; set; }
        
        public string Descripcion { get; set; }
        
        public DateTime FechaVencimiento { get; set; }

        //Propiedad de navegacion a Estado
        
        public int IdEstado { get; set; }

            
        public List<object> LisTareas { get; set; }
    }
}
