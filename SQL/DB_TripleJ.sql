USE Solitel_Database;

-- DROPs
BEGIN
	-- Comando para borrar todas las tablas
	DROP TABLE TSOLITEL_RequerimientoProveedor_DatoRequerido;
	DROP TABLE TSOLITEL_DatoRequerido;
	DROP TABLE TSOLITEL_TipoDato;
	DROP TABLE TSOLITEL_TipoSolicitud_RequerimientoProveedor;
	DROP TABLE TSOLITEL_TipoSolicitud;
	DROP TABLE TSOLITEL_RequerimientoProveedor;
	DROP TABLE TSOLITEL_SolicitudAnalisis_Condicion;
	DROP TABLE TSOLITEL_Condicion;
	DROP TABLE TSOLITEL_TipoAnalisis_SolicitudAnalisis;
	DROP TABLE TSOLITEL_TipoAnalisis;
	DROP TABLE TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis;
	DROP TABLE TSOLITEL_ObjetivoAnalisis;
	DROP TABLE TSOLITEL_Rol_Permiso;
	DROP TABLE TSOLITEL_Usuario_Oficina;
	DROP TABLE TSOLITEL_SolicitudProveedor;
	DROP TABLE TSOLITEL_SolicitudAnalisis;
	DROP TABLE TSOLITEL_Delito;
	DROP TABLE TSOLITEL_CategoriaDelito;
	DROP TABLE TSOLITEL_Estado;
	DROP TABLE TSOLITEL_Fiscalia;
	DROP TABLE TSOLITEL_Historial;
	DROP TABLE TSOLITEL_Logger;
	DROP TABLE TSOLITEL_Modalidad;
	DROP TABLE TSOLITEL_SubModalidad;
	DROP TABLE TSOLITEL_Oficina;	
	DROP TABLE TSOLITEL_Permiso;
	DROP TABLE TSOLITEL_Proveedor;
	DROP TABLE TSOLITEL_Rol;
	DROP TABLE TSOLITEL_Usuario;
END
GO

-- Modulo De Seguridad
BEGIN
	-- Tabla TSOLITEL_Usuario
	CREATE TABLE TSOLITEL_Usuario (
		TN_IdUsuario INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Apellido VARCHAR(100) NOT NULL,
		TC_Usuario VARCHAR(50) NOT NULL,
		TC_Contrasenna VARCHAR(255) NOT NULL,
		TC_CorreoElectronico VARCHAR(100) NOT NULL,
		CONSTRAINT [PK_TSOLITEL_Usuario] PRIMARY KEY NONCLUSTERED (TN_IdUsuario ASC)
	);

	-- Tabla TSOLITEL_Rol
	CREATE TABLE TSOLITEL_Rol (
		TN_IdRol INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		CONSTRAINT [PK_TSOLITEL_Rol] PRIMARY KEY NONCLUSTERED (TN_IdRol ASC)
	);

	-- Tabla TSOLITEL_Permiso
	CREATE TABLE TSOLITEL_Permiso (
		TN_IdPermiso INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_Permiso] PRIMARY KEY NONCLUSTERED (TN_IdPermiso ASC)
	);

	-- Tabla TSOLITEL_Oficina
	CREATE TABLE TSOLITEL_Oficina (
		TN_IdOficina INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_Oficina] PRIMARY KEY NONCLUSTERED (TN_IdOficina ASC)
	);

	-- Tabla TSOLITEL_Rol_Permiso
	CREATE TABLE TSOLITEL_Rol_Permiso (
		TN_IdRol INT NOT NULL,
		TN_IdPermiso INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_Rol_Permiso] PRIMARY KEY NONCLUSTERED (TN_IdRol, TN_IdPermiso ASC),
		CONSTRAINT FK_TSOLITEL_Rol_Permiso_TSOLITEL_Rol FOREIGN KEY (TN_IdRol) REFERENCES TSOLITEL_Rol (TN_IdRol),
		CONSTRAINT FK_TSOLITEL_Rol_Permiso_TSOLITEL_Permiso FOREIGN KEY (TN_IdPermiso) REFERENCES TSOLITEL_Permiso (TN_IdPermiso)
	);

	-- Tabla TSOLITEL_Usuario_Oficina
	CREATE TABLE TSOLITEL_Usuario_Oficina (
		TN_IdUsuario INT NOT NULL,
		TN_IdOficina INT NOT NULL,
		TN_IdRol INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_Usuario_Oficina] PRIMARY KEY NONCLUSTERED (TN_IdUsuario, TN_IdOficina ASC),
		CONSTRAINT FK_TSOLITEL_Usuario_Oficina_TSOLITEL_Usuario FOREIGN KEY (TN_IdUsuario) REFERENCES TSOLITEL_Usuario (TN_IdUsuario),
		CONSTRAINT FK_TSOLITEL_Usuario_Oficina_TSOLITEL_Oficina FOREIGN KEY (TN_IdOficina) REFERENCES TSOLITEL_Oficina (TN_IdOficina),
		CONSTRAINT FK_TSOLITEL_Usuario_Oficina_TSOLITEL_Rol FOREIGN KEY (TN_IdRol) REFERENCES TSOLITEL_Rol (TN_IdRol)
	);
