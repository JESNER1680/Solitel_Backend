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
    @PN_IdUsuarioCreador INT,
    @TF_FechaDelHecho DATE,
    @TC_OtrosDetalles VARCHAR(255),
    @TC_OtrosObjetivosDeAnalisis VARCHAR(255) = NULL,
    @TB_Aprobado BIT,
    @TF_FechaCrecion DATE = NULL,
    @TN_NumeroSolicitud INT,
    @TN_IdOficina INT,
    @TN_IdSolicitudAnalisis INT OUTPUT -- Parámetro de salida
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Iniciar la transacción
        BEGIN TRANSACTION;

        DECLARE @ResultadoIdEstado INT;

        -- Inserción principal
        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis 
        (
            TF_FechaDeHecho, 
            TC_OtrosDetalles, 
            TC_OtrosObjetivosDeAnalisis, 
            TB_Aprobado, 
            TF_FechaDeCreacion, 
            TN_IdOficina
        )
        VALUES 
        (
            @TF_FechaDelHecho, 
            @TC_OtrosDetalles, 
            @TC_OtrosObjetivosDeAnalisis, 
            @TB_Aprobado, 
            @TF_FechaCrecion,
            @TN_IdOficina
        );

        -- Capturar el ID generado
        SET @TN_IdSolicitudAnalisis = SCOPE_IDENTITY();

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        -- Cambiar el estado de la solicitud
        EXEC PA_CambiarEstadoSolicitudAnalisis @TN_IdSolicitudAnalisis, 'Aprobar Análisis', 'Analisis', @TN_IdEstado = @ResultadoIdEstado OUTPUT;

        -- Insertar en el historial de la solicitud
        EXEC PA_InsertarHistoricoSolicitud NULL, @TN_IdSolicitudAnalisis, @PN_IdUsuarioCreador, '', @ResultadoIdEstado;

    END TRY
    BEGIN CATCH
        -- Si ocurre un error, revertir la transacción
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
    @TN_IdAnalisis INT,
    @TN_IdRequerimiento INT OUTPUT
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

        SET @TN_IdRequerimiento = SCOPE_IDENTITY();

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
CREATE OR ALTER PROCEDURE dbo.PA_InsertarRequerimientoAnalisis_Condicion
    @TN_IdRequerimiento INT,
    @TN_IdCondicion INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verificar si el TN_IdAnalisis existe en TSOLITEL_SolicitudAnalisis
        IF NOT EXISTS (SELECT 1 FROM dbo.TSOLITEL_RequerimentoAnalisis WHERE TN_IdRequerimientoAnalisis = @TN_IdRequerimiento)
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
        INSERT INTO dbo.[TSOLITEL_RequerimientoAnalisis_Condicion] (TN_IdRequerimientoAnalisis, TN_IdCondicion)
        VALUES (@TN_IdRequerimiento, @TN_IdCondicion);

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

-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Consultar todas solicitudes de analisis
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].PA_ObtenerSolicitudesAnalisis
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            TN_IdAnalisis,
			TN_IdAnalisis AS TN_NumeroSolicitud,
            TF_FechaDeHecho,
            TC_OtrosDetalles,
            TC_OtrosObjetivosDeAnalisis,
            TF_FechaDeCreacion,
            TB_Aprobado,
            ES.TN_IdEstado,
			ES.TC_Nombre,
            TN_IdOficina
        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_SolicitudAnalisis] AS SOLI
			JOIN TSOLITEL_Estado AS ES ON ES.TN_IdEstado = SOLI.TN_IdEstado
        ORDER BY 
            TN_IdAnalisis DESC;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Consultar todos los querimientos de analisis por Id
-- =============================================

USE PROYECTO_ANALISIS
GO
CREATE OR ALTER PROCEDURE [dbo].[PA_ObtenerRequerimientosPorSolicitudAnalisis]
    @TN_IdAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            REQ.TN_IdRequerimientoAnalisis,
            REQ.TC_Objetivo,
            REQ.TC_UtilizadoPor,
            REQ.TN_IdAnalisis,
            REQ.TN_IdTipoDato AS TN_IdTipo,
            CON.TC_Nombre,
            CON.TN_IdCondicion,
			TIP.TC_Nombre AS TC_NombreTipoDato
        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_RequerimentoAnalisis] AS REQ
		JOIN 
		TSOLITEL_TipoDato AS TIP ON TIP.TN_IdTipoDato = REQ.TN_IdTipoDato
        JOIN  
            [Proyecto_Analisis].[dbo].[TSOLITEL_RequerimientoAnalisis_Condicion] AS SAC
        ON 
            REQ.TN_IdRequerimientoAnalisis = SAC.TN_IdRequerimientoAnalisis
        JOIN 
            [Proyecto_Analisis].[dbo].[TSOLITEL_Condicion] AS CON 
        ON 
            CON.TN_IdCondicion = SAC.TN_IdCondicion
        WHERE 
            REQ.TN_IdAnalisis = @TN_IdAnalisis
        ORDER BY 
            REQ.TN_IdRequerimientoAnalisis ASC;

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
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Consultar todos los Objetivos de analisis por Id
-- =============================================

