-- Paso 1: Insertar en TSOLITEL_Estado (Proveedores y Análisis)
USE Proyecto_Analisis;
INSERT INTO [dbo].[TSOLITEL_Estado] ([TC_Nombre], [TC_Descripcion], [TC_Tipo])
VALUES
    ('Sin Efecto', 'Una solicitud no tramitada deja de ser valida o necesaria', 'Proveedor'),
    ('Legajo', 'Una solicitud tramitada deja de ser valida o necesaria', 'Proveedor'),
    ('Creado', 'Una solicitud es aprobada por la jefatura correspondiente', 'Proveedor'),
    ('Pendiente', 'Una solicitud es creada pero esta pendiente de aprobacion', 'Proveedor'),
    ('Tramitado', 'Una solicitud fue espera ser respondida por un provedor de telecomunicaciones', 'Proveedor'),
    ('Finalizado', 'Una solicitud tramitada que la fue analizada por el investigador o fiscal solicitante', 'Proveedor'),
    ('Solicitado', '', 'Proveedor'),
    ('Sin Efecto', 'Una solicitud de analisis no tramitada deja de ser valida o necesaria', 'Analisis'),
    ('Legajo', 'Una solicitud de analisis tramitada deja de ser valida o necesaria', 'Analisis'),
    ('Finalizado', 'Una solicitud de analisis respondida ya fue estudiada por el fiscal o investigador', 'Analisis'),
    ('Aprobar Analisis', 'Una solicitud debe ser aprobada por la jefatura correspondiente', 'Analisis'),
    ('En Analisis', 'Una solicitud es aprobada por la jefatura correspondiente', 'Analisis'),
    ('Analizando', 'Una solicitud esta en proceso de ser respondida por los analistas', 'Analisis');
GO

-- Paso 2: Insertar en TSOLITEL_Condicion
INSERT INTO [dbo].[TSOLITEL_Condicion] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES 
    ('Abonado', 'Descripción de Abonado', 0),
    ('Usuario', 'Descripción de Usuario', 0),
    ('Aportado por', 'Descripción de Aportado por', 0);
GO

-- Paso 3: Insertar en TSOLITEL_ObjetivoAnalisis
INSERT INTO [dbo].[TSOLITEL_ObjetivoAnalisis] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES 
    ('Consulta en la base Maestra', 'Descripción de Consulta en la base Maestra', 0),
    ('Cronología de comunicaciones', 'Descripción de Cronología de comunicaciones', 0),
    ('Cronología de comunicaciones con radio bases', 'Descripción de Cronología de comunicaciones con radio bases', 0),
    ('Cronología de llamadas internacionales', 'Descripción de Cronología de llamadas internacionales', 0),
    ('Frecuencia de comunicaciones', 'Descripción de Frecuencia de comunicaciones', 0),
    ('Flujogramas entre observados', 'Descripción de Flujogramas entre observados', 0),
    ('Flujogramas de comunicaciones en común', 'Descripción de Flujogramas de comunicaciones en común', 0),
    ('Flujogramas de llamadas internacionales', 'Descripción de Flujogramas de llamadas internacionales', 0),
    ('Flujogramas de uso de IMEI', 'Descripción de Flujogramas de uso de IMEI', 0),
    ('Mapas de radio bases', 'Descripción de Mapas de radio bases', 0),
    ('Sumatoria de radio bases', 'Descripción de Sumatoria de radio bases', 0),
    ('Utilización de IMEI', 'Descripción de Utilización de IMEI', 0),
    ('Otros', 'Descripción de Otros', 0);
GO

