USE Proyecto_Analisis
GO

SELECT TOP 1 * FROM TSOLITEL_Usuario

SELECT TOP 1 * FROM TSOLITEL_Delito
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_CategoriaDelito
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_Modalidad
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_SubModalidad
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_Estado
SELECT TOP 1 * FROM TSOLITEL_Proveedor
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_Fiscalia
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_Oficina
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_TipoSolicitud
WHERE TB_Borrado = 0
SELECT TOP 1 * FROM TSOLITEL_TipoDato
WHERE TB_Borrado = 0

INSERT INTO TSOLITEL_Usuario (TC_Nombre, TC_Apellido, TC_Usuario, TC_Contrasenna, TC_CorreoElectronico)
VALUES ('Harvey', 'Mendez', 'HarveyUsuario', 'HarveyContrasenna', 'harvey.mendez@ucr.ac.cr')

INSERT INTO TSOLITEL_Estado (TC_Nombre, TC_Descripcion)
VALUES ('Estado1', 'Descripcion de Estado1')

INSERT INTO TSOLITEL_Proveedor (TC_Nombre)
VALUES ('Proveedor2')

INSERT INTO TSOLITEL_Fiscalia (TC_Nombre)
VALUES ('Fiscalia1')

INSERT INTO TSOLITEL_Oficina (TC_Nombre)
VALUES ('Oficina1')



SELECT * FROM TSOLITEL_SolicitudProveedor
SELECT * FROM TSOLITEL_RequerimientoProveedor
SELECT * FROM TSOLITEL_TipoSolicitud_RequerimientoProveedor
SELECT * FROM TSOLITEL_DatoRequerido
SELECT * FROM TSOLITEL_RequerimientoProveedor_DatoRequerido


DELETE FROM TSOLITEL_TipoSolicitud_RequerimientoProveedor
DELETE FROM TSOLITEL_RequerimientoProveedor_DatoRequerido
DELETE FROM TSOLITEL_DatoRequerido
DELETE FROM TSOLITEL_RequerimientoProveedor
DELETE FROM TSOLITEL_SolicitudProveedor















SELECT CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE TABLE_NAME = 'TSOLITEL_SolicitudProveedor'
AND CONSTRAINT_TYPE = 'UNIQUE';

ALTER TABLE TSOLITEL_SolicitudProveedor
DROP CONSTRAINT UQ__TSOLITEL__F8E485BC3093468D;


