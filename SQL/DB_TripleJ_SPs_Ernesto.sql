USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-29
-- Descripción:		    Solicitar solicitudes de analisis por filtrado y otros datos
-- =============================================
EXEC [dbo].[PA_ObtenerBandejaAnalisis] @pTN_Estado = 13, @pTF_FechaInicio = '2024-11-07 00:00:00.0000000', @pTF_FechaFin = NULL;

CREATE OR ALTER PROCEDURE [dbo].[PA_ObtenerBandejaAnalisis]
    @pTN_Estado INT,
    @pTF_FechaInicio DATETIME2 = NULL,
    @pTF_FechaFin DATETIME2 = NULL
AS
BEGIN
    BEGIN TRY

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
        FROM [Proyecto_Analisis].[dbo].[TSOLITEL_SolicitudAnalisis] AS SOLI
        INNER JOIN TSOLITEL_Estado AS ES ON ES.TN_IdEstado = SOLI.TN_IdEstado
        WHERE ES.TN_IdEstado = @pTN_Estado AND
			(@pTF_FechaInicio IS NULL OR TF_FechaDeCreacion >= @pTF_FechaInicio) AND
			(@pTF_FechaFin IS NULL OR TF_FechaDeCreacion <= @pTF_FechaFin)
		ORDER BY TN_IdAnalisis DESC;

    END TRY
    BEGIN CATCH
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-29
-- Descripción:		    Historial de solicitud de analisis
-- =============================================

CREATE OR ALTER PROCEDURE PA_ConsultarHistoricoSolicitudAnalisis
    @PN_IdSolicitudAnalisis INT
AS
BEGIN
    BEGIN TRY

         SELECT 
			H.TN_IdHistorial, 
			H.TC_Observacion, 
			H.TF_FechaDeModificacion, 
			Usuario.TN_IdUsuario,
			CONCAT(Usuario.TC_Nombre, ' ', Usuario.TC_Apellido) AS TC_NombreUsuario,
			Estado.TN_IdEstado, 
			Estado.TC_Nombre AS TC_NombreEstado,
			H.TN_IdAnalisis, 
			H.TN_IdSolicitud

        FROM TSOLITEL_Historial AS H
		INNER JOIN TSOLITEL_Estado AS Estado ON H.TN_IdEstado = Estado.TN_IdEstado
		INNER JOIN TSOLITEL_Usuario AS Usuario ON H.TN_IdUsuario = Usuario.TN_IdUsuario
        WHERE TN_IdAnalisis = @PN_IdSolicitudAnalisis
		ORDER BY H.TN_IdHistorial DESC;

    END TRY
    BEGIN CATCH
       
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-29
-- Descripción:		    Mover solicitud de analisis a finalizado
-- =============================================

CREATE OR ALTER PROCEDURE PA_ActualizarEstadoFinalizadoSolicitudAnalisis
	@pTN_IdSolicitud INT,
	@PN_IdUsuario INT,
	@PC_Observacion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @IdEstado int;

        EXEC PA_CambiarEstadoSolicitudAnalisis @pTN_IdSolicitud, 'Finalizado', 'Analisis', @TN_IdEstado = @IdEstado OUTPUT;

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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-29
-- Descripción:		    Mover solicitud de analisis a legajo
-- =============================================

CREATE OR ALTER PROCEDURE PA_ActualizarEstadoLegajoSolicitudAnalisis
	@pTN_IdSolicitud INT,
	@PN_IdUsuario INT,
	@PC_Observacion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @IdEstado int;

        EXEC PA_CambiarEstadoSolicitudAnalisis @pTN_IdSolicitud, 'Legajo', 'Analisis', @TN_IdEstado = @IdEstado OUTPUT;

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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-29
-- Descripción:		    Devulve una solicitud en estado finalizado a tramitado
-- =============================================

CREATE OR ALTER PROCEDURE PA_DevolverATramitado
	@pTN_IdSolicitud INT,
	@pTN_IdUsuario INT,
	@pTC_Observacion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @IdEstado int;

        -- Cambiar el estado de la solicitud a 'Sin Efecto'
        EXEC PA_CambiarEstadoSolicitudProveedor @pTN_IdSolicitud, 'Tramitado', 'Proveedor', @TN_IdEstado = @IdEstado OUTPUT;

		EXEC [PA_InsertarHistoricoSolicitud] @pTN_IdSolicitud, NULL, @pTN_IdUsuario, @pTC_Observacion, @IdEstado;

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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-29
-- Descripción:		    Devulve una solicitud en estado finalizado o en legajo a analizado 
-- =============================================

