using capacitacion4b_api.Data.interfaces;
using capacitacion4b_api.DTOs.task;
using capacitacion4b_api.DTOs.user;
using capacitacion4b_api.Models;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capacitacion4b_api.Data.services
{
    
    public class taskService : iTaskService
    {

        private postgresqlConection _postgresqlConection;
        public taskService(postgresqlConection postgresqlConection) => _postgresqlConection = postgresqlConection;

        private NpgsqlConnection GetConnection() => new NpgsqlConnection(_postgresqlConection._connection);

        #region taskFindAll

        public async Task<IEnumerable<taskModel>> FindAll()
        {

            /* cadena query */
            string sqlQuery = "select * from v_tareas";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                IEnumerable<taskModel> allTasks = await database.QueryAsync<taskModel, userModel, taskModel>(
                    sqlQuery,
                    /* mapeo */
                    map:
                    (task, user) => 
                    {

                        /* el atributo usuario en task es user */
                        task.usuario = user;
                        /* regresa la tarea */
                        return task;

                    },
                    splitOn: "idUsuario"
                    );

                /* cierra conexión*/
                await database.CloseAsync();

                /* regresa las tareas */
                return allTasks;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion

        #region taskFindOne

        public async Task<taskModel> FindOne(int idTarea)
        {

            /* cadena query */
            string sqlQuery = "select * from v_tareas where idTarea = @idTarea";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                IEnumerable<taskModel> task = await database.QueryAsync<taskModel, userModel, taskModel>(
                    sqlQuery, param: new { idTarea },
                    /* mapeo */
                    map:
                    (task, user) =>
                    {

                        /* asigna el usuario a la tarea */
                        task.usuario = user;
                        /* regresa la tarea */
                        return task;

                    },
                    splitOn: "idUsuario"
                    );

                /* regresa las tareas */
                return task.FirstOrDefault();

            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion

        #region taskCreate
        public async Task<taskModel?> Create(createTaskDto createTaskDto)
        {

            /* cadena query */
            string sqlQuery = "select * from f_createTask (" +
                "p_tarea := @tarea," +
                "p_descripcion := @descripcion," +
                "p_fk_idUsuario := @idUsuario);";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                taskModel? task = await database.QueryFirstOrDefaultAsync<taskModel>(sqlQuery, param: new
                {

                    tarea = createTaskDto.tarea,
                    descripcion = createTaskDto.descripcion,
                    idUsuario = createTaskDto.idUsuario

                });

                /* cierra conexión*/
                await database.CloseAsync();

                /* regresa la tarea creada */
                return task;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region taskUpdate

        public async Task<taskModel?> Update(int idTarea, updateTaskDto updateTaskDto)
        {
            /* query de function */
            string sqlQuery = "select * from f_updateTask (" +
                "p_idTarea := @idTarea," +
                "p_tarea := @tarea," +
                "p_descripcion := @descripcion);";

            /* usamos la conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                taskModel? task = await database.QueryFirstOrDefaultAsync<taskModel>(sqlQuery,
                param: new
                {

                    /* parametros */
                    idTarea = idTarea,
                    tarea = updateTaskDto.tarea,
                    descripcion = updateTaskDto.descripcion

                }
                );

                /* cierra conexión*/
                await database.CloseAsync();

                /* regresa la tarea actualizada */
                return task;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region taskRemove

        public async Task<taskModel> Remove(int idTarea)
        {

            /* cadena query */
            string sqlQuery = "select * from f_removeTask ( p_idTarea := @idTarea );";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                taskModel? task = await database.QueryFirstOrDefaultAsync<taskModel>(sqlQuery, param: new 
                {

                    idTarea 

                });

                /* cierra conexión*/
                await database.CloseAsync();

                /* regresa la tarea eliminada */
                return task;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion

        #region taskToggleStatus

        public async Task<taskModel> ToggleStatus(int idTarea)
        {

            /* cadena query */
            string sqlQuery = "select * from f_task_toggleStatus ( p_idTarea := @idTarea );";

            /* conexión */
            using NpgsqlConnection database = GetConnection();

            try
            {

                /* abre conexión */
                await database.OpenAsync();

                /* ejecuta el query */
                taskModel? task = await database.QueryFirstOrDefaultAsync<taskModel>(sqlQuery, param: new
                {

                    idTarea

                });

                /* cierra conexión*/
                await database.CloseAsync();

                /* regresa la tarea con el estado cambiado */
                return task;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion

    }

}
