USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:                Jesner Melgara
-- Create date:           16-10-2024
-- Description:           Procedimiento almacenado para insertar una solicitud de análisis
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarSolicitudAnalisis
    @TF_FechaDelHecho DATE,
    @TC_OtrosDetalles VARCHAR(255),
    @TC_OtrosObjetivosDeAnalisis VARCHAR(255) = NULL,
    @TB_Aprobado BIT,
    @TF_FechaCrecion DATE = NULL,
    @TN_NumeroSolicitud INT,
    @TN_IdOficina INT,
    @TN_IdSolicitudAnalisis INT OUTPUT -- Agregar parámetro de salida
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Inserción principal
        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis 
        (
            TF_FechaDeHecho, 
            TC_OtrosDetalles, 
            TC_OtrosObjetivosDeAnalisis, 
            TB_Aprobado, 
            TF_FechaDeCreacion, 
			TN_IdEstado,
            TN_IdOficina
        )
        VALUES 
        (
            @TF_FechaDelHecho, 
            @TC_OtrosDetalles, 
            @TC_OtrosObjetivosDeAnalisis, 
            @TB_Aprobado, 
            @TF_FechaCrecion,
			4,
            @TN_IdOficina
        );

        -- Captura el ID generado
        SET @TN_IdSolicitudAnalisis = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- =============================================