CREATE OR ALTER PROCEDURE PA_DevolverATramitadoAnalisis
	@pTN_IdSolicitud INT,
	@pTN_IdUsuario INT,
	@pTC_Observacion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @IdEstado int;

        EXEC PA_CambiarEstadoSolicitudAnalisis @pTN_IdSolicitud, 'Analizado', 'Analisis', @TN_IdEstado = @IdEstado OUTPUT;

		EXEC [PA_InsertarHistoricoSolicitud] NULL, @pTN_IdSolicitud, @pTN_IdUsuario, @pTC_Observacion, @IdEstado;

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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_CategoriaDelito
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarCategoriaDelito
	@pTN_IdCategoriaDelito INT OUTPUT,-- Parámetro de salida para el ID
    @pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar la nueva categoría de delito y obtener el ID recién generado
        INSERT INTO dbo.TSOLITEL_CategoriaDelito
            (TC_Nombre, TC_Descripcion, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, 0);  -- TB_Borrado por defecto es 0 (no eliminado)

        -- Obtener el último ID insertado
        SET @pTN_IdCategoriaDelito = SCOPE_IDENTITY();

        -- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_CategoriaDelito
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarCategoriaDelito
    @pTN_IdCategoriaDelito INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            TN_IdCategoriaDelito,
            TC_Nombre,
            TC_Descripcion,
            TB_Borrado
        FROM dbo.TSOLITEL_CategoriaDelito WITH (NOLOCK)
        WHERE (@pTN_IdCategoriaDelito IS NULL OR TN_IdCategoriaDelito = @pTN_IdCategoriaDelito)
          AND TB_Borrado = 0  -- Solo mostrar categorías que no están borradas lógicamente
        ORDER BY TN_IdCategoriaDelito DESC;
    END TRY
    BEGIN CATCH
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_CategoriaDelito
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarCategoriaDelito
    @pTN_IdCategoriaDelito INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar lógicamente cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_CategoriaDelito
        SET TB_Borrado = 1
        WHERE TN_IdCategoriaDelito = @pTN_IdCategoriaDelito;

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

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_Condicion
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarCondicion
	@pTN_IdCondicion INT OUTPUT,
    @pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO dbo.TSOLITEL_Condicion
            (TC_Nombre, TC_Descripcion, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, 0);  -- TB_Borrado por defecto es 0 (no eliminado)

		SET @pTN_IdCondicion = SCOPE_IDENTITY();

        -- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_Condicion
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarCondicion
    @pTN_IdCondicion INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            TN_IdCondicion,
            TC_Nombre,
            TC_Descripcion,
            TB_Borrado
        FROM dbo.TSOLITEL_Condicion WITH (NOLOCK)
        WHERE (@pTN_IdCondicion IS NULL OR TN_IdCondicion = @pTN_IdCondicion)
          AND TB_Borrado = 0  -- Solo mostrar condiciones que no están borradas lógicamente
        ORDER BY TN_IdCondicion DESC;
    END TRY
    BEGIN CATCH
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_Condicion
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarCondicion
    @pTN_IdCondicion INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar lógicamente cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_Condicion
        SET TB_Borrado = 1
        WHERE TN_IdCondicion = @pTN_IdCondicion;

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

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:			    Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_Delito
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarDelito
    @pTN_IdDelito INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            Delito.TN_IdDelito,
            Delito.TC_Nombre,
            Delito.TC_Descripcion,
            Delito.TN_IdCategoriaDelito,
            Delito.TB_Borrado
        FROM dbo.TSOLITEL_Delito AS Delito WITH (NOLOCK)
		INNER JOIN dbo.TSOLITEL_CategoriaDelito AS CatDelito WITH (NOLOCK)
		ON Delito.TN_IdCategoriaDelito = CatDelito.TN_IdCategoriaDelito
        WHERE (@pTN_IdDelito IS NULL OR Delito.TN_IdDelito = @pTN_IdDelito) 
		AND (Delito.TB_Borrado = 0) AND (CatDelito.TB_Borrado = 0) -- Filtro opcional
        ORDER BY TN_IdDelito DESC;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1; -- Indicar que la operación falló
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
-- Autor:			    Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_Delito
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarDelitosPorCategoria
    @pTN_IdCategoriaDelito INT = NULL  -- Parámetro opcional para filtrar por Id de Categoría
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            Delito.TN_IdDelito,
            Delito.TC_Nombre,
            Delito.TC_Descripcion,
            Delito.TN_IdCategoriaDelito,
            Delito.TB_Borrado
        FROM dbo.TSOLITEL_Delito AS Delito WITH (NOLOCK)
        INNER JOIN dbo.TSOLITEL_CategoriaDelito AS CatDelito WITH (NOLOCK)
        ON Delito.TN_IdCategoriaDelito = CatDelito.TN_IdCategoriaDelito
        WHERE (@pTN_IdCategoriaDelito IS NULL OR Delito.TN_IdCategoriaDelito = @pTN_IdCategoriaDelito)
        AND (Delito.TB_Borrado = 0)  -- Aseguramos que no esté marcado como borrado
        AND (CatDelito.TB_Borrado = 0)  -- Filtramos categorías no borradas
        ORDER BY TN_IdDelito ASC;  -- Ordenamos por Id de Delito
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1; -- Indicar que la operación falló
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
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_Delito
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarDelito
	@pTN_IdDelito INT OUTPUT,
    @pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255),
    @pTN_IdCategoriaDelito INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    
    -- Iniciar la transacción
    BEGIN TRANSACTION;
    
    BEGIN TRY
		
		IF NOT EXISTS (SELECT TOP 1 1 FROM TSOLITEL_CategoriaDelito 
			WHERE TN_IdCategoriaDelito = @pTN_IdCategoriaDelito AND TB_Borrado = 0)
		BEGIN
			-- Hacer rollback si no existe la categoría o está borrada
			ROLLBACK TRANSACTION;
    
			-- Lanzar un error con un mensaje personalizado
			RAISERROR('La categoría de delito con Id %d no existe o ha sido eliminada.', 16, 1, @pTN_IdCategoriaDelito);
			RETURN -1;  -- Salir del procedimiento si hubo un error
		END


        INSERT INTO dbo.TSOLITEL_Delito
            (TC_Nombre, TC_Descripcion, TN_IdCategoriaDelito, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, @pTN_IdCategoriaDelito, 0);
        
		SET @pTN_IdDelito = SCOPE_IDENTITY();

        -- Comprobar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            -- Hacer rollback si hay un error
            ROLLBACK TRANSACTION;
            RETURN -1;  -- Salir del procedimiento si hubo un error
        END

        -- Si todo salió bien, hacer commit
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            -- Si la transacción sigue activa, revertir
            ROLLBACK TRANSACTION;
        END

        -- Levantar el error
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
-- Autor:		        Ernesto Vega Rosriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_Delito
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarDelito
    @pTN_IdDelito INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Eliminar de forma lógica cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_Delito
        SET TB_Borrado = 1
        WHERE TN_IdDelito = @pTN_IdDelito;

        -- Comprobar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0 OR @@ROWCOUNT = 0
        BEGIN
            -- Hacer rollback si hay un error o si no se encontró el registro
            ROLLBACK TRANSACTION;
            IF @@ROWCOUNT = 0
            BEGIN
                RAISERROR('No se encontró ningún registro con el Id especificado.', 16, 1);
            END
            RETURN -1;  -- Salir del procedimiento si hubo un error
        END

        -- Si todo salió bien, hacer commit
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            -- Si la transacción sigue activa, revertir
            ROLLBACK TRANSACTION;
        END

        -- Levantar el error
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
-- Autor:			    Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-21
-- Descripción:		    Consulta un registro específico de la tabla TSOLITEL_Fiscalia por su ID
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarFiscalia
    @pTN_IdFiscalia INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            Fiscalia.TN_IdFiscalia,
            Fiscalia.TC_Nombre,
            Fiscalia.TB_Borrado
        FROM dbo.TSOLITEL_Fiscalia AS Fiscalia WITH (NOLOCK)
        WHERE (@pTN_IdFiscalia IS NULL OR Fiscalia.TN_IdFiscalia = @pTN_IdFiscalia) 
		AND (Fiscalia.TB_Borrado = 0)  -- Filtro para registros no eliminados
        ORDER BY TN_IdFiscalia DESC;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();
        
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        RETURN -1; -- Indicar que la operación falló
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
-- Autor:			    Ernesto Vega Rodriguez		        
-- Fecha de creación: 	2024-10-21
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_Fiscalia
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarFiscalia
    @pTN_IdFiscalia INT OUTPUT,
	@pTC_Nombre VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;
    
    -- Iniciar la transacción
    BEGIN TRANSACTION;
    
    BEGIN TRY
        INSERT INTO dbo.TSOLITEL_Fiscalia
            (TC_Nombre, TB_Borrado)
        VALUES
            (@pTC_Nombre, 0);
        
		SET @pTN_IdFiscalia = SCOPE_IDENTITY();

        -- Comprobar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            -- Hacer rollback si hay un error
            ROLLBACK TRANSACTION;
            RETURN -1;  -- Salir del procedimiento si hubo un error
        END

        -- Si todo salió bien, hacer commit
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            -- Si la transacción sigue activa, revertir
            ROLLBACK TRANSACTION;
        END

        -- Levantar el error
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
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-21
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_Fiscalia
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarFiscalia
    @pTN_IdFiscalia INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Eliminar de forma lógica cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_Fiscalia
        SET TB_Borrado = 1
        WHERE TN_IdFiscalia = @pTN_IdFiscalia;

        -- Comprobar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0 OR @@ROWCOUNT = 0
        BEGIN
            -- Hacer rollback si hay un error o si no se encontró el registro
            ROLLBACK TRANSACTION;
            IF @@ROWCOUNT = 0
            BEGIN
                RAISERROR('No se encontró ningún registro con el Id especificado.', 16, 1);
            END
            RETURN -1;  -- Salir del procedimiento si hubo un error
        END

        -- Si todo salió bien, hacer commit
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            -- Si la transacción sigue activa, revertir
            ROLLBACK TRANSACTION;
        END

        -- Levantar el error
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
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_Modalidad
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarModalidad
    @pTN_IdModalidad INT OUTPUT,
	@pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        INSERT INTO dbo.TSOLITEL_Modalidad
            (TC_Nombre, TC_Descripcion, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, 0);  -- TB_Borrado por defecto es 0 (no borrado)

		SET @pTN_IdModalidad = SCOPE_IDENTITY();

        -- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_Modalidad
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarModalidad
    @pTN_IdModalidad INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            TN_IdModalidad,
            TC_Nombre,
            TC_Descripcion,
            TB_Borrado
        FROM dbo.TSOLITEL_Modalidad WITH (NOLOCK)
        WHERE (@pTN_IdModalidad IS NULL OR TN_IdModalidad = @pTN_IdModalidad)
          AND TB_Borrado = 0  -- Solo mostrar modalidades que no están borradas lógicamente
        ORDER BY TN_IdModalidad DESC;
    END TRY
    BEGIN CATCH
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_Modalidad
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarModalidad
    @pTN_IdModalidad INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar lógicamente cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_Modalidad
        SET TB_Borrado = 1
        WHERE TN_IdModalidad = @pTN_IdModalidad;

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

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_SubModalidad
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarSubModalidad
	@pTN_IdSubModalidad INT OUTPUT, 
    @pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255),
    @pTN_IdModalidad INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    -- Validar si la modalidad asociada existe y no está borrada
    IF NOT EXISTS (SELECT TOP 1 1 FROM TSOLITEL_Modalidad 
                   WHERE TN_IdModalidad = @pTN_IdModalidad AND TB_Borrado = 0)
    BEGIN
        -- Hacer rollback si no existe la modalidad o está borrada
        ROLLBACK TRANSACTION;
        
        -- Lanzar un error con un mensaje personalizado
        RAISERROR('La modalidad con Id %d no existe o ha sido eliminada.', 16, 1, @pTN_IdModalidad);
        RETURN -1;  -- Salir del procedimiento si hubo un error
    END

    BEGIN TRY
        -- Insertar la nueva submodalidad
        INSERT INTO dbo.TSOLITEL_SubModalidad
            (TC_Nombre, TC_Descripcion, TN_IdModalida, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, @pTN_IdModalidad, 0);  -- TB_Borrado por defecto es 0 (no borrado)

		SET @pTN_IdSubModalidad = SCOPE_IDENTITY();

        -- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_SubModalidad
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarSubModalidad
    @pTN_IdSubModalidad INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            TSubModalidad.TN_IdSubModalidad,
            TSubModalidad.TC_Nombre,
            TSubModalidad.TC_Descripcion,
            TSubModalidad.TN_IdModalida,
            TSubModalidad.TB_Borrado
        FROM dbo.TSOLITEL_SubModalidad AS TSubModalidad WITH (NOLOCK)
		INNER JOIN dbo.TSOLITEL_Modalidad AS TModalidad WITH (NOLOCK)
		ON TSubModalidad.TN_IdModalida = TModalidad.TN_IdModalidad
        WHERE (@pTN_IdSubModalidad IS NULL OR TSubModalidad.TN_IdSubModalidad = @pTN_IdSubModalidad)
          AND (TSubModalidad.TB_Borrado = 0) AND (TModalidad.TB_Borrado = 0)  -- Solo mostrar submodalidades que no están borradas lógicamente
        ORDER BY TN_IdSubModalidad DESC;
    END TRY
    BEGIN CATCH
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_SubModalidad
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarSubModalidadPorModalidad
    @pTN_IdModalidad INT = NULL  -- Parámetro opcional para filtrar por Id de Modalidad
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            TSubModalidad.TN_IdSubModalidad,
            TSubModalidad.TC_Nombre,
            TSubModalidad.TC_Descripcion,
            TSubModalidad.TN_IdModalida,
            TSubModalidad.TB_Borrado
        FROM dbo.TSOLITEL_SubModalidad AS TSubModalidad WITH (NOLOCK)
        INNER JOIN dbo.TSOLITEL_Modalidad AS TModalidad WITH (NOLOCK)
        ON TSubModalidad.TN_IdModalida = TModalidad.TN_IdModalidad
        WHERE (@pTN_IdModalidad IS NULL OR TSubModalidad.TN_IdModalida = @pTN_IdModalidad)
          AND (TSubModalidad.TB_Borrado = 0) AND (TModalidad.TB_Borrado = 0)  -- Solo mostrar submodalidades que no están borradas lógicamente
        ORDER BY TSubModalidad.TN_IdSubModalidad ASC;
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
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_SubModalidad
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarSubModalidad
    @pTN_IdSubModalidad INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar lógicamente cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_SubModalidad
        SET TB_Borrado = 1
        WHERE TN_IdSubModalidad = @pTN_IdSubModalidad;

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

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:                Ernesto Vega Rodriguez
-- Fecha de creación:    2024-10-24
-- Descripción:          Inserta un registro en la tabla TSOLITEL_TipoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarTipoAnalisis
    @pTN_IdTipoAnalisis INT OUTPUT,  -- Parámetro de salida para obtener el ID generado
    @pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar un nuevo tipo de análisis
        INSERT INTO dbo.TSOLITEL_TipoAnalisis
            (TC_Nombre, TC_Descripcion, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, 0);  -- TB_Borrado por defecto es 0 (no borrado)

        -- Obtener el último ID insertado
        SET @pTN_IdTipoAnalisis = SCOPE_IDENTITY();

        -- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:                Ernesto Vega Rodriguez