-- Paso 4: Insertar en TSOLITEL_Oficina
INSERT INTO [dbo].[TSOLITEL_Oficina] (TC_Nombre, TC_Tipo, TB_Borrado)
VALUES 
    ('1101-CENTRO JUDICIAL DE INTERVENCION DE LAS COMUNICACIONES (CJIC)', 'Descripción de Centro Judicial de Intervención de las Comunicaciones', 0),
    ('0059-DELEGACION REGIONAL DE HEREDIA', 'Descripción de Delegación Regional de Heredia', 0),
    ('0060-DELEGACION REGIONAL DE LIBERIA', 'Descripción de Delegación Regional de Liberia', 0),
    ('0033-FISCALIA GENERAL', 'Descripción de Fiscalía General', 0),
    ('1982-OFICINA ESPECIALIZADA CONTRA LA DELINCUENCIA ORGANIZADA (OECDO)', 'Descripción de Oficina Especializada Contra la Delincuencia Organizada', 0),
    ('1132-PLATAFORMA DE INFORMACION POLICIAL', 'Descripción de Plataforma de Información Policial', 0),
    ('0051-SECCION DELITOS VARIOS', 'Descripción de Sección Delitos Varios', 0),
    ('0682-SECCION ESPECIALIZADA CONTRA EL CIBERCRIMEN', 'Descripción de Sección Especializada Contra el Cibercrimen', 0),
    ('2026-SECCION ESPECIALIZADA CONTRA EL FRAUDE INFORMATICO (SECFI)', 'Descripción de Sección Especializada Contra el Fraude Informático', 0),
    ('0048-SECCION HOMICIDIOS', 'Descripción de Sección Homicidios', 0),
    ('0955-UNIDAD DE ANALISIS CRIMINAL', 'Descripción de Unidad de Análisis Criminal', 0),
    ('1134-UNIDAD DE RECOLECCION DE INFORMACION POLICIAL', 'Descripción de Unidad de Recolección de Información Policial', 0);
GO

-- Paso 5: Insertar en TSOLITEL_TipoDato
INSERT INTO [dbo].[TSOLITEL_TipoDato] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES 
    ('Número Nacional', 'Descripción de Número Nacional', 0),
    ('Número Internacional', 'Descripción de Número Internacional', 0),
    ('IMEI', 'Descripción de IMEI', 0),
    ('SIM', 'Descripción de SIM', 0),
    ('IP', 'Descripción de IP', 0),
    ('Radio Base', 'Descripción de Radio Base', 0),
    ('Otro', 'Descripción de Otro', 0);
GO

-- Paso 6: Insertar en TSOLITEL_Modalidad
INSERT INTO [dbo].[TSOLITEL_Modalidad] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES 
    ('ARDID PREVIO/DISTRACCION', 'Descripción de Ardid Previo/Distracción', 0),
    ('CARTERISTA', 'Descripción de Carterista', 0),
    ('CON LLAVE', 'Descripción de Con Llave', 0),
    ('GANZUA/VARILLA', 'Descripción de Ganzúa/Varilla', 0),
    ('OTRO O INDETERMINADO', 'Descripción de Otro o Indeterminado', 0),
    ('POR CONFIANZA', 'Descripción de Por Confianza', 0),
    ('POR DESCUIDO', 'Descripción de Por Descuido', 0),
    ('PROGRESIVOS', 'Descripción de Progresivos', 0),
    ('USO DE SOMNIFERO', 'Descripción de Uso de Somnífero', 0),
    ('RETIRO DE CAJERO AUTOMATICO', 'Descripción de Retiro de Cajero Automático', 0);
GO

-- Paso 7: Insertar en TSOLITEL_SubModalidad
INSERT INTO [dbo].[TSOLITEL_SubModalidad] (TC_Nombre, TC_Descripcion, TB_Borrado, TN_IdModalida)
VALUES
    ('Robo en Multitudes', 'Robo cometido aprovechando el tumulto de personas', 0, 2),
    ('Robo en Transporte Público', 'Robo cometido dentro de buses, trenes, u otros medios públicos', 0, 2),
    ('Hurto de Cartera', 'Sustracción de la cartera sin violencia', 0, 2),
    ('Forzamiento de Cerradura', 'Ingreso mediante el uso de ganzúa o varilla', 0, 4),
    ('Llave Maestra', 'Uso de llave maestra para ingresar sin causar daño', 0, 3),
    ('Distraer para Robar', 'Uso de distracción para robar a la víctima', 0, 1),
    ('Confianza Maliciosa', 'Robo aprovechando la confianza de la víctima', 0, 6),
    ('Descuidos Públicos', 'Robo aprovechando el descuido en lugares públicos', 0, 7);
GO

-- Paso 8: Insertar en TSOLITEL_Proveedor
INSERT INTO [dbo].[TSOLITEL_Proveedor] (TC_Nombre, TB_Borrado)
VALUES 
    ('CLARO', 0),
    ('ICE', 0),
    ('TIGO', 0),
    ('LIBERTY', 0);
