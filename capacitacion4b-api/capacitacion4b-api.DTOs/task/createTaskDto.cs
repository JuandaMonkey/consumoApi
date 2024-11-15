using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capacitacion4b_api.DTOs.task
{

    public class createTaskDto
    {

        public string? tarea { get; set; }
        public string? descripcion { get; set; }   
        public int idUsuario { get; set; }

    }

}