-- Author:                Jesner Melgara
-- Create date:           16-10-2024
-- Description:           Procedimiento almacenado para insertar en la tabla intermedia de ObjetivoAnalisis_SolicitudAnalisis
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarObjetivoAnalisis_SolicitudAnalisis
    @TN_IdObjetivo INT OUTPUT,
    @TN_IdAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insertar en la tabla intermedia y capturar el ID del objetivo
        INSERT INTO dbo.TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis 
        (
            TN_IdObjetivoAnalisis, 
            TN_IdAnalisis
        )
        VALUES 
        (
            @TN_IdObjetivo, 
            @TN_IdAnalisis
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO


-- =============================================
-- Author:                Jesner Melgara
-- Create date:           16-10-2024
-- Description:           Procedimiento almacenado para insertar en la tabla RequerimientoAnalisis
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarRequerimentoAnalisis
    @TC_Objetivo VARCHAR(255),
    @TC_UtilizadoPor VARCHAR(255),
    @TN_IdTipo INT,
    @TN_IdAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.TSOLITEL_RequerimentoAnalisis 
        (
            TC_Objetivo, 
            TC_UtilizadoPor, 
            TN_IdTipoDato, 
            TN_IdAnalisis
        )
        VALUES 
        (
            @TC_Objetivo, 
            @TC_UtilizadoPor, 
            @TN_IdTipo, 
            @TN_IdAnalisis
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- =============================================
-- Author:                Jesner Melgara Murillo
-- Create date:           2024-10-20
-- Description:           Insertar en la tabla intermedia SolicitudAnalisis_Archivo
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarSolicitudAnalisis_Archivo
    @pIdAnalisis INT,
    @pIdArchivo INT,
    @pTipo NVARCHAR(50) -- Agregar el nuevo parámetro
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Inserción con la columna TC_Tipo
        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis_Archivo (TN_IdAnalisis, TN_IdArchivo, TC_Tipo)
        VALUES (@pIdAnalisis, @pIdArchivo, @pTipo);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1;
    END CATCH
END
GO

-- =============================================
-- Author:                Jesner Melgara Murillo
-- Create date:           2024-10-20
-- Description:           Insertar en la tabla intermedia TipoAnalisis_SolicitudAnalisis
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarTipoAnalisis_SolicitudAnalisis
    @pIdTipoAnalisis INT,
    @pIdAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.TSOLITEL_TipoAnalisis_SolicitudAnalisis(TN_IdTipoAnalisis, TN_IdAnalisis)
        VALUES (@pIdTipoAnalisis, @pIdAnalisis);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1;
    END CATCH
END
GO
-- =============================================
-- Author:                Jesner Melgara Murillo
-- Create date:           2024-10-20
-- Description:           Insertar en la tabla intermedia PA_InsertarSolicitudAnalisis_SolicitudProveedor
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarSolicitudAnalisis_SolicitudProveedor
    @TN_IdAnalisis INT,
    @TN_IdSolicitud INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar que TN_IdAnalisis existe en TSOLITEL_SolicitudAnalisis
        IF NOT EXISTS (SELECT 1 FROM dbo.TSOLITEL_SolicitudAnalisis WHERE TN_IdAnalisis = @TN_IdAnalisis)
        BEGIN
            RAISERROR ('El TN_IdAnalisis proporcionado no existe en la tabla TSOLITEL_SolicitudAnalisis.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Verificar que TN_IdSolicitud existe en TSOLITEL_SolicitudProveedor
        IF NOT EXISTS (SELECT 1 FROM dbo.TSOLITEL_SolicitudProveedor WHERE TN_IdSolicitud = @TN_IdSolicitud)
        BEGIN
            RAISERROR ('El TN_IdSolicitud proporcionado no existe en la tabla TSOLITEL_SolicitudProveedor.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insertar los valores en TSOLITEL_SolicitudAnalisis_SolicitudProveedor
        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis_SolicitudProveedor (TN_IdAnalisis, TN_IdSolicitud)
        VALUES (@TN_IdAnalisis, @TN_IdSolicitud);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanzar error para manejo en el cliente
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    16-10-2024
-- Descripción:          Procedimiento almacenado para insertar un objetivo de análisis en TSOLITEL_ObjetivoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarObjetivoAnalisis
    @pTN_IdObjetivoAnalisis INT OUTPUT, -- Parámetro de salida para el ID generado
    @pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255),
    @pTB_Borrado BIT = 0 -- Valor por defecto de 0 si no se proporciona
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Inserta los valores en la tabla TSOLITEL_ObjetivoAnalisis y obtiene el ID generado
        INSERT INTO dbo.TSOLITEL_ObjetivoAnalisis 
        (
            TC_Nombre, 
            TC_Descripcion, 
            TB_Borrado
        )
        VALUES 
        (
            @pTC_Nombre, 
            @pTC_Descripcion, 
            @pTB_Borrado
        );

        -- Obtener el último ID insertado
        SET @pTN_IdObjetivoAnalisis = SCOPE_IDENTITY();

        -- Confirma la transacción si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Maneja el error mostrando detalles
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanza el error para que sea manejado fuera del procedimiento si es necesario
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:                Jesner Melgara Murillo
-- Fecha de creación:    2024-10-16
-- Descripción:          Consulta los registros de la tabla TSOLITEL_ObjetivoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarObjetivoAnalisis 
    @pTN_IdObjetivoAnalisis INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Consulta la tabla TSOLITEL_ObjetivoAnalisis
        SELECT 
            TN_IdObjetivoAnalisis,
            TC_Nombre,
            TC_Descripcion,
            TB_Borrado
        FROM dbo.TSOLITEL_ObjetivoAnalisis WITH (NOLOCK)
        WHERE (@pTN_IdObjetivoAnalisis = 0 OR TN_IdObjetivoAnalisis = @pTN_IdObjetivoAnalisis)
          AND TB_Borrado = 0  -- Solo mostrar registros que no están marcados como borrados
        ORDER BY TN_IdObjetivoAnalisis DESC;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1;
    END CATCH
END
GO

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Elimina lógicamente un registro de la tabla TSOLITEL_ObjetivoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarObjetivoAnalisis
    @pTN_IdObjetivoAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Realiza la eliminación lógica cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_ObjetivoAnalisis
        SET TB_Borrado = 1
        WHERE TN_IdObjetivoAnalisis = @pTN_IdObjetivoAnalisis;

        -- Verificar si el registro existe
        IF @@ROWCOUNT = 0
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('No se encontró ningún registro con el Id especificado.', 16, 1);
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, hacer rollback
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanza el error
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Obtiene todos los archivos relacionados al ID de la solicitud del proveedor
-- =============================================

--REVISION ESTE SP, NO ESTA CARGADO
CREATE OR ALTER PROCEDURE dbo.SP_ConsultarArchivosPorSolicitudProveedor
    @idSolicitudProveedor INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        a.TN_IdArchivo,
        a.TC_Nombre,
        a.TC_FormatoAchivo,
        a.TV_DireccionFileStream AS TV_Contenido,  -- Renombrado para que coincida con el modelo
        a.TF_FechaDeModificacion AS TF_FechaModificacion
    FROM 
        dbo.TSOLITEL_Archivo a
    INNER JOIN 
        dbo.TSOLITEL_SolicitudAnalisis_SolicitudProveedor sasp 
    ON 
        a.TN_IdArchivo = sasp.TN_IdSolicitud  -- Ajusta esta relación según la FK real
    WHERE 
        sasp.TN_IdSolicitud = @idSolicitudProveedor;
END
GO
USE [Proyecto_Analisis]
GO
-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Insertar datos en la tabla intermedia PA_InsertarSolicitudAnalisis_Condicion
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarSolicitudAnalisis_Condicion
    @TN_IdAnalisis INT,
    @TN_IdCondicion INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si el TN_IdAnalisis existe en TSOLITEL_SolicitudAnalisis
        IF NOT EXISTS (SELECT 1 FROM dbo.TSOLITEL_SolicitudAnalisis WHERE TN_IdAnalisis = @TN_IdAnalisis)
        BEGIN
            RAISERROR ('El ID de Análisis proporcionado no existe en TSOLITEL_SolicitudAnalisis.', 16, 1);
            RETURN;
        END

        -- Verificar si el TN_IdCondicion existe en TSOLITEL_Condicion
        IF NOT EXISTS (SELECT 1 FROM dbo.TSOLITEL_Condicion WHERE TN_IdCondicion = @TN_IdCondicion)
        BEGIN
            RAISERROR ('El ID de Condición proporcionado no existe en TSOLITEL_Condicion.', 16, 1);
            RETURN;
        END

        -- Insertar en la tabla TSOLITEL_SolicitudAnalisis_Condicion
        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis_Condicion (TN_IdAnalisis, TN_IdCondicion)
        VALUES (@TN_IdAnalisis, @TN_IdCondicion);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshacer la transacción
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Manejar el error
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanzar el error para que sea manejado por la aplicación
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- Tabla principal de Solicitudes de Análisis
SELECT * FROM dbo.TSOLITEL_SolicitudAnalisis;

-- Archivos asociados a cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_SolicitudAnalisis_Archivo;

-- Condiciones relacionadas con cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_SolicitudAnalisis_Condicion;

-- Objetivos de análisis asociados a cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis;

-- Tipos de análisis asociados a cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_TipoAnalisis_SolicitudAnalisis;

-- Requerimientos de análisis relacionados con cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_RequerimentoAnalisis;

-- Proveedores asociados a cada solicitud de análisis (si aplica en contexto)
SELECT * FROM dbo.TSOLITEL_SolicitudAnalisis_SolicitudProveedor;






--Eliminar Datos
-- Eliminar proveedores asociados a cada solicitud de análisis
DELETE FROM dbo.TSOLITEL_SolicitudAnalisis_SolicitudProveedor;

-- Eliminar requerimientos de análisis relacionados con cada solicitud de análisis
DELETE FROM dbo.TSOLITEL_RequerimentoAnalisis;

-- Eliminar tipos de análisis asociados a cada solicitud de análisis
DELETE FROM dbo.TSOLITEL_TipoAnalisis_SolicitudAnalisis WHERE TN_IdAnalisis > 2;

-- Eliminar objetivos de análisis asociados a cada solicitud de análisis
DELETE FROM dbo.TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis;

-- Eliminar condiciones relacionadas con cada solicitud de análisis
DELETE FROM dbo.TSOLITEL_SolicitudAnalisis_Condicion;

-- Eliminar archivos asociados a cada solicitud de análisis
DELETE FROM dbo.TSOLITEL_SolicitudAnalisis_Archivo WHERE TN_IdAnalisis > 2;;

-- Eliminar las solicitudes de análisis
DELETE FROM dbo.TSOLITEL_SolicitudAnalisis WHERE TN_IdAnalisis > 2;;