END
GO

-- Modulo Principal
BEGIN
	-- Tabla TSOLITEL_Proveedor
	CREATE TABLE TSOLITEL_Proveedor (
		TN_IdProveedor INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_Proveedor] PRIMARY KEY NONCLUSTERED (TN_IdProveedor ASC)
	);

	-- Tabla TSOLITEL_CategoriaDelito
	CREATE TABLE TSOLITEL_CategoriaDelito (
		TN_IdCategoriaDelito INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_CategoriaDelito] PRIMARY KEY NONCLUSTERED (TN_IdCategoriaDelito ASC)
	);

	-- Tabla TSOLITEL_Delito
	CREATE TABLE TSOLITEL_Delito (
		TN_IdDelito INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		TN_IdCategoriaDelito INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_Delito] PRIMARY KEY NONCLUSTERED (TN_IdDelito ASC),
		CONSTRAINT FK_TSOLITEL_Delito_TSOLITEL_CategoriaDelito FOREIGN KEY (TN_IdCategoriaDelito) REFERENCES TSOLITEL_CategoriaDelito (TN_IdCategoriaDelito)
	);

	-- Tabla TSOLITEL_Estado
	CREATE TABLE TSOLITEL_Estado (
		TN_IdEstado INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TC_Tipo VARCHAR(50) NOT NULL,
		CONSTRAINT [PK_TSOLITEL_Estado] PRIMARY KEY NONCLUSTERED (TN_IdEstado ASC)
	);

	-- Tabla TSOLITEL_Modalidad
	CREATE TABLE TSOLITEL_Modalidad (
		TN_IdModalidad INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_Modalidad] PRIMARY KEY NONCLUSTERED (TN_IdModalidad ASC)
	);

	-- Tabla TSOLITEL_SubModalidad
	CREATE TABLE TSOLITEL_SubModalidad (
		TN_IdSubModalidad INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		TN_IdModalida INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_SubModalidad] PRIMARY KEY NONCLUSTERED (TN_IdSubModalidad ASC),
		CONSTRAINT FK_TSOLITEL_SubModalidad_TSOLITEL_Modalidad FOREIGN KEY (TN_IdModalida) REFERENCES TSOLITEL_Modalidad (TN_IdModalidad)
	);

	-- Tabla TSOLITEL_Fiscalia
	CREATE TABLE TSOLITEL_Fiscalia (
		TN_IdFiscalia INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_Fiscalia] PRIMARY KEY NONCLUSTERED (TN_IdFiscalia ASC)
	);

	-- Tabla TSOLITEL_TipoSolicitud
	CREATE TABLE TSOLITEL_TipoSolicitud (
		TN_IdTipoSolicitud INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_TipoSolicitud] PRIMARY KEY NONCLUSTERED (TN_IdTipoSolicitud ASC)
	);

	-- Tabla TSOLITEL_TipoDato
	CREATE TABLE TSOLITEL_TipoDato (
		TN_IdTipoDato INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_TipoDato] PRIMARY KEY NONCLUSTERED (TN_IdTIpoDato ASC)
	);

	-- Tabla SolicitudProveedor
	CREATE TABLE TSOLITEL_SolicitudProveedor (
		TN_IdSolicitudProveedor INT IDENTITY NOT NULL,
		TN_NumeroUnico VARCHAR(100) NULL,
		TN_NumeroCaso VARCHAR(100) NULL,
		TC_Imputado VARCHAR(150) NOT NULL,
		TC_Ofendido VARCHAR(150) NOT NULL,
		TC_Resennia VARCHAR(255) NULL,
		TB_Urgente BIT NOT NULL,
		TB_Aprobado BIT NOT NULL,
		TF_FechaCrecion DATETIME2 NOT NULL,
		TN_IdUsuarioCreador INT NOT NULL,
		TN_IdDelito INT NOT NULL,
		TN_IdCategoriaDelito INT NOT NULL,
		TN_IdModalida INT NOT NULL,
		TN_IdSubModalidad INT NULL,
		TN_IdEstado INT NOT NULL,
		TN_IdProveedor INT NOT NULL,
		TN_IdFiscalia INT NOT NULL,
		TN_IdOficina INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_SolicitudProveedor] PRIMARY KEY NONCLUSTERED (TN_IdSolicitudProveedor ASC),
		--CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_Usuario FOREIGN KEY (TN_IdUsuarioCreador) REFERENCES TSOLITEL_Usuario (TN_IdUsuario),
		CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_Delito FOREIGN KEY (TN_IdDelito) REFERENCES TSOLITEL_Delito (TN_IdDelito),
		CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_CategoriaDelito FOREIGN KEY (TN_IdCategoriaDelito) REFERENCES TSOLITEL_CategoriaDelito (TN_IdCategoriaDelito),
		CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_Modalidad FOREIGN KEY (TN_IdModalida) REFERENCES TSOLITEL_Modalidad (TN_IdModalidad),
		CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_SubModalidad FOREIGN KEY (TN_IdSubModalidad) REFERENCES TSOLITEL_SubModalidad (TN_IdSubModalidad),
		CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_Estado FOREIGN KEY (TN_IdEstado) REFERENCES TSOLITEL_Estado (TN_IdEstado),
		CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_Proveedor FOREIGN KEY (TN_IdProveedor) REFERENCES TSOLITEL_Proveedor (TN_IdProveedor),
		CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_Fiscalia FOREIGN KEY (TN_IdFiscalia) REFERENCES TSOLITEL_Fiscalia (TN_IdFiscalia),
		--CONSTRAINT FK_TSOLITEL_SolicitudProveedor_TSOLITEL_Oficina FOREIGN KEY (TN_IdOficina) REFERENCES TSOLITEL_Oficina (TN_IdOficina)
	);

	-- Tabla TSOLITEL_RequerimientoProveedor
	CREATE TABLE TSOLITEL_RequerimientoProveedor (
		TN_IdRequerimientoProveedor INT IDENTITY NOT NULL,
		TF_FechaInicio DATETIME2 NOT NULL,
		TF_FechaFinal DATETIME2 NOT NULL,
		TC_Requerimiento VARCHAR(255) NOT NULL,
		CONSTRAINT [PK_TSOLITEL_RequerimientoProveedor] PRIMARY KEY NONCLUSTERED (TN_IdRequerimientoProveedor ASC),
		--CONSTRAINT FK_TSOLITEL_RequerimientoProveedor_TSOLITEL_SolicitudProveedor FOREIGN KEY (TN_NumeroSolicitud) REFERENCES TSOLITEL_SolicitudProveedor (TN_IdSolicitudProveedor)
	);

	-- Tabla Intermedia TSOLITEL_SolicitudRequerimientoProveedor
	CREATE TABLE TSOLITEL_SolicitudProveedor_RequerimientoProveedor (
		TN_IdSolicitudProveedor INT NOT NULL,
		TN_IdRequerimientoProveedor INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_SolicitudRequerimientoProveedor] PRIMARY KEY NONCLUSTERED (TN_IdSolicitudProveedor, TN_IdRequerimientoProveedor ASC),
		CONSTRAINT FK_TSOLITEL_SolicitudRequerimientoProveedor_TSOLITEL_SolicitudProveedor FOREIGN KEY (TN_IdSolicitudProveedor) REFERENCES TSOLITEL_SolicitudProveedor (TN_IdSolicitudProveedor),
		CONSTRAINT FK_TSOLITEL_SolicitudRequerimientoProveedor_TSOLITEL_RequerimientoProveedor FOREIGN KEY (TN_IdRequerimientoProveedor) REFERENCES TSOLITEL_RequerimientoProveedor (TN_IdRequerimientoProveedor)
	);

	-- Tabla TSOLITEL_DatoRequerido
	CREATE TABLE TSOLITEL_DatoRequerido (
		TN_IdDatoRequerido INT IDENTITY NOT NULL,
		TC_DatoRequerido VARCHAR(255) NOT NULL,
		TC_Motivacion VARCHAR(255) NOT NULL,
		TN_IdTipoDato INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_DatoRequerido] PRIMARY KEY NONCLUSTERED (TN_IdDatoRequerido ASC),
		CONSTRAINT FK_TSOLITEL_DatoRequerido_TSOLITEL_TipoDato FOREIGN KEY (TN_IdTipoDato) REFERENCES TSOLITEL_TipoDato (TN_IdTIpoDato)
	);

	-- Tabla TSOLITEL_RequerimientoProveedor_DatoRequerido
	CREATE TABLE TSOLITEL_RequerimientoProveedor_DatoRequerido (
		TN_IdRequerimientoProveedor INT NOT NULL,
		TN_IdDatoRequerido INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_RequerimientoProveedor_DatoRequerido] PRIMARY KEY NONCLUSTERED (TN_IdRequerimientoProveedor, TN_IdDatoRequerido ASC),
		CONSTRAINT FK_TSOLITEL_RequerimientoProveedor_DatoRequerido_TSOLITEL_RequerimientoProveedor FOREIGN KEY (TN_IdRequerimientoProveedor) REFERENCES TSOLITEL_RequerimientoProveedor (TN_IdRequerimientoProveedor),
		CONSTRAINT FK_TSOLITEL_RequerimientoProveedor_DatoRequerido_TSOLITEL_DatoRequerido FOREIGN KEY (TN_IdDatoRequerido) REFERENCES TSOLITEL_DatoRequerido (TN_IdDatoRequerido)
	);