GO

-- Paso 9: Insertar en TSOLITEL_Usuario
INSERT INTO [dbo].[TSOLITEL_Usuario]
           ([TC_Nombre], [TC_Apellido], [TC_Usuario], [TC_Contrasennia], [TC_CorreoElectronico])
VALUES
           ('Eliecer', 'Melgara', 'eliecer.melgara', 'contrasenniaSegura123', 'eliecermelgara1680@gmail.com');
GO

-- Paso 10: Insertar en TSOLITEL_TipoSolicitud
INSERT INTO [dbo].[TSOLITEL_TipoSolicitud] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES 
    ('Datos de abonado', 'Descripción de Datos de Abonado', 0),
    ('Datos de IP', 'Descripción de Datos de IP', 0),
    ('Datos IMEI', 'Descripción de Datos IMEI', 0),
	('Datos o conexiones móviles', 'Descripción de Datos o Conexiones Móviles', 0),
    ('Llamadas entrantes y salientes de números fijos', 'Descripción de Llamadas Entrantes y Salientes de Números Fijos', 0),
    ('Llamadas internacionales', 'Descripción de Llamadas Internacionales', 0),
    ('Portabilidad numérica', 'Descripción de Portabilidad Numérica', 0),
    ('Rastreo de celdas', 'Descripción de Rastreo de Celdas', 0),
    ('Recargas Telefónicas (Trazabilidad de las Transacciones)', 'Descripción de Recargas Telefónicas', 0),
    ('Registro de Personas (Ubicación por cédula de Abonados)', 'Descripción de Registro de Personas', 0),
    ('Solicitud de contratos copias y originales', 'Descripción de Solicitud de Contratos', 0),
    ('Reporte de recargas fraudulentas', 'Descripción de Reporte de Recargas Fraudulentas', 0),
    ('Caras de las radio bases', 'Descripción de Caras de las Radio Bases', 0),
    ('Llamadas de Cobro Revertido', 'Descripción de Llamadas de Cobro Revertido', 0),
    ('Fecha de adquisición de la línea', 'Descripción de Fecha de Adquisición de la Línea', 0),
    ('Verificación de cambio de número', 'Descripción de Verificación de Cambio de Número', 0),
    ('Verificación de cambio de abonado', 'Descripción de Verificación de Cambio de Abonado', 0),
    ('Datos SIM', 'Descripción de Datos SIM', 0),
    ('Datos PIN', 'Descripción de Datos PIN', 0),
    ('Datos PUK', 'Descripción de Datos PUK', 0),
    ('Nota Aclaratoria', 'Descripción de Nota Aclaratoria', 0),
    ('Tarjeta 1197', 'Descripción de Tarjeta 1197', 0),
    ('Tarjeta 1199', 'Descripción de Tarjeta 1199', 0),
    ('Radio Localizadores', 'Descripción de Radio Localizadores', 0),
    ('Rastreo de Radio Base', 'Descripción de Rastreo de Radio Base', 0),
    ('Mensaje de Texto (respaldo contenido)', 'Descripción de Mensaje de Texto (Respaldo Contenido)', 0),
    ('Mensaje de Texto (origen y destino)', 'Descripción de Mensaje de Texto (Origen y Destino)', 0),
    ('Llamadas Entrantes y Salientes celulares con radio bases', 'Descripción de Llamadas Entrantes y Salientes Celulares con Radio Bases', 0),
    ('Llamadas Entrantes y Salientes celulares sin radio bases', 'Descripción de Llamadas Entrantes y Salientes Celulares sin Radio Bases', 0),
    ('Cobro Revertido', 'Descripción de Cobro Revertido', 0),
    ('Llamadas salientes celulares con radio bases', 'Descripción de Llamadas Salientes Celulares con Radio Bases', 0),
    ('Llamadas Entrantes celulares con radio bases', 'Descripción de Llamadas Entrantes Celulares con Radio Bases', 0),
    ('Llamadas salientes números fijos', 'Descripción de Llamadas Salientes Números Fijos', 0),
    ('Llamadas entrantes números fijos', 'Descripción de Llamadas Entrantes Números Fijos', 0),
    ('Llamadas Salientes celulares sin radio bases', 'Descripción de Llamadas Salientes Celulares sin Radio Bases', 0),
    ('Llamadas Entrantes celulares sin radio bases', 'Descripción de Llamadas Entrantes Celulares sin Radio Bases', 0),
    ('IMSI', 'Descripción de IMSI', 0),
    ('Recargar telefónicas App Kolbi', 'Descripción de Recargar Telefónicas App Kolbi', 0),
    ('Número de Localización o Medidor', 'Descripción de Número de Localización o Medidor', 0),
    ('Desconexión de un Servicio Telefónico', 'Descripción de Desconexión de un Servicio Telefónico', 0);
