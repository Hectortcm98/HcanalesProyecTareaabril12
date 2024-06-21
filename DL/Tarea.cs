using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Tarea
    {
        [Key]
        public int IdTarea { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public DateTime FechaVencimiento { get; set; }

        [ForeignKey("Estado")]
        public int IdEstado { get; set; }

        public DL.Estado Estado { get; set; }
    }
}