-- PENDIENTE DE REVISAR
-- Tabla TSOLITEL_Archivo
CREATE TABLE TSOLITEL_Archivo (
    TN_IdArchivo INT IDENTITY NOT NULL,
    TC_HashAchivo VARCHAR(255) NOT NULL UNIQUE,
    TC_Nombre INT NOT NULL,
    TV_Contenido BINARY(1000) NOT NULL,
    TC_FormatoAchivo VARCHAR(50) NOT NULL,
    TF_FechaModificacion DATE NOT NULL,
    TN_IdRequerimiento INT NULL,
    CONSTRAINT [PK_TSOLITEL_Archivo] PRIMARY KEY NONCLUSTERED (TN_IdArchivo ASC),
    CONSTRAINT FK_TSOLITEL_Archivo_TSOLITEL_RequerimientoProveedor FOREIGN KEY (TN_IdRequerimiento) REFERENCES TSOLITEL_RequerimientoProveedor (TN_IdRequerimientoProveedor)
);

	-- Tabla TSOLITEL_SolicitudAnalisis
	CREATE TABLE TSOLITEL_SolicitudAnalisis (
		TN_IdSolicitudAnalisis INT IDENTITY NOT NULL,
		TF_FechaDelHecho DATETIME2 NOT NULL,
		TC_OtrosDetalles VARCHAR(255) NOT NULL,
		TC_OtrosObjetivosDeAnalisis VARCHAR(255) NULL,
		TB_Aprobado BIT NOT NULL,
		TF_FechaCrecion DATETIME2 NULL,
		--TN_NumeroSolicitud INT NOT NULL,
		TN_IdOficina INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_SolicitudAnalisis] PRIMARY KEY NONCLUSTERED (TN_IdSolicitudAnalisis ASC),
		--CONSTRAINT FK_TSOLITEL_SolicitudAnalisis_TSOLITEL_SolicitudProveedor FOREIGN KEY (TN_NumeroSolicitud) REFERENCES TSOLITEL_SolicitudProveedor (TN_IdSolicitudProveedor),
		--CONSTRAINT FK_TSOLITEL_SolicitudAnalisis_TSOLITEL_Oficina FOREIGN KEY (TN_IdOficina) REFERENCES TSOLITEL_Oficina (TN_IdOficina)
	);

	-- Tabla Intermedia SolicitudAnalisis_SolicitudProveedor
	CREATE TABLE TSOLITEL_SolicitudAnalisis_SolicitudProveedor (
		TN_IdSolicitudAnalisis INT NOT NULL,
		TN_IdSolicitudProveedor INT NOT NULL,
		CONSTRAINT [PK_SolicitudAnalisis_SolicitudProveedor] PRIMARY KEY NONCLUSTERED (TN_IdSolicitudAnalisis, TN_IdSolicitudProveedor ASC),
		CONSTRAINT FK_SolicitudAnalisis_SolicitudProveedor_Analisis FOREIGN KEY (TN_IdSolicitudAnalisis) REFERENCES TSOLITEL_SolicitudAnalisis (TN_IdSolicitudAnalisis),
		CONSTRAINT FK_SolicitudAnalisis_SolicitudProveedor_SolicitudProveedor FOREIGN KEY (TN_IdSolicitudProveedor) REFERENCES TSOLITEL_SolicitudProveedor (TN_IdSolicitudProveedor)
	);

	-- Tabla TSOLITEL_RequerimentoAnalisis
	CREATE TABLE TSOLITEL_RequerimentoAnalisis (
		TN_IdRequerimientoAnalisis INT IDENTITY NOT NULL,
		TC_Objetivo VARCHAR(255) NOT NULL,
		TC_UtilizadoPor VARCHAR(255) NOT NULL,
		TN_IdTipoDato INT NOT NULL,
		TN_IdAnalisis INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_RequerimentoAnalisis] PRIMARY KEY NONCLUSTERED (TN_IdRequerimientoAnalisis ASC),
		CONSTRAINT FK_TSOLITEL_RequerimentoAnalisis_TSOLITEL_TipoDato FOREIGN KEY (TN_IdTipoDato) REFERENCES TSOLITEL_TipoDato (TN_IdTIpoDato),
		CONSTRAINT FK_TSOLITEL_RequerimentoAnalisis_TSOLITEL_SolicitudAnalisis FOREIGN KEY (TN_IdAnalisis) REFERENCES TSOLITEL_SolicitudAnalisis (TN_IdSolicitudAnalisis)
	);

