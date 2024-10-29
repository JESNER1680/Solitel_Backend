
-- =============================================
-- Author:		Jesner Melgara
-- Create date: 16-10-2024
-- Description:	Procedimiento almacenado para insertar una solicitud de analisis
-- =============================================
CREATE OR ALTER PROCEDURE PA_InsertarSolicitudAnalisis
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
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Inserta los valores en la tabla TSOLITEL_SolicitudAnalisis
        INSERT INTO [dbo].[TSOLITEL_SolicitudAnalisis] 
        (
            TF_FechaDelHecho, 
            TC_OtrosDetalles, 
            TC_OtrosObjetivosDeAnalisis, 
            TB_Aprobado, 
            TF_FechaCrecion, 
            TN_NumeroSolicitud, 
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

        -- Si todo va bien, confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
-- =============================================
-- Author:		Jesner Melgara
-- Create date: 16-10-2024
-- Description:	Procedimiento almacenado para insertar una objetivo de analisis
-- =============================================

CREATE OR ALTER PROCEDURE PA_InsertarObjetivoAnalisis
    @TC_Nombre VARCHAR(50),
    @TC_Descripcion VARCHAR(255),
    @TB_Borrado BIT = 0 -- Valor por defecto de 0 si no se proporciona
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Inserta los valores en la tabla TSOLITEL_ObjetivoAnalisis
        INSERT INTO [dbo].[TSOLITEL_ObjetivoAnalisis] 
        (
            TC_Nombre, 
            TC_Descripcion, 
            TB_Borrado
        )
        VALUES 
        (
            @TC_Nombre, 
            @TC_Descripcion, 
            @TB_Borrado
        );

        -- Si todo va bien, confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
-- =============================================
-- Author:		Jesner Melgara
-- Create date: 16-10-2024
-- Description:	Procedimiento almacenado para insertar en la tabla intermedia de ObjetivoAnalisis_SolicitudAnalisis
-- =============================================
CREATE OR ALTER PROCEDURE PA_ObjetivoAnalisis_SolicitudAnalisis
    @TN_IdObjetivo INT,
    @TN_IdAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Inserta los valores en la tabla TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis
        INSERT INTO [dbo].[TSOLITEL_ObjetivoAnalisis_SolicitudAnalisis] 
        (
            TN_IdObjetivo, 
            TN_IdAnalisis
        )
        VALUES 
        (
            @TN_IdObjetivo, 
            @TN_IdAnalisis
        );

        -- Si todo va bien, confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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

-- =============================================
-- Author:		Jesner Melgara
-- Create date: 16-10-2024
-- Description:	Procedimiento almacenado para insertar a la tabla RequerimientoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE PA_InsertarRequerimentoAnalisis
    @TC_Objetivo VARCHAR(255),
    @TC_UtilizadoPor VARCHAR(255),
    @TN_IdTipo INT,
    @TN_IdAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Inserta los valores en la tabla TSOLITEL_RequerimentoAnalisis
        INSERT INTO [dbo].[TSOLITEL_RequerimentoAnalisis] 
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

        -- Si todo va bien, confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
-- =============================================
-- Autor:                Jesner Melgara Murillo
-- Fecha de creación:     2024-10-16
-- Descripción:            Consulta los registros de la tabla [TSOLITEL_ObjetivoAnalisis]
-- =============================================
CREATE OR ALTER PROCEDURE PA_ObtenerObjetivoAnalisis 
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
        WHERE (@pTN_IdObjetivoAnalisis IS NULL OR TN_IdObjetivoAnalisis = @pTN_IdObjetivoAnalisis)
          AND TB_Borrado = 0  -- Solo mostrar registros que no están marcados como borrados
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
-- =============================================
-- Autor:                Jesner Melgara Murillo
-- Fecha de creación:     2024-10-20
-- Descripción:            Insertar datos en la tabla intermedia SolicitudAnalisis_Archivo
-- =============================================
CREATE OR ALTER PROCEDURE PA_InsertarSolicitudAnalisis_Archivo
    @pIdAnalisis INT,     -- ID de la solicitud de análisis (requerido)
    @pIdArchivo INT       -- ID del archivo (requerido)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Inserta en la tabla intermedia TSOLITEL_SolicitudAnalisis_Archivo
        INSERT INTO dbo.TSOLITEL_SolicitudAnalisis_Archivo (IdAnalisis, IdArchivo)
        VALUES (@pIdAnalisis, @pIdArchivo);

        -- Confirma la transacción si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Maneja el error mostrando detalles
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanza el error para que sea manejado fuera del procedimiento si es necesario
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1;
    END CATCH
END
GO



-- =============================================
-- Autor:                Jesner Melgara Murillo
-- Fecha de creación:     2024-10-20
-- Descripción:            Insertar datos en la tabla intermedia TipoAnalisis_SolicitudAnalisis
-- =============================================
CREATE OR ALTER PROCEDURE PA_InsertarTipoAnalisis_SolicitudAnalisis
    @pIdTipoAnalisis INT,    -- ID del tipo de análisis (requerido)
    @pIdAnalisis INT         -- ID de la solicitud de análisis (requerido)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Inserta en la tabla intermedia TipoAnalisis_SolicitudAnalisis
        INSERT INTO dbo.TipoAnalisis_SolicitudAnalisis (IdTipoAnalisis, IdAnalisis)
        VALUES (@pIdTipoAnalisis, @pIdAnalisis);

        -- Confirma la transacción si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Manejo del error mostrando detalles
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanza el error para que sea manejado fuera del procedimiento si es necesario
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1;
    END CATCH
END
GO

