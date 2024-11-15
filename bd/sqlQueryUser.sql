-- Active: 1728606315083@@127.0.0.1@5432@postgres

-- tabla 1 --
create table usuario (
    idUsuario serial primary key not null,
    nombresUsuario varchar not null,
    usuarioUsuario varchar not null,
    contrasenaUsuario varchar not null
);

-- view usuarios --
create or replace view v_usuarios as 
    select * from usuario;
    
create or replace function public.f_createUser (

    p_nombresUsuario varchar,
    p_usuarioUsuario varchar,
    p_contrasenaUsuario varchar

)
returns setof v_usuarios
language plpgsql
as $function$
declare 

    nuevoUsuarioId int;

begin

    insert into usuario (nombresUsuario, usuarioUsuario, contrasenaUsuario)
        values (p_nombresUsuario, p_usuarioUsuario, p_contrasenaUsuario)

    returning idUsuario into nuevoUsuarioId;

    return query select * from v_usuarios where idUsuario = nuevoUsuarioId;

end
$function$;

create or replace function public.f_updateUser (

    p_idUsuario int,
    p_nombresUsuario varchar default null,
    p_usuarioUsuario varchar default null,
    p_contrasenaUsuario varchar default null

)
returns setof v_usuarios
language plpgsql
as $function$
declare 

begin

    update usuario set
                    -- coalesce: selecciona el primer elemento no nulo
    nombresUsuario = coalesce(p_nombresUsuario, nombresUsuario),
    usuarioUsuario = coalesce(p_usuarioUsuario, usuarioUsuario),
    contrasenaUsuario = coalesce(p_contrasenaUsuario, contrasenaUsuario)
    where idUsuario = p_idUsuario;

    return query select * from v_usuarios where idUsuario = p_idUsuario;

end
$function$;

create or replace function public.f_removeUser (

    p_idUsuario int

)
returns setof v_usuarios
language plpgsql
as $function$
declare 

begin

    -- tabla temporal con el usuario
    create temp table usuarioEliminadoTabla on commit drop 
        as select * from v_usuarios where idUsuario = p_idUsuario;

    delete from usuario where idUsuario = p_idUsuario;

    return query select * from usuarioEliminadoTabla;

end
$function$;

select * from f_createUser (
    p_nombresUsuario := 'juanda',
    p_usuarioUsuario := 'juan_daniel',
    p_contrasenaUsuario := 'juan1'
);

select * from f_updateUser (
    p_idUsuario := 1,
    p_nombresUsuario := 'juanda',
    p_usuarioUsuario := 'juan_monki',
    p_contrasenaUsuario := 'juan1'
);

select * from f_removeUser (
    p_idUsuario := 2
);

select * from v_usuarios