GO

-- Paso 11: Insertar en TSOLITEL_Fiscalia
INSERT INTO [dbo].[TSOLITEL_Fiscalia] (TC_Nombre, TB_Borrado)
VALUES 
    ('0033-FISCALIA GENERAL', 0),
    ('0175-FISCALIA ADJUNTA II CIRCUITO JUD. SAN JOSE (PENAL)', 0),
    ('0219-FISCALIA ADJUNTA I CIRC. JUD. ZONA SUR (PENAL)', 0),
    ('0275-FISCALIA DE TURNO EXTRAORDINARIO DE SAN JOSE (PENAL)', 0),
    ('0276-FISCALIA ADJUNTA III CIRCUITO JUDICIAL DE SAN JOSE, SEDE DESAMPARADOS (PENAL)', 0),
    ('0278-FISCALIA DE HATILLO (PENAL)', 0),
    ('0283-FISCALIA DE PURISCAL (PENAL)', 0),
    ('0285-FISCALIA ADJUNTA DE PAVAS (PENAL)', 0),
    ('0306-FISCALIA ADJUNTA I CIRCUITO JUD. ALAJUELA (PENAL)', 0),
    ('0307-FISCALIA ADJUNTA II CIRCUITO JUDICIAL DE ALAJUELA (PENAL)', 0),
    ('0325-FISCALIA DE GRECIA (PENAL)', 0),
    ('0326-FISCALIA DEL III CIRCUITO JUDICIAL ALAJUELA (SAN RAMON) (PENAL)', 0),
    ('0356-FISCALIA ADJUNTA CARTAGO (PENAL)', 0),
    ('0359-FISCALIA DE TURRIALBA (PENAL)', 0),
    ('0382-FISCALIA ADJUNTA HEREDIA (PENAL)', 0),
    ('0385-FISCALIA DE SAN JOAQUIN DE FLORES (PENAL)', 0),
    ('0396-FISCALIA ADJUNTA I CIRC. JUD. GUANACASTE (PENAL)', 0);
GO

-- Paso 12: Insertar en TSOLITEL_CategoriaDelito
INSERT INTO [dbo].[TSOLITEL_CategoriaDelito] ([TC_Nombre], [TC_Descripcion], [TB_Borrado])
VALUES
    ('DELITOS CONTRA LA PROPIEDAD', 'Descripción de Delitos Contra la Propiedad', 0);
GO

-- Paso 13: Insertar en TSOLITEL_Delito
INSERT INTO [dbo].[TSOLITEL_Delito] ([TC_Nombre], [TC_Descripcion], [TB_Borrado], [TN_IdCategoriaDelito])
VALUES
    ('ASALTO', 'Descripción de Asalto', 0, 1),
    ('HURTO', 'Descripción de Hurto', 0, 1),
    ('ROBO', 'Descripción de Robo', 0, 1),
    ('ROBO DE GANADO', 'Descripción de Robo de Ganado', 0, 1),
    ('ROBO DE VEHICULO', 'Descripción de Robo de Vehículo', 0, 1);
GO

