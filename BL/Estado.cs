using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static (bool, string, ML.Estado, Exception) GetAllEstados(DL.AppDbContext dbContext)
        {
            ML.Estado estado1 = new ML.Estado();
            try
            {
                var registros = (from obj in dbContext.Estados select obj).ToList();

                if (registros.Count > 0)
                {
                    estado1.LisEstado = new List<object>();

                    foreach (var registro in registros)
                    {
                        ML.Estado estado = new ML.Estado();
                        estado.IdEstado = registro.IdEstado;
                        estado.Descripcion = registro.Descripcion;

                        estado1.LisEstado.Add(estado);
                    }
                    return (true, null, estado1, null);
                }
            }
            catch (Exception ex)
            {
                return (false, "Error al obtener la lista de estados: " + ex.Message, null, ex);
            }
            return (false, null, null, null);
        }

    }
}
