import { Component, OnInit, Optional } from '@angular/core';
/* --- */
import { ReactiveFormsModule, FormBuilder, Validators, FormsModule, FormGroup } from '@angular/forms';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';
import { Table, TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { Button, ButtonModule } from 'primeng/button';
import { trigger, state, style, animate, transition } from '@angular/animations';
/* servicio de usuario */
import { UserService } from '../../services/user/user.service';
/* modelo de usuario */
import { UserModel } from '../../models/user/user.model';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, TableModule, DialogModule, ButtonModule],
  templateUrl: './user-list.component.html',
  providers: [DialogService],
  styleUrl: './user-list.component.css',
  animations: [
    trigger('fadeInOut', [
      state('void', style({opacity: 0})),
      transition(':enter', [animate('500ms ease-in')]),
      transition(':leave', [animate('500ms ease-out', style({opacity: 0}))])
    ])
  ]
})

export class UserListComponent implements OnInit {
  /* usuario */
  user: UserModel[] = [];
  /* displayDialog */
  displayDialog: boolean = false;
  /* formulario */
  userForm!: FormGroup;
  /* se almacena el id del usuario 
   * o nulo para poder vacíar la variable */
  idUsuario: number | null = null;

  constructor(
    private userService: UserService,
    private fb: FormBuilder,
    private dialogService: DialogService
  ) {} 

  ngOnInit()
  {
    /* formulario de usuario */
    this.userForm = this.fb.group({
      idUsuario: [null],
      nombres: ['', Validators.required],
      usuario: ['', Validators.required],
      contrasena: ['', Validators.required]
    });
    /* cargar todos los usuarios */
    this.loadUsuarios();
  }

  /* realiza la operación */
  saveUser(){
    /* si el formulario de usuarios es valido */
    if (this.userForm.valid) {
      /* userData, contendra los valores */
      const userData = this.userForm.value;
      /* si, idUsuario contiene una id */
      if (this.idUsuario != null)
        /* actualiza */
        this.updateUser(this.idUsuario, userData);
      else /* si no */
        /* crealo */
        this.createUser(userData);
    }
  }

  /* alamacenara los datos del usuario */
  saveUpdateUser(user: UserModel){
    /* se almacenara el idUsuario en la variable */
    this.idUsuario = user.idUsuario;
    /* los datos, se mostraran el el formulario */
    this.userForm.patchValue({
      /* parametros */
      nombres: user.nombresUsuario,
      usuario: user.usuarioUsuario,
      contrasena: user.contrasenaUsuario
    });
    /* activa el displayDialog */
    this.displayDialog = true;
  }

  /* cargar usuarios */
  loadUsuarios(){
    /* obtiene todos los usuarios */
    this.userService.getUsers().subscribe((data) => {
      this.user = data;
      console.log(this.user);
    })
  }

  /* crear usuario */
  createUser(userData: any){
    /* crea el usuarios */
    this.userService.createUser({
      /* parametros */
      nombres: userData.nombres,
      usuario: userData.usuario,
      contrasena: userData.contrasena
    }).subscribe(() => {
      this.loadUsuarios();
      /* cierra el displayDialog */
      this.displayDialog = false;
    })
  }

  /* actualiza */
  updateUser(id: number, userData: any){
    /* recibe los parametros de saveUpdateUser() */
    this.userService.updateUser(id,{
      nombres: userData.nombres,
      usuario: userData.usuario,
      contrasena: userData.contrasena
    }).subscribe(() => {
      this.loadUsuarios();
      /* cierra el displayDialog */
      this.displayDialog = false;
      /* vacía la variable */
      this.idUsuario = null;
    })
  }

  /* eliminar usuario */
  deleteUser(id: number){
    /* recibe el idUsuario y lo elimina */
    this.userService.deleteUser(id).subscribe(() => {
      this.loadUsuarios();
      })
  }
  
  /* abrir dialog - crear */
  openDialog(){
    /* abre */
    this.displayDialog = true;
    /* resetea parametros */
    this.userForm.reset();
    /* vacía el idUsuario */
    this.idUsuario = null;
  }
}
