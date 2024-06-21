using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL
{
    public class Estado
    {
        [Key]
       public int IdEstado { get; set; }
        [Required]
       public string Descripcion { get; set; }
        
    }
}