-- Paso 14: Insertar en TSOLITEL_SolicitudProveedor
INSERT INTO [dbo].[TSOLITEL_SolicitudProveedor] ([TN_NumeroUnico], [TN_NumeroCaso], [TC_Imputado], [TC_Ofendido], [TC_Resennia], [TB_Urgente], [TB_Aprobado], [TF_FechaDeCrecion], [TN_IdDelito], [TN_IdCategoriaDelito], [TN_IdModalida], [TN_IdSubModalidad], [TN_IdEstado], [TN_IdProveedor], [TN_IdFiscalia], [TN_IdOficina], [TN_IdUsuario])
VALUES
    ('24-000100-1132-pe', NULL, 'PRUEBA', 'PRUEBA', 'PRUEBA', 1, 0, GETDATE(), 1, 1, 1, NULL, 1, 1, 1, 1, 1),
	('24-000101-1133-pe', NULL, 'PRUEBA', 'PRUEBA', 'PRUEBA', 1, 0, GETDATE(), 1, 1, 1, NULL, 1, 1, 1, 1, 1);
GO

-- Paso 15: Insertar en TSOLITEL_RequerimientoProveedor
INSERT INTO [dbo].[TSOLITEL_RequerimientoProveedor] ([TF_FechaDeInicio], [TF_FechaDeFinal], [TC_Requerimiento])
VALUES
    ('2024-07-01 14:31:14', '2024-07-31 14:31:14', 'Pruebas de datos de solitel');
GO

-- Paso 16: Insertar en TSOLITEL_SolicitudProveedor_RequerimientoProveedor
INSERT INTO [dbo].[TSOLITEL_SolicitudProveedor_RequerimientoProveedor] ([TN_IdSolicitud], [TN_IdRequerimientoProveedor])
VALUES
    (1, 1);
GO

-- Paso 17: Insertar en TSOLITEL_TipoSolicitud_RequerimientoProveedor
INSERT INTO [dbo].[TSOLITEL_TipoSolicitud_RequerimientoProveedor] ([TN_IdTipoSolicitud], [TN_IdRequerimientoProveedor])
VALUES
    (1, 1),
    (2, 1),
    (3, 1);
GO

-- Paso 18: Insertar en TSOLITEL_DatoRequerido
INSERT INTO [dbo].[TSOLITEL_DatoRequerido] ([TC_DatoRequerido], [TC_Motivacion], [TN_IdTipoDato])
VALUES
    ('88888888', 'Prueba de datos de SOLITEL', 1),
    ('77777777', 'Prueba de datos de SOLITEL', 1);
GO

-- Paso 19: Insertar en TSOLITEL_Archivo
INSERT INTO [dbo].[TSOLITEL_Archivo] ([TC_Nombre], [TC_FormatoAchivo], [TV_DireccionFileStream], [TF_FechaDeModificacion])
VALUES
    ('Reporte de Análisis', 'PDF', 0x255044462D312E350D0A25E2E3CFD30D0A, GETDATE()),
    ('Registro de Comunicaciones', 'DOCX', 0x504B030414000600080000002100, GETDATE()),
    ('Mapa de Radio Bases', 'JPEG', 0xFFD8FFE000104A4649460001010100600060, GETDATE());
GO

-- Paso 20: Insertar en TSOLITEL_RequerimientoProveedor_Archivo
INSERT INTO [dbo].[TSOLITEL_RequerimientoProveedor_Archivo] ([TN_IdRequerimientoProveedor], [TN_IdArchivo])
VALUES
    (1, 1),
    (1, 2);
GO

USE Proyecto_Analisis;
GO

-- Insertar en TSOLITEL_Rol
INSERT INTO [dbo].[TSOLITEL_Rol] (TC_Nombre, TC_Descripcion)
VALUES 
    ('Admin', 'Administrador del sistema'),
    ('Analista', 'Analista de solicitudes'),
    ('Operador', 'Operador de solicitudes');
GO

-- Insertar en TSOLITEL_Permiso
INSERT INTO [dbo].[TSOLITEL_Permiso] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES 
    ('CrearSolicitud', 'Permiso para crear solicitudes', 0),
    ('VerReporte', 'Permiso para ver reportes', 0),
    ('ModificarSolicitud', 'Permiso para modificar solicitudes', 0);
GO

-- Insertar en TSOLITEL_Rol_Permiso
INSERT INTO [dbo].[TSOLITEL_Rol_Permiso] (TN_IdRol, TN_IdPermiso)
VALUES
    (1, 1),  -- Admin - CrearSolicitud
    (1, 2),  -- Admin - VerReporte
    (1, 3),  -- Admin - ModificarSolicitud
    (2, 2),  -- Analista - VerReporte
    (3, 1);  -- Operador - CrearSolicitud
