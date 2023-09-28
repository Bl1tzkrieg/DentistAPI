create table acceso (
	id integer primary key,
	uuid varchar(32),
	pass varchar(64),
	username varchar(64),
	token varchar(256),
	fk_perfil int not null
);

create table permisos (
	id integer primary key,
	uuid varchar (32),
	rol int not null
);

create table perfil (
	id integer primary key,
	uuid varchar(32),
	fk_permisos int not null,
	fk_persona int not null
);

create table persona (
	id integer primary key,
	uuid varchar(32),
	nombre varchar(128),
	nacimiento date,
	nota varchar(256)
);

create table historia (
	id integer primary key,
	uuid varchar(32),
	fk_persona int not null
);

create table caso (
	id integer primary key,
	uuid varchar(32),
	fk_historia int not null,
	fk_cita int not null,
	fecha date not null,
	rutafichero varchar (256)
);

create table cita (
	id integer primary key,
	uuid varchar(32),
	fk_persona_paciente int not null,
	fk_perfil_doctor int not null,
	rutafichero varchar (256)
);

create table historialcompraventa (
	id integer primary key,
	uuid varchar(129)
	
);

create table operacion (
	id integer primary key,
	uuid varchar(32),
	fk_historialcompraventa int not null,
	fecha date not null,
	monto_crudo int not null,
	tipooperacion int not null,
	estado int not null,
	descripcion varchar(256)
);

create table inventario (
	id integer primary key,
	uuid varchar(32)

);


create table item (
	id integer primary key,
	uuid varchar(32),
	fk_inventario int not null,
	cantidad int not null,
	nombre var(128)
);

create table internaoperacionitem (
	id integer primary key,
	fk_operacion int not null,
	fk_item int not null
);
