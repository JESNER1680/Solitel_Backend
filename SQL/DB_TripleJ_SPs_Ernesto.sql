-- TSOLITEL_CategoriaDelito

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
    @pTC_Nombre VARCHAR(50),
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
        ORDER BY TN_IdCategoriaDelito ASC;
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



-- TSOLITEL_Condicion
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
    @pTC_Nombre VARCHAR(50),
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
        ORDER BY TN_IdCondicion ASC;
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



-- TSOLITEL_Delito

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
        ORDER BY TN_IdDelito ASC;
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
    @pTC_Nombre VARCHAR(50),
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


-- TSOLITEL Fiscalia

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
        ORDER BY TN_IdFiscalia ASC;
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
	@pTC_Nombre VARCHAR(50)
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



-- TSOLITEL_Modalidad
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
	@pTC_Nombre VARCHAR(50),
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
        ORDER BY TN_IdModalidad ASC;
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



-- TSOLITEL_SubModalidad
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
    @pTC_Nombre VARCHAR(50),
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
        ORDER BY TN_IdSubModalidad ASC;
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



-- TSOLITEL_TipoSolicitud
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
	@pTC_Nombre VARCHAR(50),
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
        ORDER BY TN_IdTipoSolicitud ASC;
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



-- TSOLITEL_TipoDato
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
	@pTC_Nombre VARCHAR(50),
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
CREATE PROCEDURE dbo.PA_ConsultarTipoDato
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
        ORDER BY TN_IdTipoDato ASC;
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
CREATE PROCEDURE dbo.PA_EliminarTipoDato
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