GO

-- Insertar en TSOLITEL_TipoAnalisis
INSERT INTO [dbo].[TSOLITEL_TipoAnalisis] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES
    ('Tipo A', 'Análisis de tipo A', 0),
    ('Tipo B', 'Análisis de tipo B', 0);
GO

-- Insertar en TSOLITEL_ObjetivoAnalisis
INSERT INTO [dbo].[TSOLITEL_ObjetivoAnalisis] (TC_Nombre, TC_Descripcion, TB_Borrado)
VALUES
    ('Objetivo 1', 'Descripción del objetivo 1', 0),
    ('Objetivo 2', 'Descripción del objetivo 2', 0);
GO

-- Insertar en TSOLITEL_SolicitudAnalisis
INSERT INTO [dbo].[TSOLITEL_SolicitudAnalisis] (TF_FechaDeHecho, TC_OtrosDetalles, TC_OtrosObjetivosDeAnalisis, TF_FechaDeCreacion, TB_Aprobado, TN_IdEstado, TN_IdOficina)
VALUES
    ('2024-07-01 12:00:00', 'Detalles adicionales para análisis 1', 'Objetivos adicionales', GETDATE(), 1, 1, 1),
    ('2024-08-01 15:30:00', 'Detalles adicionales para análisis 2', 'Objetivos adicionales', GETDATE(), 0, 2, 2);
GO

-- Insertar en TSOLITEL_TipoAnalisis_SolicitudAnalisis
INSERT INTO [dbo].[TSOLITEL_TipoAnalisis_SolicitudAnalisis] (TN_IdTipoAnalisis, TN_IdAnalisis)
VALUES
    (1, 1),  -- Tipo A - Análisis 1
    (2, 2);  -- Tipo B - Análisis 2
GO

-- Insertar en TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis
INSERT INTO [dbo].[TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis] (TN_IdObjetivoAnalisis, TN_IdAnalisis)
VALUES
    (1, 1),  -- Objetivo 1 - Análisis 1
    (2, 2);  -- Objetivo 2 - Análisis 2
GO

-- Insertar en TSOLITEL_SolicitudAnalisis_Condicion
INSERT INTO [dbo].[TSOLITEL_SolicitudAnalisis_Condicion] (TN_IdAnalisis, TN_IdCondicion)
VALUES
    (1, 1),  -- Análisis 1 - Condición 1
    (2, 2);  -- Análisis 2 - Condición 2
GO

-- Insertar en TSOLITEL_RequerimientoProveedor_Archivo
INSERT INTO [dbo].[TSOLITEL_RequerimientoProveedor_Archivo] (TN_IdRequerimientoProveedor, TN_IdArchivo)
VALUES
    (1, 1),  -- Requerimiento 1 - Archivo 1
    (1, 2);  -- Requerimiento 1 - Archivo 2
GO

-- Insertar en TSOLITEL_SolicitudAnalisis_Archivo
INSERT INTO [dbo].[TSOLITEL_SolicitudAnalisis_Archivo] (TN_IdAnalisis, TN_IdArchivo, TC_Tipo)
VALUES
    (1, 1, 'Informe'),  -- Análisis 1 - Archivo 1 - Informe
    (2, 2, 'Resumen');  -- Análisis 2 - Archivo 2 - Resumen
GO

-- Insertar en TSOLITEL_Historial
INSERT INTO [dbo].[TSOLITEL_Historial] (TC_Observacion, TF_FechaDeModificacion, TN_IdEstado, TN_IdAnalisis, TN_IdSolicitud, TN_IdUsuario)
VALUES
    ('Observación para análisis 1', GETDATE(), 1, 1, 2, 1),
    ('Observación para análisis 2', GETDATE(), 2, 2, 3, 1);
GO

-- Insertar en TSOLITEL_Logger
INSERT INTO [dbo].[TSOLITEL_Logger] (TC_Accion, TC_Descripcion, TF_FechaDeEvento)
VALUES
    ('Crear Análisis', 'Se ha creado un análisis', GETDATE()),
    ('Modificar Solicitud', 'Se ha modificado una solicitud', GETDATE());
GO