CREATE OR ALTER PROCEDURE [dbo].[PA_ObtenerObjetivosPorSolicitudAnalisis]
    @TN_IdAnalisis INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            OA.TN_IdObjetivoAnalisis,
            OA.TC_Nombre,
            OA.TC_Descripcion
        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_ObjetivoAnalisis] OA
        INNER JOIN 
            [Proyecto_Analisis].[dbo].[TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis] OASA
        ON 
            OA.TN_IdObjetivoAnalisis = OASA.TN_IdObjetivoAnalisis
        WHERE 
            OASA.TN_IdAnalisis = @TN_IdAnalisis
        ORDER BY 
            OA.TN_IdObjetivoAnalisis ASC;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Consultar todos los Tipos de analisis por Id
-- =============================================
CREATE PROCEDURE [dbo].[PA_ObtenerTiposAnalisisPorSolicitud]
    @TN_IdAnalisis INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            TA.TN_IdTipoAnalisis,
            TA.TC_Nombre,
            TA.TC_Descripcion
        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_TipoAnalisis] TA
        INNER JOIN 
            [Proyecto_Analisis].[dbo].[TSOLITEL_TipoAnalisis_SolicitudAnalisis] TASA
        ON 
            TA.TN_IdTipoAnalisis = TASA.TN_IdTipoAnalisis
        WHERE 
            TASA.TN_IdAnalisis = @TN_IdAnalisis
        ORDER BY 
            TA.TN_IdTipoAnalisis ASC;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Consultar todos las Condiciones de analisis por Id
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[PA_ObtenerCondicionesPorRequerimientoAnalisis]
    @TN_IdRequerimientoAnalisis INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            C.TN_IdCondicion,
            C.TC_Nombre,
            C.TC_Descripcion
        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_Condicion] C
        INNER JOIN 
            [Proyecto_Analisis].[dbo].[TSOLITEL_RequerimientoAnalisis_Condicion] SAC
        ON 
            C.TN_IdCondicion = SAC.TN_IdCondicion
        WHERE 
            SAC.TN_IdRequerimientoAnalisis = @TN_IdRequerimientoAnalisis
        ORDER BY 
            C.TN_IdCondicion ASC;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Consultar todos las Archivos de analisis por Id
-- =============================================

CREATE OR ALTER PROCEDURE [dbo].[PA_ObtenerArchivosPorSolicitudAnalisis]
    @TN_IdAnalisis INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            A.TN_IdArchivo,
            A.TC_Nombre,
            A.TC_FormatoAchivo AS TC_FormatoArchivo,
            A.TV_DireccionFileStream AS TV_Contenido,
            A.TF_FechaDeModificacion AS TF_FechaModificacion
        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_Archivo] A
        INNER JOIN 
            [Proyecto_Analisis].[dbo].[TSOLITEL_SolicitudAnalisis_Archivo] SAA
        ON 
            A.TN_IdArchivo = SAA.TN_IdArchivo
        WHERE 
            SAA.TN_IdAnalisis = @TN_IdAnalisis AND SAA.TC_TIPO = 'Respuesta'
        ORDER BY 
            A.TN_IdArchivo ASC;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE [dbo].[PA_ConsultarSoliciProveSoliciAnalisis]
    @pTN_IdSolicitud INT = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta las solicitudes del proveedor basadas en el ID de la solicitud de análisis
        SELECT 
            T.TN_IdSolicitud,
            T.TN_NumeroUnico,
            T.TN_NumeroCaso,
            T.TC_Imputado,
            T.TC_Ofendido,
            T.TC_Resennia,
            T.TB_Urgente,
            T.TB_Aprobado,
            T.TF_FechaDeCrecion AS TF_FechaDeCreacion,
            Usuario.TN_IdUsuario,
            CONCAT(Usuario.TC_Nombre, ' ', Usuario.TC_Apellido) AS TC_NombreUsuario,
            Proveedor.TN_IdProveedor,
            Proveedor.TC_Nombre AS TC_NombreProveedor,
            Fiscalia.TN_IdFiscalia,
            Fiscalia.TC_Nombre AS TC_NombreFiscalia,
            Delito.TN_IdDelito AS TN_IdDelito,
            Delito.TC_Nombre AS TC_NombreDelito,
            CategoriaDelito.TN_IdCategoriaDelito,
            CategoriaDelito.TC_Nombre AS TC_NombreCategoriaDelito,
            Modalidad.TN_IdModalidad AS TN_IdModalidad,
            Modalidad.TC_Nombre AS TC_NombreModalidad,
            Estado.TN_IdEstado,
            Estado.TC_Nombre AS TC_NombreEstado,
            SubModalidad.TN_IdSubModalidad,
            SubModalidad.TC_Nombre AS TC_NombreSubModalidad,
            T.TN_IdSolicitud AS TN_NumeroSolicitud
            
        FROM TSOLITEL_SolicitudProveedor AS T
        INNER JOIN TSOLITEL_Proveedor AS Proveedor ON T.TN_IdProveedor = Proveedor.TN_IdProveedor
        INNER JOIN TSOLITEL_Fiscalia AS Fiscalia ON T.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        INNER JOIN TSOLITEL_Delito AS Delito ON T.TN_IdDelito = Delito.TN_IdDelito
        INNER JOIN TSOLITEL_CategoriaDelito AS CategoriaDelito ON T.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        INNER JOIN TSOLITEL_Modalidad AS Modalidad ON T.TN_IdModalida = Modalidad.TN_IdModalidad
        INNER JOIN TSOLITEL_Estado AS Estado ON T.TN_IdEstado = Estado.TN_IdEstado
        INNER JOIN TSOLITEL_SubModalidad AS SubModalidad ON T.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
        INNER JOIN TSOLITEL_Usuario AS Usuario ON T.TN_IdUsuario = Usuario.TN_IdUsuario
        INNER JOIN TSOLITEL_SolicitudAnalisis_SolicitudProveedor AS SA ON T.TN_IdSolicitud = SA.TN_IdSolicitud
        WHERE (@pTN_IdSolicitud IS NULL OR SA.TN_IdAnalisis = @pTN_IdSolicitud)
        ORDER BY T.TN_IdSolicitud DESC;

        -- Confirma la transacción si no hay errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Captura y lanza el error
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE PA_AprobarSolicitudAnalisis
	@pTN_IdSolicitud INT,
	@PN_IdUsuario INT,
	@PC_Observacion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
	DECLARE @Error INT;

	BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @IdEstado int;

		UPDATE [dbo].[TSOLITEL_SolicitudAnalisis]
            SET TB_Aprobado = 1
            WHERE TN_IdAnalisis = @pTN_IdSolicitud;

		-- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0 OR @@ROWCOUNT = 0
        BEGIN
            ROLLBACK TRANSACTION;
            IF @@ROWCOUNT = 0
            BEGIN
                RAISERROR('No se encontró ningún registro con el Id especificado.', 16, 1);
            END
            RETURN -1;
        END

		COMMIT TRANSACTION;

		EXEC PA_CambiarEstadoSolicitudAnalisis @pTN_IdSolicitud, 'En Análisis', 'Analisis', @TN_IdEstado = @IdEstado OUTPUT;

		EXEC [PA_InsertarHistoricoSolicitud] NULL, @pTN_IdSolicitud, @PN_IdUsuario, @PC_Observacion, @IdEstado;

    END TRY
    BEGIN CATCH

        -- En caso de error, hacer rollback
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Lanzar el error de SQL Server
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


