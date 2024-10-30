USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:                Jesner Melgara
-- Create date:           16-10-2024
-- Description:           Procedimiento almacenado para insertar una solicitud de an�lisis
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_InsertarSolicitudAnalisis
    @TF_FechaDelHecho DATE,
    @TC_OtrosDetalles VARCHAR(255),
    @TC_OtrosObjetivosDeAnalisis VARCHAR(255) = NULL,
    @TB_Aprobado BIT,
    @TF_FechaCrecion DATE = NULL,
    @TN_NumeroSolicitud INT,
    @TN_IdOficina INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis 
        (
            TF_FechaDeHecho, 
            TC_OtrosDetalles, 
            TC_OtrosObjetivosDeAnalisis, 
            TB_Aprobado, 
            TF_FechaDeCreacion, 
            TN_Solicitud, 
            TN_IdOficina
        )
        VALUES 
        (
            @TF_FechaDelHecho, 
            @TC_OtrosDetalles, 
            @TC_OtrosObjetivosDeAnalisis, 
            @TB_Aprobado, 
            @TF_FechaCrecion, 
            @TN_NumeroSolicitud, 
            @TN_IdOficina
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
-- Description:           Procedimiento almacenado para insertar en la tabla intermedia de ObjetivoAnalisis_SolicitudAnalisis
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_ObjetivoAnalisis_SolicitudAnalisis
    @TN_IdObjetivo INT,
    @TN_IdAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

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
            TN_IdTipo, 
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
    @pIdArchivo INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis_Archivo (IdAnalisis, IdArchivo)
        VALUES (@pIdAnalisis, @pIdArchivo);

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

        INSERT INTO dbo.TipoAnalisis_SolicitudAnalisis (IdTipoAnalisis, IdAnalisis)
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creaci�n:    16-10-2024
-- Descripci�n:          Procedimiento almacenado para insertar un objetivo de an�lisis en TSOLITEL_ObjetivoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarObjetivoAnalisis
    @pTN_IdObjetivoAnalisis INT OUTPUT, -- Par�metro de salida para el ID generado
    @pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255),
    @pTB_Borrado BIT = 0 -- Valor por defecto de 0 si no se proporciona
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacci�n
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

        -- Obtener el �ltimo ID insertado
        SET @pTN_IdObjetivoAnalisis = SCOPE_IDENTITY();

        -- Confirma la transacci�n si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacci�n
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
-- Fecha de creaci�n:    2024-10-16
-- Descripci�n:          Consulta los registros de la tabla TSOLITEL_ObjetivoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarObjetivoAnalisis 
    @pTN_IdObjetivoAnalisis INT = NULL  -- Par�metro opcional para filtrar por Id
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
        WHERE (@pTN_IdObjetivoAnalisis IS NULL OR TN_IdObjetivoAnalisis = @pTN_IdObjetivoAnalisis)
          AND TB_Borrado = 0  -- Solo mostrar registros que no est�n marcados como borrados
        ORDER BY TN_IdObjetivoAnalisis ASC;
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
-- Fecha de creaci�n:    2024-10-16
-- Descripci�n:          Elimina l�gicamente un registro de la tabla TSOLITEL_ObjetivoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarObjetivoAnalisis
    @pTN_IdObjetivoAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacci�n
        BEGIN TRANSACTION;

        -- Realiza la eliminaci�n l�gica cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_ObjetivoAnalisis
        SET TB_Borrado = 1
        WHERE TN_IdObjetivoAnalisis = @pTN_IdObjetivoAnalisis;

        -- Verificar si el registro existe
        IF @@ROWCOUNT = 0
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR('No se encontr� ning�n registro con el Id especificado.', 16, 1);
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
