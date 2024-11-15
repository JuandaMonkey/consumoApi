using capacitacion4b_api.Data.interfaces;
using capacitacion4b_api.Data.services;
using capacitacion4b_api.DTOs.task;
using capacitacion4b_api.DTOs.user;
using capacitacion4b_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace capacitacion4b_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class taskController : Controller
    {

        private iTaskService _taskService;
        public taskController(iTaskService taskService) => _taskService = taskService;

        [HttpGet]
        /* obtiene todas las tareas */
        public async Task<IActionResult> FindAll()
        {

            var task = await _taskService.FindAll();
            return Ok(task);

        }


        [HttpGet("idTarea")]
        /* obtiene la tarea seleccionada */
        public async Task<IActionResult> FindOne(int idTarea)
        {

            var task = await _taskService.FindOne(idTarea);
            return Ok(task);

        }

        [HttpPost]
        /* crea una nueva tarea */
        public async Task<IActionResult> Create([FromBody] createTaskDto createTaskDto)
        {

            taskModel? task = await _taskService.Create(createTaskDto);
            return Ok(task);

        }

        [HttpPut("{idTarea}")]
        /* actualiza la tarea indicada */
        public async Task<IActionResult> Update(int idTarea, [FromBody] updateTaskDto updateTaskDto)
        {

            taskModel? task = await _taskService.Update(idTarea, updateTaskDto);
            return Ok(task);

        }

        [HttpDelete("{idTarea}")]
        /* elimina la tarea indicada */
        public async Task<IActionResult> Remove(int idTarea)
        {

            taskModel? task = await _taskService.Remove(idTarea);
            return Ok(task);

        }

        [HttpGet("{idTarea}")]
        /* cambia el estado de la tarea indicada */
        public async Task<IActionResult> ToggleStatus(int idTarea)
        {

            taskModel? task = await _taskService.ToggleStatus(idTarea);
            return Ok(task);

        }

    }
}
