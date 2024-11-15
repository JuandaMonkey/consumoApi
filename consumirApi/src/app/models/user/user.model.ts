/* modelo de tarea */
import { TaskModel } from "../task/task.model"; 

/* modelo de usuario */
export interface UserModel {
    idUsuario: number;
    nombresUsuario: string;
    usuarioUsuario: string;
    contrasenaUsuario: string;
    /* info de taskModel */
    tasks: TaskModel[];
}