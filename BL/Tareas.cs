using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class Tareas
    {
        //INYECCION DE DEPENDECIAS
        private readonly DL.AppDbContext _DbContext;

        public Tareas(DL.AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }




        public static (bool, string, ML.Tarea, Exception) GetAll(DL.AppDbContext dbContext, int? idEstado)
        {
            ML.Tarea tarea = new ML.Tarea();
            try
            {
                IQueryable<DL.Tarea> consulta = dbContext.Tareas; // Inicializa la consulta con todas las tareas

                if (idEstado.HasValue)
                {
                    // Aplica el filtro de estado si es que se proporciona
                    consulta = consulta.Where(t => t.IdEstado == idEstado.Value);
                }

                var registros = consulta.ToList(); // Ejecuta la consulta

                if (registros.Count > 0)
                {
                    tarea.LisTareas = new List<object>();

                    foreach (var registro in registros)
                    {
                        ML.Tarea tareaObj = new ML.Tarea();
                        tareaObj.IdTarea = registro.IdTarea;
                        tareaObj.Titulo = registro.Titulo;
                        tareaObj.Descripcion = registro.Descripcion;
                        tareaObj.FechaVencimiento = registro.FechaVencimiento;
                        tareaObj.IdEstado = registro.IdEstado;

                        tarea.LisTareas.Add(tareaObj);
                    }
                    return (true, null, tarea, null);
                }
                else
                {
                    return (false, "No se encontraron registros.", null, null);
                }
            }
            catch (Exception ex)
            {
                return (false, "Error al obtener la lista de tareas: " + ex.Message, null, ex);
            }
        }


        public static (bool, string, ML.Tarea, Exception) Add(ML.Tarea tarea, DL.AppDbContext context)
        {
            try
            {
                var query = context.Database.ExecuteSqlRaw("EXEC TareasAdd @Titulo, @Descripcion, @FechaVencimiento, @IdEstado",
                    new SqlParameter("@Titulo", tarea.Titulo),
                    new SqlParameter("@Descripcion", tarea.Descripcion),
                    new SqlParameter("@FechaVencimiento", tarea.FechaVencimiento),
                    new SqlParameter("@IdEstado", tarea.IdEstado)
                );

                if (query > 0)
                {
                    return (true, null, tarea, null);
                }
                else
                {
                    return (false, "No se agregó correctamente", tarea, null);
                }
            }
            catch (Exception ex)
            {
                return (false, "No se agregó correctamente", null, ex);
            }
        }


        public static (bool, string, ML.Tarea, Exception) GetById(int idTarea, DL.AppDbContext dbContext)
        {
            try
            {
                var tarea = new ML.Tarea();

                // Llama al procedimiento almacenado TareaGetById para obtener la tarea por su IdTarea
                var query = dbContext.Tareas.FromSqlRaw("EXEC [TareaGetById] @IdTarea",
                    new SqlParameter("@IdTarea", idTarea)).FirstOrDefault();

                if (query != null)
                {
                    tarea.IdTarea = query.IdTarea;
                    tarea.Titulo = query.Titulo;
                    tarea.Descripcion = query.Descripcion;
                    tarea.FechaVencimiento = query.FechaVencimiento;
                    tarea.IdEstado = query.IdEstado;

                    return (true, null, tarea, null);
                }
                else
                {
                    return (false, "No se encontró la tarea con el Id especificado", null, null);
                }
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error al obtener la tarea: " + ex.Message, null, ex);
            }
        }

        public static (bool, string, Exception) Update(ML.Tarea tarea, DL.AppDbContext dbContext)
        {
            try
            {
                // Verifica si la fecha de vencimiento es nula y asigna DBNull.Value si es el caso
                SqlParameter fechaVencimientoParam = new SqlParameter("@FechaVencimiento", tarea.FechaVencimiento != null ? (object)tarea.FechaVencimiento : DBNull.Value);

                // Llama al procedimiento almacenado TareaUpdate para actualizar la tarea en la base de datos
                var query = dbContext.Database.ExecuteSqlRaw("EXEC [TareaUpdate] @IdTarea, @Titulo, @Descripcion, @FechaVencimiento, @IdEstado",
                    new SqlParameter("@IdTarea", tarea.IdTarea),
                    new SqlParameter("@Titulo", tarea.Titulo),
                    new SqlParameter("@Descripcion", tarea.Descripcion),
                    fechaVencimientoParam,
                    new SqlParameter("@IdEstado", tarea.IdEstado));

                if (query > 0)
                {
                    return (true, "Tarea actualizada correctamente", null);
                }
                else
                {
                    return (false, "No se actualizó correctamente la tarea. No se encontró ninguna fila afectada.", null);
                }
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error al actualizar la tarea: " + ex.Message, ex);
            }
        }


        public static (bool, string, Exception) Delete(int IdTarea, DL.AppDbContext dbContext)
        {
            try
            {
                var query = dbContext.Database.ExecuteSqlRaw("EXEC [TareaDelete] @IdTarea, @Titulo, @Descripcion, @FechaVencimiento, @IdEstado",
            new SqlParameter("@IdTarea", IdTarea),
            new SqlParameter("@Titulo", DBNull.Value), 
            new SqlParameter("@Descripcion", DBNull.Value), 
            new SqlParameter("@FechaVencimiento", DBNull.Value),
            new SqlParameter("@IdEstado", DBNull.Value)); 

                if (query > 0)
                {
                    return (true, null, null);
                }
                else
                {
                    return (false, "No se eliminó correctamente", null);
                }
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error al eliminar la tarea", ex);
            }
        }


        public static (bool, string, Exception) ActualizarEstadoTarea(int idTarea, int idEstado, DL.AppDbContext dbContext)
        {
            try
            {
                // Buscar la tarea en la base de datos
                var tarea = dbContext.Tareas.FirstOrDefault(t => t.IdTarea == idTarea);

                if (tarea == null)
                {
                    return (false, "La tarea no se encontró", null);
                }

                // Actualizar el estado de la tarea
                tarea.IdEstado = idEstado;

                // Guardar los cambios en la base de datos
                dbContext.SaveChanges();

                return (true, "Se actualizó correctamente el estado de la tarea", null);
            }
            catch (Exception ex)
            {
                return (false, "Ocurrió un error al actualizar el estado de la tarea", ex);
            }
        }
    }

}
