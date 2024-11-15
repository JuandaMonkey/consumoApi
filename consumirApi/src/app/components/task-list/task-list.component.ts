import { Component, OnInit } from '@angular/core';
/* --- */
import { ReactiveFormsModule, FormBuilder, Validators, FormsModule, FormGroup } from '@angular/forms';
import { DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';
import { Table, TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { Button, ButtonModule } from 'primeng/button';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { Dropdown, DropdownModule } from 'primeng/dropdown';
/* servicio de tarea */
import { TaskService } from '../../services/task/tarea.service';
/* modelo de tarea */
import { TaskModel } from '../../models/task/task.model';
/* servicio de usuario */
import { UserService } from '../../services/user/user.service';
/* modelo de usuario */
import { UserModel } from '../../models/user/user.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, TableModule, DialogModule, ButtonModule, DropdownModule],
  templateUrl: './task-list.component.html',
  providers: [DialogService],
  styleUrl: './task-list.component.css',
  animations: [
    trigger('fadeInOut', [
      state('void', style({opacity: 0})),
      transition(':enter', [animate('500ms ease-in')]),
      transition(':leave', [animate('500ms ease-out', style({opacity: 0}))])
    ])
  ]
})
export class TaskListComponent implements OnInit {
  /* tarea */
  task: TaskModel[] = [];
  /* displayDialog */
  displayDialog: boolean = false;
  /* formulario */
  taskForm!: FormGroup;
  /* usuario */
  usuario: UserModel[] = [];
  /* se almacenara el id de la tarea 
   * o nulo para poder vacíar la variable */
  idTarea: number | null = null;
  /* desactivar dropList */
  disable: boolean = false;

  constructor(
    private taskService: TaskService,
    private userService: UserService,
    private fb: FormBuilder,
    private dialogService: DialogService
  ) {}

  ngOnInit()
  {
    /* formilario de tarea */
    this.taskForm = this.fb.group({
      idTarea: [null],
      tarea: ['', Validators.required],
      descripcion: ['', Validators.required],
      completada: [false],
      idUsuario: [null, Validators.required]
    });
    /* carga todas las tareas */
    this.loadTasks();
    /* carga todos los usuarios */
    this.loadUsers();
  }

  /* realiza la operación */
  saveTask(){
    /* si el formulario de tarea es valido */
    if (this.taskForm.valid) {
      /* taskData, contendra los valores */
      const taskData = this.taskForm.value;
      /* si, idTarea contiene una id */
      if (this.idTarea != null)
        /* actualiza */
        this.updateTask(this.idTarea, taskData);
      else /* si no */
        /* crealo */
        this.createTask(taskData);
    }
  }

  /* alamacenara los datos de la tarea */
  saveUpdateTask(task: TaskModel){
    /* se almacenara el idTarea en la variable */
    this.idTarea = task.idTarea;
    /* los datos, se mostraran el el formulario */
    this.taskForm.patchValue({
      /* parametros */
      tarea: task.tarea,
      descripcion: task.descripcion,
      idUsuario: task.usuario
    });
    /* activa el displayDialog */
    this.displayDialog = true;
    /* deshabilita dropList de usuarios  */
    this.disable = true;
  }

  /* cargar tareas */
  loadTasks(){
    /* obtiene todos las tareas */
    this.taskService.getTasks().subscribe((data) => {
      this.task = data;
      console.log(this.task);
  })
  }

  /* cargar usuarios */
  loadUsers(){
    /* obtiene todos los usuarios */
    this.userService.getUsers().subscribe((data) => {
      this.usuario = data;
      console.log(this.usuario);
    })
  }

  /* crear tarea */
  createTask(taskData: any){
    /* crea la tarea */
    this.taskService.createTask({
      /* parametros */
      tarea: taskData.tarea,
      descripcion: taskData.descripcion,
      idUsuario: taskData.idUsuario
    }).subscribe(() => {
      this.loadTasks();
      /* cierra el displayDialog */
      this.displayDialog = false;
    })
  }

  /* actualiza */
  updateTask(id: number, taskData: any){
    /* recibe los parametros de saveUpdateTask() */
    this.taskService.updateTask(id,{
      tarea: taskData.tarea,
      descripcion: taskData.descripcion
    }).subscribe(() => {
      this.loadTasks();
      /* cierra el displayDialog */
      this.displayDialog = false;
      /* vacía la variable */
      this.idTarea = null;
    })
  }

  /* eliminar tarea */
  deleteTask(id: number){
    /* recibe el idTarea y lo elimina */
    this.taskService.deleteTask(id).subscribe(() => {
      this.loadTasks();
      })
  }

  /* cambiar estado */
  toggleStatus(id: number){
    /* recibe el idTarea y cambia el estado */
    this.taskService.toggleStatus(id).subscribe(() => {
      this.loadTasks();
    })
  }

  /* abrir dialog - crear*/
  openDialog(){
    /* abre */
    this.displayDialog = true;
    /* resetea parametros */
    this.taskForm.reset();
    /* vacía el idTarea */
    this.idTarea = null;
    /* activa dropList */
    this.disable = false;
  }
}
