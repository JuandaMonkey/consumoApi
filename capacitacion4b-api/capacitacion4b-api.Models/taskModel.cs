namespace capacitacion4b_api.Models
{

    public class taskModel
    {

        public int idTarea { get; set; }
        public string? tarea { get; set; }
        public string? descripcion { get; set; }
        public bool completada { get; set; }
        public userModel? usuario { get; set; }

    }

}
