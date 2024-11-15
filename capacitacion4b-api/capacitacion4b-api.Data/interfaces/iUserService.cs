using capacitacion4b_api.DTOs.user;
using capacitacion4b_api.Models;

namespace capacitacion4b_api.Data.interfaces
{

    public interface iUserService
    {

        /* buscar todos los usuarios */
        public Task<IEnumerable<userModel>> FindAll();
        /* buscar un solo usuario*/
        public Task<userModel> FindOne(int idUsuario);
        /* crear un usuario */
        public Task<userModel> Create(createUserDto createUserDto);
        /* actualizar un usuario */
        public Task<userModel> Update(int idUsuario, updateUserDto updateUserDto);
        /* eliminar un usuario */
        public Task<userModel> Remove(int idUsuario);

    }

}