-- Fecha de creación:    2024-10-24
-- Descripción:          Elimina lógicamente un registro de la tabla TSOLITEL_TipoAnalisis
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarTipoAnalisis
    @pTN_IdTipoAnalisis INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar lógicamente cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_TipoAnalisis
        SET TB_Borrado = 1
        WHERE TN_IdTipoAnalisis = @pTN_IdTipoAnalisis;

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

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_TipoSolicitud
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarTipoSolicitud
    @pTN_IdTipoSolicitud INT OUTPUT,
	@pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar un nuevo tipo de solicitud
        INSERT INTO dbo.TSOLITEL_TipoSolicitud
            (TC_Nombre, TC_Descripcion, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, 0);  -- TB_Borrado por defecto es 0 (no borrado)

		SET @pTN_IdTipoSolicitud = SCOPE_IDENTITY();

        -- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_TipoSolicitud
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarTipoSolicitud
    @pTN_IdTipoSolicitud INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            TN_IdTipoSolicitud,
            TC_Nombre,
            TC_Descripcion,
            TB_Borrado
        FROM dbo.TSOLITEL_TipoSolicitud WITH (NOLOCK)
        WHERE (@pTN_IdTipoSolicitud IS NULL OR TN_IdTipoSolicitud = @pTN_IdTipoSolicitud)
          AND TB_Borrado = 0  -- Solo mostrar tipos de solicitud que no están borrados lógicamente
        ORDER BY TN_IdTipoSolicitud DESC;
    END TRY
    BEGIN CATCH
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_TipoSolicitud
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarTipoSolicitud
    @pTN_IdTipoSolicitud INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar lógicamente cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_TipoSolicitud
        SET TB_Borrado = 1
        WHERE TN_IdTipoSolicitud = @pTN_IdTipoSolicitud;

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

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Inserta un registro en la tabla TSOLITEL_TipoDato
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_InsertarTipoDato
    @pTN_IdTipoDato INT OUTPUT,
	@pTC_Nombre VARCHAR(100),
    @pTC_Descripcion VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Insertar un nuevo tipo de dato
        INSERT INTO dbo.TSOLITEL_TipoDato
            (TC_Nombre, TC_Descripcion, TB_Borrado)
        VALUES
            (@pTC_Nombre, @pTC_Descripcion, 0);  -- TB_Borrado por defecto es 0 (no borrado)

		SET @pTN_IdTipoDato = SCOPE_IDENTITY();

        -- Verificar si hubo algún error
        SET @Error = @@ERROR;
        IF @Error <> 0
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN -1;
        END

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Consulta los registros de la tabla TSOLITEL_TipoDato
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarTipoDato
    @pTN_IdTipoDato INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SELECT 
            TN_IdTipoDato,
            TC_Nombre,
            TC_Descripcion,
            TB_Borrado
        FROM dbo.TSOLITEL_TipoDato WITH (NOLOCK)
        WHERE (@pTN_IdTipoDato IS NULL OR TN_IdTipoDato = @pTN_IdTipoDato)
          AND TB_Borrado = 0  -- Solo mostrar tipos de datos que no están borrados lógicamente
        ORDER BY TN_IdTipoDato DESC;
    END TRY
    BEGIN CATCH
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		       Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-13
-- Descripción:		    Elimina lógicamente un registro de la tabla TSOLITEL_TipoDato
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_EliminarTipoDato
    @pTN_IdTipoDato INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Eliminar lógicamente cambiando TB_Borrado a 1
        UPDATE dbo.TSOLITEL_TipoDato
        SET TB_Borrado = 1
        WHERE TN_IdTipoDato = @pTN_IdTipoDato;

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

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
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

USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Autor:		        Ernesto Vega Rodriguez
-- Fecha de creación: 	2024-10-29
-- Descripción:		    Procedimiento almacenado para consultar los registros de la tabla TSOLITEL_Estado
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarEstados
    @pTN_IdEstado INT = NULL  -- Parámetro opcional para filtrar por Id
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Consulta los registros de la tabla TSOLITEL_Estado
        SELECT 
            TN_IdEstado,
            TC_Nombre,
            TC_Descripcion,
            TC_Tipo
        FROM dbo.TSOLITEL_Estado WITH (NOLOCK)
        WHERE (@pTN_IdEstado IS NULL OR TN_IdEstado = @pTN_IdEstado)
        ORDER BY TN_IdEstado ASC;

        -- Si todo es exitoso, confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshace la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
-- Autor:                Ernesto Vega Rodriguez
-- Fecha de creación:    2024-10-16
-- Descripción:          Consulta solicitudes de proveedor por estado con filtros adicionales y paginación.
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarSolicitudesProveedorPorEstado
    @pPageNumber INT,
    @pPageSize INT,
    @pIdEstado INT = NULL,
    @pNumeroUnico INT = NULL,
    @pFechaInicio DATETIME2 = NULL,
    @pFechaFin DATETIME2 = NULL,
    @pCaracterIngresado VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        DECLARE @Offset INT = (@pPageNumber - 1) * @pPageSize;

        -- Realiza la consulta de solicitudes con JOINs, filtros adicionales y paginación
        SELECT 
            T.TN_IdSolicitud,
            T.TN_NumeroUnico,
            T.TN_NumeroCaso,
            T.TC_Imputado,
            T.TC_Ofendido,
            T.TC_Resennia,
            T.TB_Urgente,
            T.TB_Aprobado,
            T.TF_FechaDeCrecion,
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
            T.TN_IdUsuario

        FROM dbo.TSOLITEL_SolicitudProveedor AS T
        LEFT JOIN dbo.TSOLITEL_Proveedor AS Proveedor ON T.TN_IdProveedor = Proveedor.TN_IdProveedor
        LEFT JOIN dbo.TSOLITEL_Fiscalia AS Fiscalia ON T.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        LEFT JOIN dbo.TSOLITEL_Delito AS Delito ON T.TN_IdDelito = Delito.TN_IdDelito
        LEFT JOIN dbo.TSOLITEL_CategoriaDelito AS CategoriaDelito ON T.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        LEFT JOIN dbo.TSOLITEL_Modalidad AS Modalidad ON T.TN_IdModalida = Modalidad.TN_IdModalidad
        LEFT JOIN dbo.TSOLITEL_Estado AS Estado ON T.TN_IdEstado = Estado.TN_IdEstado
        LEFT JOIN dbo.TSOLITEL_SubModalidad AS SubModalidad ON T.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
        WHERE (@pIdEstado IS NULL OR T.TN_IdEstado = @pIdEstado)
          AND (@pNumeroUnico IS NULL OR T.TN_NumeroUnico = @pNumeroUnico)
          AND (@pFechaInicio IS NULL OR T.TF_FechaDeCrecion >= @pFechaInicio)
          AND (@pFechaFin IS NULL OR T.TF_FechaDeCrecion <= @pFechaFin)
          AND (@pCaracterIngresado IS NULL OR 
               T.TC_Imputado LIKE '%' + @pCaracterIngresado + '%' OR
               T.TC_Ofendido LIKE '%' + @pCaracterIngresado + '%' OR
               T.TC_Resennia LIKE '%' + @pCaracterIngresado + '%')
        ORDER BY T.TN_IdSolicitud DESC
        OFFSET @Offset ROWS FETCH NEXT @pPageSize ROWS ONLY;

        -- Confirmar la transacción si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshacer la transacción
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanzar el error para ser manejado fuera del procedimiento si es necesario
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