-- PENDIENTE DE REVISION
-- Tabla TSOLITEL_Asignacion
CREATE TABLE TSOLITEL_Asignacion (
    TN_IdUsuario INT NOT NULL,
    TN_IdAnalisis INT NULL,
    TN_NumeroSolicitud INT NULL,
    TF_FechaModificacion DATE NOT NULL,
    CONSTRAINT [PK_TSOLITEL_Asignacion] PRIMARY KEY NONCLUSTERED (TN_IdUsuario ASC),
    CONSTRAINT FK_TSOLITEL_Asignacion_TSOLITEL_Usuario FOREIGN KEY (TN_IdUsuario) REFERENCES TSOLITEL_Usuario (TN_IdUsuario),
    CONSTRAINT FK_TSOLITEL_Asignacion_TSOLITEL_SolicitudAnalisis FOREIGN KEY (TN_IdAnalisis) REFERENCES TSOLITEL_SolicitudAnalisis (TN_IdSolicitudAnalisis),
    CONSTRAINT FK_TSOLITEL_Asignacion_TSOLITEL_SolicitudProveedor FOREIGN KEY (TN_NumeroSolicitud) REFERENCES TSOLITEL_SolicitudProveedor (TN_IdSolicitudProveedor)
);

	-- Tabla TSOLITEL_ObjetivoAnalisis
	CREATE TABLE TSOLITEL_ObjetivoAnalisis (
		TN_IdObjetivoAnalisis INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_ObjetivoAnalisis] PRIMARY KEY NONCLUSTERED (TN_IdObjetivoAnalisis ASC)
	);

	-- Tabla TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis
	CREATE TABLE TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis (
		TN_IdObjetivo INT NOT NULL,
		TN_IdAnalisis INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis] PRIMARY KEY NONCLUSTERED (TN_IdObjetivo, TN_IdAnalisis ASC),
		CONSTRAINT FK_TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis_TSOLITEL_ObjetivoAnalisis FOREIGN KEY (TN_IdObjetivo) REFERENCES TSOLITEL_ObjetivoAnalisis (TN_IdObjetivoAnalisis),
		CONSTRAINT FK_TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis_TSOLITEL_SolicitudAnalisis FOREIGN KEY (TN_IdAnalisis) REFERENCES TSOLITEL_SolicitudAnalisis (TN_IdSolicitudAnalisis)
	);

	-- Tabla TSOLITEL_Condicion
	CREATE TABLE TSOLITEL_Condicion (
		TN_IdCondicion INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_Condicion] PRIMARY KEY NONCLUSTERED (TN_IdCondicion ASC)
	);

	-- Tabla TSOLITEL_SolicitudAnalisis_Condicion
	CREATE TABLE TSOLITEL_SolicitudAnalisis_Condicion (
		TN_IdAnalisis INT NOT NULL,
		TN_IdCondicion INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_SolicitudAnalisis_Condicion] PRIMARY KEY NONCLUSTERED (TN_IdAnalisis, TN_IdCondicion ASC),
		CONSTRAINT FK_TSOLITEL_SolicitudAnalisis_Condicion_TSOLITEL_SolicitudAnalisis FOREIGN KEY (TN_IdAnalisis) REFERENCES TSOLITEL_SolicitudAnalisis (TN_IdSolicitudAnalisis),
		CONSTRAINT FK_TSOLITEL_SolicitudAnalisis_Condicion_TSOLITEL_Condicion FOREIGN KEY (TN_IdCondicion) REFERENCES TSOLITEL_Condicion (TN_IdCondicion)
	);

	-- Tabla TSOLITEL_TipoAnalisis
	CREATE TABLE TSOLITEL_TipoAnalisis (
		TN_IdTipoAnalisis INT IDENTITY NOT NULL,
		TC_Nombre VARCHAR(50) NOT NULL,
		TC_Descripcion VARCHAR(255) NOT NULL,
		TB_Borrado BIT DEFAULT 0,
		CONSTRAINT [PK_TSOLITEL_TipoAnalisis] PRIMARY KEY NONCLUSTERED (TN_IdTipoAnalisis ASC)
	);

	-- Tabla TSOLITEL_TipoAnalisis_SolicitudAnalisis
	CREATE TABLE TSOLITEL_TipoAnalisis_SolicitudAnalisis (
		TN_IdTipoAnalisis INT NOT NULL,
		TN_IdAnalisis INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_TipoAnalisis_SolicitudAnalisis] PRIMARY KEY NONCLUSTERED (TN_IdTipoAnalisis, TN_IdAnalisis ASC),
		CONSTRAINT FK_TSOLITEL_TipoAnalisis_SolicitudAnalisis_TSOLITEL_TipoAnalisis FOREIGN KEY (TN_IdTipoAnalisis) REFERENCES TSOLITEL_TipoAnalisis (TN_IdTipoAnalisis),
		CONSTRAINT FK_TSOLITEL_TipoAnalisis_SolicitudAnalisis_TSOLITEL_SolicitudAnalisis FOREIGN KEY (TN_IdAnalisis) REFERENCES TSOLITEL_SolicitudAnalisis (TN_IdSolicitudAnalisis)
	);

	-- Tabla TSOLITEL_Logger
	CREATE TABLE TSOLITEL_Logger (
		TN_IdLogger INT IDENTITY NOT NULL,
		TC_Accion VARCHAR(255) NOT NULL,
		TC_DescripcionEvento VARCHAR(255) NULL,
		TC_InformacionExtra VARCHAR(255) NULL,
		TF_FechaEvento DATETIME2 NOT NULL,
		CONSTRAINT [PK_TSOLITEL_Logger] PRIMARY KEY NONCLUSTERED (TN_IdLogger ASC)
	);

