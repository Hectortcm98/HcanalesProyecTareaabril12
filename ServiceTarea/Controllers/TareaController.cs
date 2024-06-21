using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServiceTarea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase

    {
        private readonly DL.AppDbContext _Context;
        public TareaController(DL.AppDbContext dbContext)
        {
            _Context = dbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(int? idEstado)
        {

            try
            {
                var result = BL.Tareas.GetAll(_Context, idEstado);
                if (result.Item1)
                {
                    var tarea = result.Item3;
                    return Ok(tarea);
                }
                else
                {
                    var tarea = new ML.Tarea();
                    return BadRequest(tarea);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí
                return RedirectToAction("Error", "Home");
            }

        }


        [HttpPost]
        [Route("Update/Add")]
        public IActionResult GetAll(int? idTarea, ML.Tarea tarea)
        {


            if (idTarea != null)
            {
                var updateResult = Tareas.Update(tarea, _Context);
                if (updateResult.Item1)
                {
                    return Ok(tarea);
                }
                else
                {
                    
                    return BadRequest(tarea);
                }
            }
            else
            {
                var addResult = Tareas.Add(tarea, _Context); // Asegúrate de pasar el contexto de la base de datos
                if (addResult.Item1)
                {
                    return Ok(tarea);
                }
                else
                {
                    return BadRequest(tarea);
                }
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int? idTarea, ML.Result resultt)
        {
            try
            {
                var result = BL.Tareas.Delete(idTarea.Value, _Context);
                if (result.Item1)
                {
                    return Ok(resultt); // Redirigir a la acción deseada después de eliminar
                }
                else
                {
                    return BadRequest(resultt);

                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí
                return RedirectToAction("Error", "Home");


            }

        } 


    }
}
