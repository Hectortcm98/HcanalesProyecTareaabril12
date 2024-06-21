using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebServiceTarea.Controllers
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
        public IActionResult GetAll()
        {
            var lista = _Context.Tareas.ToList();
            var result = BL.Tareas.GetAll();
            if (result.Item1 == true)
            {
                ML.Tarea tarea = result.Item3;
                return Ok(tarea);
            }
            else
            {
                ML.Tarea tarea = new ML.Tarea();
                return BadRequest(tarea);
            }
        }
    }
}
