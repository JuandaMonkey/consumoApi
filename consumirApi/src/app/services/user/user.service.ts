import { Injectable } from '@angular/core';
/* --- */
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
/* modelo de usuario */
import { UserModel } from '../../models/user/user.model';
/* modelo de crear usuario */
import { CreateUserModel } from '../../models/user/createUser.model';
/* modelo de modificar usuario */
import { UpdateUserModel } from '../../models/user/updateUser.model';

@Injectable({
  providedIn: 'root'
})

/* servicios de usuario */
export class UserService {

  /* apiURL */
  private apiUrl='https://localhost:44331/api/user';

  constructor(private http: HttpClient) { }

  /* obtener usuarios */
  getUsers(): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(this.apiUrl);
  }

  /* insertar usuario */
  createUser(user: CreateUserModel): Observable<UserModel> {
    return this.http.post<UserModel>(this.apiUrl, user);
  }
  
  /* modificar usuario */
  updateUser(id: number, user: UpdateUserModel): Observable<UserModel> {
    return this.http.put<UserModel>(`${this.apiUrl}/${id}`, user);
  }

  /* eliminar usuario */
  deleteUser(id: number): Observable<UserModel> {
    return this.http.delete<UserModel>(`${this.apiUrl}/${id}`);
  }
}
