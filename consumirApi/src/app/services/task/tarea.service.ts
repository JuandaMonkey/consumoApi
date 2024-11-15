import { Injectable } from '@angular/core';
/* --- */
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
/* modelo de tarea */
import { TaskModel } from '../../models/task/task.model';
/* modelo de crear tarea */
import { CreateTaskModel } from '../../models/task/createTask.model';
/* modelo de modificar tarea */
import { UpdateTaskModel } from '../../models/task/updateTask.model';

@Injectable({
  providedIn: 'root'
})

/* servicios de tarea */
export class TaskService {

  /* apiURL */
  private apiUrl='https://localhost:44331/api/task';

  constructor(private http: HttpClient) { }

  /* obtener tareas */
  getTasks(): Observable<TaskModel[]> {
    return this.http.get<TaskModel[]>(this.apiUrl);
  }
  
  /* insertar tarea */
  createTask(task: CreateTaskModel): Observable<TaskModel> {
    return this.http.post<TaskModel>(this.apiUrl, task);
  }
  
  /* modificar tarea */
  updateTask(id: number, task: UpdateTaskModel): Observable<TaskModel> {
    return this.http.put<TaskModel>(`${this.apiUrl}/${id}`, task);
  }

  /* eliminar tarea */
  deleteTask(id: number): Observable<TaskModel> {
    return this.http.delete<TaskModel>(`${this.apiUrl}/${id}`);
  }

  /* cambiar estado de tarea */
  toggleStatus(id: number): Observable<TaskModel> {
    return this.http.get<TaskModel>(`${this.apiUrl}/${id}`);
  }
}
