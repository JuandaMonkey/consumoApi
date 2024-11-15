using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capacitacion4b_api.Models
{

    public class userModel
    {

        /* idUsuario */
        public int idUsuario { get; set; }
        /* nombresUsuario */
        public string? nombresUsuario { get; set; }
        /* usuarioUsuario */
        public string? usuarioUsuario { get; set; }
        /* contrasenaUsuario*/
        public string? contrasenaUsuario { get; set; }
        /* lista tareas */
        public List<taskModel> tasks { get; set; } = [];

        public override int GetHashCode() => idUsuario.GetHashCode();

        public override bool Equals(object? obj)
        {
        
            if (obj == null) return false;

            if (obj is userModel user)
            {

                return user.idUsuario == idUsuario;

            }

            return false;

        }
    }

}
