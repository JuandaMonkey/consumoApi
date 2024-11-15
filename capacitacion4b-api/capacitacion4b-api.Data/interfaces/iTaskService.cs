using capacitacion4b_api.DTOs.task;
using capacitacion4b_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capacitacion4b_api.Data.interfaces
{

    public interface iTaskService
    {

        /* todas las tareas */
        public Task<IEnumerable<taskModel>> FindAll();
        /* buscar una tarea */
        public Task<taskModel> FindOne(int idTarea);
        /* crear tarea */
        public Task<taskModel?> Create(createTaskDto createTaskDto);
        /* actualizar tarea */
        public Task<taskModel?> Update(int idTarea, updateTaskDto updateTaskDto);
        /* eliminar tarea */
        public Task<taskModel> Remove(int idTarea);
        /* cambiar el status tarea */
        public Task<taskModel> ToggleStatus(int idTarea);
        
    }

}
