create table tarea (
    idTarea serial primary key not null,
    tarea varchar not null,
    descripcion varchar not null,
    completada boolean default false,
    fk_idUsuario int not null,
    foreign key (fk_idUsuario) references usuario(idUsuario)
);

create or replace view v_tareas as
    select
        t.*,
        u.*
        from tarea t 
        inner join usuario u
        on t.fk_idUsuario = u.idUsuario;

create or replace function public.f_createTask (
    p_tarea varchar,
    p_descripcion varchar,
    p_fk_idUsuario int 
)
returns setof v_tareas
language plpgsql
as $function$
declare
    newTaskId int;
begin
    insert into tarea(tarea, descripcion, fk_idUsuario)
        values (p_tarea, p_descripcion, p_fk_idUsuario)

        returning idTarea into newTaskId;
    
    return query select * from v_tareas where idTarea = newTaskId;
end
$function$;

create or replace function public.f_updateTask (
    p_idTarea int,
    p_tarea varchar,
    p_descripcion varchar
)
returns setof v_tareas
language plpgsql
as $function$
begin
    update tarea set 
        tarea = coalesce(p_tarea, tarea),
        descripcion = coalesce(p_descripcion, descripcion)
        where idTarea = p_idTarea;
    
    return query select * from v_tareas where idTarea = p_idTarea;
end
$function$;

create or replace function public.f_removeTask (
    p_idTarea int
)
returns setof v_tareas
language plpgsql
as $function$
declare 
begin 
    create temp table tareaEliminarTabla on commit drop
        as select * from v_tareas where idTarea = p_idTarea;
    
    delete from tarea where idTarea = p_idTarea;

    return query select * from tareaEliminarTabla;
end
$function$

create or replace function public.f_task_toggleStatus (
    p_idTarea int
)
returns setof v_tareas
language plpgsql
as $function$
declare
begin
    update tarea set
        completada = not completada
        where idTarea = p_idTarea;

    return query select * from v_tareas where idTarea = p_idTarea;
end
$function$;

select * from f_createTask (
    p_tarea := 'tarea 2',
    p_descripcion := 'dakkda',
    p_fk_idUsuario := 3
);

select * from f_updateTask (
    p_idTarea := 2,
    p_tarea := 'tarea',
    p_descripcion := 'ganarle al waka waka'
);

select * from f_task_toggleStatus (
    p_idTarea := 1
);

select * from f_removeTask (
    p_idTarea := 1
);

select * from v_tareas where idTarea = 2

select nombresUsuario, tarea, descripcion, completada from v_tareas where fk_idUsuario = 3