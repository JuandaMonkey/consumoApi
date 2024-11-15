/* modelo de usuario */
import { UserModel } from "../user/user.model";

/* modelo de tarea */
export interface TaskModel {
    idTarea: number;
    tarea: string;
    descripcion: string;
    completada: boolean;
    /* info de userModel */
    usuario: UserModel;
}