EXEC PA_ConsultarSolicitud 2 
USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:                Ernesto Vega Rodriguez
-- Fecha de creación:    2024-10-16
-- Descripción:          Consulta solicitudes de proveedor por estado con filtros adicionales y paginación.
-- =============================================

CREATE OR ALTER PROCEDURE dbo.PA_ConsultarSolicitud
    @pTN_IdSolicitud INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        -- Realiza la consulta de solicitudes con JOINs, filtros adicionales y paginación
        SELECT 
            T.TN_IdSolicitud,
            T.TN_NumeroUnico,
            T.TN_NumeroCaso,
            T.TC_Imputado,
            T.TC_Ofendido,
            T.TC_Resennia,
            T.TB_Urgente,
            T.TB_Aprobado,
            T.TF_FechaDeCrecion,
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
            T.TN_IdUsuario,
			Usuario.TC_Nombre +' '+ Usuario.TC_Apellido AS TC_NombreUsuarioCreador

        FROM dbo.TSOLITEL_SolicitudProveedor AS T
        INNER JOIN dbo.TSOLITEL_Proveedor AS Proveedor ON T.TN_IdProveedor = Proveedor.TN_IdProveedor
        INNER JOIN dbo.TSOLITEL_Fiscalia AS Fiscalia ON T.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        INNER JOIN dbo.TSOLITEL_Delito AS Delito ON T.TN_IdDelito = Delito.TN_IdDelito
        INNER JOIN dbo.TSOLITEL_CategoriaDelito AS CategoriaDelito ON T.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        INNER JOIN dbo.TSOLITEL_Modalidad AS Modalidad ON T.TN_IdModalida = Modalidad.TN_IdModalidad
        INNER JOIN dbo.TSOLITEL_Estado AS Estado ON T.TN_IdEstado = Estado.TN_IdEstado
        INNER JOIN dbo.TSOLITEL_SubModalidad AS SubModalidad ON T.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
		INNER JOIN dbo.TSOLITEL_Usuario AS Usuario ON T.TN_IdUsuario = Usuario.TN_IdUsuario
        WHERE T.TN_IdSolicitud = @pTN_IdSolicitud;

        -- Confirmar la transacción si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshacer la transacción
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanzar el error para ser manejado fuera del procedimiento si es necesario
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
-- Author:                Ernesto Vega Rodriguez
-- Create date:           29-10-2024
-- Description:           Procedimiento almacenado para consultar solicitudes de análisis con información adicional
-- =============================================
CREATE OR ALTER PROCEDURE dbo.PA_ConsultarSolicitudesAnalisis
    @pPageNumber INT,
    @pPageSize INT,
    @pIdEstado INT = NULL,
    @pNumeroUnico VARCHAR(100) = NULL,
    @pFechaInicio DATETIME2 = NULL,
    @pFechaFin DATETIME2 = NULL,
    @pCaracterIngresado VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        DECLARE @Offset INT = (@pPageNumber - 1) * @pPageSize;

        -- Realiza la consulta con JOINs, filtros adicionales y paginación
        SELECT 
              SP.TN_IdSolicitud,
    SP.TN_NumeroUnico,
    CONCAT(U.TC_Nombre, ' ', U.TC_Apellido) AS TC_NombreUsuario,
    O.TC_Nombre AS TC_NombreOficina,
    SP.TF_FechaDeCrecion AS TF_FechaDeCreacion, -- From SolicitudProveedor table
    E.TC_Nombre AS TC_NombreEstado,
    SA.TF_FechaDeCreacion AS TF_FechaDeCreacion_Analisis, -- Optional alias if needed
    SA.TF_FechaDeHecho,  -- Ensure this column is selected
    SP.TB_Urgente,
    SA.TN_IdAnalisis,
    SP.TB_Aprobado,
    SA.TC_OtrosDetalles,
    SA.TC_OtrosObjetivosDeAnalisis

        FROM dbo.TSOLITEL_SolicitudProveedor AS SP
        INNER JOIN dbo.TSOLITEL_SolicitudAnalisis_SolicitudProveedor AS SASP ON SP.TN_IdSolicitud = SASP.TN_IdSolicitud
        INNER JOIN dbo.TSOLITEL_SolicitudAnalisis AS SA ON SASP.TN_IdAnalisis = SA.TN_IdAnalisis
        INNER JOIN dbo.TSOLITEL_Usuario AS U ON SP.TN_IdUsuario = U.TN_IdUsuario
        INNER JOIN dbo.TSOLITEL_Oficina AS O ON SP.TN_IdOficina = O.TN_IdOficina
        INNER JOIN dbo.TSOLITEL_Estado AS E ON SP.TN_IdEstado = E.TN_IdEstado
        WHERE (@pIdEstado IS NULL OR SP.TN_IdEstado = @pIdEstado)
          AND (@pNumeroUnico IS NULL OR SP.TN_NumeroUnico = @pNumeroUnico)
          AND (@pFechaInicio IS NULL OR SP.TF_FechaDeCrecion >= @pFechaInicio)
          AND (@pFechaFin IS NULL OR SP.TF_FechaDeCrecion <= @pFechaFin)
          AND (@pCaracterIngresado IS NULL OR 
               U.TC_Nombre LIKE '%' + @pCaracterIngresado + '%' OR
               U.TC_Apellido LIKE '%' + @pCaracterIngresado + '%' OR
               SP.TC_Imputado LIKE '%' + @pCaracterIngresado + '%' OR
               SP.TC_Ofendido LIKE '%' + @pCaracterIngresado + '%' OR
               SP.TC_Resennia LIKE '%' + @pCaracterIngresado + '%')
        ORDER BY SP.TN_IdSolicitud DESC
        OFFSET @Offset ROWS FETCH NEXT @pPageSize ROWS ONLY;

        -- Confirmar la transacción si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshacer la transacción
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanzar el error para ser manejado fuera del procedimiento si es necesario
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

