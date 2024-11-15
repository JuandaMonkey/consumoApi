using capacitacion4b_api.Data.interfaces;
using capacitacion4b_api.DTOs.user;
using capacitacion4b_api.Models;
using Dapper;
using Npgsql;

namespace capacitacion4b_api.Data.services
{

    public class userService : iUserService
    {

        private postgresqlConection _postgresqlConection;
        public userService(postgresqlConection postgresqlConection) => _postgresqlConection = postgresqlConection;

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_postgresqlConection._connection);

        #region userFindAll

        public async Task<IEnumerable<userModel>> FindAll()
        {

            /* cadena query */
            string sqlQuery = "select * from v_usuarios";

            Dictionary<int, List<taskModel>> tasks = [];

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                IEnumerable<userModel> allUsers = await database.QueryAsync<userModel, taskModel, userModel>(
                    sqlQuery,
                    param: new { },
                    map: (user, task) =>
                    {

                        List<taskModel>? currentTask = [];
                        tasks.TryGetValue(user.idUsuario, out currentTask);

                        currentTask ??= [];
                    
                        if (currentTask.Count == 0 && task != null)
                        {
                        
                            currentTask = [task];
                        
                        } 
                        else if (currentTask.Count > 0 && task != null)
                        {

                            currentTask.Add(task);

                        }

                        tasks[user.idUsuario] = currentTask;

                        return user;

                    },
                    splitOn: "idTarea"
                    );

                /* cierra conexión*/
                await database.CloseAsync();

                allUsers = allUsers.Distinct().Select(user =>
                {

                    user.tasks = tasks[user.idUsuario];
                    return user;

                });

                /* regresa los usuarios */
                return allUsers;

            }
            catch (Exception e)
            {
                return [];
            }

        }

        #endregion

        #region userFindOne

        public async Task<userModel> FindOne(int idUsuario)
        {

            /* cadena query */
            string sqlQuery = "select * from v_usuarios where idUsuario = @idUsuario;";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                userModel? user = await database.QueryFirstOrDefaultAsync<userModel>(sqlQuery, new { idUsuario });

                /* cierra conexión*/
                await database.CloseAsync();

                /* regresa al usuario */
                return user;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region userCreate

        public async Task<userModel> Create(createUserDto createUserDto)
        {

            /* cadena query */
            string sqlQuery = "select * from f_createUser (" +
                "p_nombresUsuario := @nombres," +
                "p_usuarioUsuario := @usuario," +
                "p_contrasenaUsuario := @contrasena);";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                userModel? user = await database.QueryFirstOrDefaultAsync<userModel>(sqlQuery, new
                {

                    nombres = createUserDto.nombres,
                    usuario = createUserDto.usuario,
                    contrasena = createUserDto.contrasena

                });

                /* cierra conexión */
                await database.CloseAsync();

                /* regresa al usuario ingresado */
                return user;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion

        #region userUpdate

        public async Task<userModel> Update(int idUsuario, updateUserDto updateUserDto)
        {

            /* cadena query */
            string sqlQuery = $"select * from f_updateUser (" +
                "p_idUsuario := @idUsuario," +
                "p_nombresUsuario := @nombres," +
                "p_usuarioUsuario := @usuario," +
                "p_contrasenaUsuario := @contrasena);";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                userModel? user = await database.QueryFirstOrDefaultAsync<userModel>(sqlQuery, new
                {

                    idUsuario = idUsuario,
                    nombres = updateUserDto.nombres,
                    usuario = updateUserDto.usuario,
                    contrasena = updateUserDto.contrasena

                });

                /* cierra conexión */
                await database.CloseAsync();

                /* regresa al usuario actualizado */
                return user;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion

        #region userRemove

        public async Task<userModel> Remove(int idUsuario)
        {

            /* ejecuta el query */
            string sqlQuery = "select * from f_removeUser (p_idUsuario := @idUsuario)";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                userModel? user = await database.QueryFirstOrDefaultAsync<userModel>(sqlQuery, param: new 
                { 

                    idUsuario 

                });

                /* cierra conexión*/
                await database.CloseAsync();

                /* regresa al usuario eliminado */
                return user;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

    }
}