-- Tabla principal de Solicitudes de Análisis
SELECT * FROM dbo.TSOLITEL_SolicitudAnalisis ORDER BY TN_IdAnalisis ASC;

-- Archivos asociados a cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_SolicitudAnalisis_Archivo ORDER BY TN_IdAnalisis ASC ;

-- Condiciones relacionadas con cada solicitud de análisis
SELECT * FROM [TSOLITEL_RequerimientoAnalisis_Condicion];

-- Objetivos de análisis asociados a cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis ORDER BY TN_IdAnalisis;

-- Tipos de análisis asociados a cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_TipoAnalisis_SolicitudAnalisis ORDER BY TN_IdAnalisis;

-- Requerimientos de análisis relacionados con cada solicitud de análisis
SELECT * FROM dbo.TSOLITEL_RequerimentoAnalisis ORDER BY TN_IdAnalisis;

-- Proveedores asociados a cada solicitud de análisis (si aplica en contexto)
SELECT * FROM dbo.TSOLITEL_SolicitudAnalisis_SolicitudProveedor;

SELECT * FROM TSOLITEL_Estado

UPDATE TSOLITEL_SolicitudAnalisis SET TN_idEstado = 12 WHERE TN_IdAnalisis = 47 
UPDATE TSOLITEL_SolicitudAnalisis SET TB_Aprobado = 0 WHERE TN_IdAnalisis = 46 

SELECT * FROM TSOLITEL_Estado


----Eliminar Datos
---- Eliminar proveedores asociados a cada solicitud de análisis
--DELETE FROM dbo.TSOLITEL_SolicitudAnalisis_SolicitudProveedor;

---- Eliminar requerimientos de análisis relacionados con cada solicitud de análisis
--DELETE FROM dbo.TSOLITEL_RequerimentoAnalisis;

---- Eliminar tipos de análisis asociados a cada solicitud de análisis
--DELETE FROM dbo.TSOLITEL_TipoAnalisis_SolicitudAnalisis WHERE TN_IdAnalisis > 2;

---- Eliminar objetivos de análisis asociados a cada solicitud de análisis
--DELETE FROM dbo.TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis;

---- Eliminar condiciones relacionadas con cada solicitud de análisis
--DELETE FROM dbo.[TSOLITEL_RequerimientoAnalisis_Condicion];

---- Eliminar archivos asociados a cada solicitud de análisis
--DELETE FROM dbo.TSOLITEL_SolicitudAnalisis_Archivo WHERE TN_IdAnalisis > 2;;

---- Eliminar las solicitudes de análisis
--DELETE FROM dbo.TSOLITEL_SolicitudAnalisis WHERE TN_IdAnalisis > 2;;