-- PENDIENTE DE REVISION
-- Tabla TSOLITEL_SolicitudAnalisis_Archivo
CREATE TABLE TSOLITEL_SolicitudAnalisis_Archivo (
    TN_IdAnalisis INT NOT NULL,
    TN_IdAchivo INT NOT NULL,
    TC_TipoArchivo VARCHAR(10) NOT NULL,
    CONSTRAINT [PK_TSOLITEL_SolicitudAnalisis_Archivo] PRIMARY KEY NONCLUSTERED (TN_IdAnalisis, TN_IdAchivo ASC),
    CONSTRAINT FK_TSOLITEL_SolicitudAnalisis_Archivo_TSOLITEL_SolicitudAnalisis FOREIGN KEY (TN_IdAnalisis) REFERENCES TSOLITEL_SolicitudAnalisis (TN_IdSolicitudAnalisis),
    CONSTRAINT FK_TSOLITEL_SolicitudAnalisis_Archivo_TSOLITEL_Archivo FOREIGN KEY (TN_IdAchivo) REFERENCES TSOLITEL_Archivo (TN_IdArchivo)
);

	-- Tabla TSOLITEL_TipoSolicitud_RequerimientoProveedor
	CREATE TABLE TSOLITEL_TipoSolicitud_RequerimientoProveedor (
		TN_IdTipoSolicitud INT NOT NULL,
		TN_IdRequerimientoProveedor INT NOT NULL,
		CONSTRAINT [PK_TSOLITEL_TipoSolicitud_RequerimientoProveedor] PRIMARY KEY NONCLUSTERED (TN_IdTipoSolicitud, TN_IdRequerimientoProveedor ASC),
		CONSTRAINT FK_TSOLITEL_TipoSolicitud_RequerimientoProveedor_TSOLITEL_TipoSolicitud FOREIGN KEY (TN_IdTipoSolicitud) REFERENCES TSOLITEL_TipoSolicitud (TN_IdTipoSolicitud),
		CONSTRAINT FK_TSOLITEL_TipoSolicitud_TipoSolicitud_TSOLITEL_RequerimientoProveedor FOREIGN KEY (TN_IdRequerimientoProveedor) REFERENCES TSOLITEL_RequerimientoProveedor (TN_IdRequerimientoProveedor)
	);

	-- Tabla TSOLITEL_Historial
	CREATE TABLE TSOLITEL_Historial (
		TN_IdHistorial INT IDENTITY NOT NULL,
		TC_Observacion VARCHAR(255) NOT NULL,
		TF_FechaEstado DATETIME2 NOT NULL,
		TN_IdUsuario INT NOT NULL,
		TN_IdEstado INT NOT NULL,
		TN_IdAnalisis INT NULL,
		TN_IdSolicitudProveedor INT NULL,
		CONSTRAINT [PK_TSOLITEL_Historial] PRIMARY KEY NONCLUSTERED (TN_IdHistorial ASC),
		--CONSTRAINT FK_TSOLITEL_Historial_TSOLITEL_Usuario FOREIGN KEY (TN_IdUsuario) REFERENCES TSOLITEL_Usuario (TN_IdUsuario),
		CONSTRAINT FK_TSOLITEL_Historial_TSOLITEL_Estado FOREIGN KEY (TN_IdEstado) REFERENCES TSOLITEL_Estado (TN_IdEstado),
		CONSTRAINT FK_TSOLITEL_Historial_SolicitudProveedor FOREIGN KEY (TN_IdSolicitudProveedor) REFERENCES TSOLITEL_SolicitudProveedor (TN_IdSolicitudProveedor)
	);
END
GO