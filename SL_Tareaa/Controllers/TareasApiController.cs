using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_Tareaa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasApiController : ControllerBase
    {

        private  readonly DL.AppDbContext _Context;
        public TareasApiController(DL.AppDbContext dbContext)
        {
            _Context = dbContext;
        }
        [HttpGet]
        [Route("GetAll")]
        //public IActionResult GetAll()
        //{
            //try
            //{
            //    var result = BL.Tareas.GetAll(_Context);
            //    if (result.Item1)
            //    {
            //        var tarea = result.Item3;
            //        return Ok(tarea);
            //    }
            //    else
            //    {
            //        var tarea = new ML.Tarea();
            //        return BadRequest(tarea);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Manejar la excepción aquí
            //    return RedirectToAction("Error", "Home");
            //}

        //}

        public IActionResult Index()
        {
            return Ok();
        }

    }
}
