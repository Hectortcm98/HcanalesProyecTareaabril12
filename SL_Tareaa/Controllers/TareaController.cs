using BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SL_Tareaa.Controllers
{
    public class TareaController : Controller
    {
        private readonly DL.AppDbContext _Context;
        public TareaController(DL.AppDbContext dbContext)
        {
            _Context = dbContext;
        }
        public IActionResult GetAll(int? idEstado)
        {
            try
            {
                var result = BL.Tareas.GetAll(_Context, idEstado);
                if (result.Item1)
                {
                    var tarea = result.Item3;
                    return View(tarea);
                }
                else
                {
                    var tarea = new ML.Tarea();
                    return View(tarea);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí
                return RedirectToAction("Error", "Home");
            }
        }

        // Acción para manejar la creación o edición de una tarea
        [HttpPost]
        public ActionResult GetAll(int? idTarea, ML.Tarea tarea)
        {


            if (idTarea != null)
            {
                var updateResult = Tareas.Update(tarea, _Context);
                if (updateResult.Item1)
                {
                    ViewBag.Text = "Se ha actualizado correctamente.";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Text = "No se ha actualizado correctamente.";
                    return PartialView("Modal");
                }
            }
            else
            {
                var addResult = Tareas.Add(tarea, _Context); // Asegúrate de pasar el contexto de la base de datos
                if (addResult.Item1)
                {
                    ViewBag.Text = "se ha agregado correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Text = " no se ha agregado correctamente";
                    return PartialView("Modal");
                }
            }
        }


        // Acción para manejar la actualización de una tarea
        [HttpPost]
        public ActionResult UpdateTarea(ML.Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                var updateResult = Tareas.Update(tarea, _Context);
                if (updateResult.Item1)
                {
                    ViewBag.Text = "Se ha actualizado correctamente.";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Text = "No se ha podido actualizar la tarea.";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Text = "Los datos de la tarea no son válidos.";
                return PartialView("Modal");
            }
        }

        public IActionResult Delete(int? idTarea)
        {
            try
            {
                var result = BL.Tareas.Delete(idTarea.Value, _Context);
                if (result.Item1)
                {
                    ViewBag.Text = "se ha eliminado correctamente";
                    return PartialView("Modal"); // Redirigir a la acción deseada después de eliminar
                }
                else
                {
                    ViewBag.Text = "NO se se ha eliminado correctamente";
                    return PartialView("Modal");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí
                ViewBag.ErrorMessage = "Ocurrió un error al eliminar la tarea"; // Opcional: mensaje de error genérico
                return PartialView("Modal"); // O redirigir a una vista de error específica
            }
        }

        [HttpPost]
        public IActionResult ActualizarEstado(int idTarea, int idEstado)
        {
            var resultado = BL.Tareas.ActualizarEstadoTarea(idTarea, idEstado, _Context);

            if (resultado.Item1) // Si la operación fue exitosa
            {
                return Json(new { success = true }); // Devolver un objeto JSON para indicar que la actualización fue exitosa
            }
            else // Si hubo algún error
            {
                return Json(new { success = false, errorMessage = resultado.Item2 }); // Devolver un objeto JSON con el mensaje de error
            }
        }



    }
}
