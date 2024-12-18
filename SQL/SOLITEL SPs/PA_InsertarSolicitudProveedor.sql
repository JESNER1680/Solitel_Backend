CREATE OR ALTER PROCEDURE [dbo].[PA_InsertarSolicitudProveedor]
    @PN_NumeroUnico        VARCHAR(100),  -- Número único de la solicitud
    @PN_NumeroCaso         VARCHAR(100),  -- Número de caso
    @PC_Imputado           VARCHAR(150),  -- Nombre del imputado
    @PC_Ofendido           VARCHAR(150),  -- Nombre del ofendido
    @PC_Resennia           VARCHAR(255),  -- Descripción o reseña de la solicitud
    @PB_Urgente            BIT,           -- Indicador si es urgente
    @PB_Aprobado           BIT,           -- Indicador si está aprobado
    @PF_FechaCreacion      DATETIME,          -- Fecha de creación
    @PN_IdUsuarioCreador   INT,           -- ID del usuario que creó la solicitud
    @PN_IdDelito           INT,           -- ID del delito
    @PN_IdCategoriaDelito  INT,           -- ID de la categoría del delito
    @PN_IdModalidad        INT,           -- ID de la modalidad
    @PN_IdSubModalidad     INT,           -- ID de la submodalidad
    @PN_IdEstado           INT,           -- ID del estado inicial
    @PN_IdProveedor        INT,           -- ID del proveedor
    @PN_IdFiscalia         INT,           -- ID de la fiscalía
    @PN_IdOficina          INT,           -- ID de la oficina
    @IdSolicitudInsertada  INT OUTPUT     -- Parámetro de salida para el ID de la solicitud insertada
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ResultadoIdEstado INT;

    BEGIN TRY
        -- Iniciar la transacción
        BEGIN TRANSACTION;

        -- Insertar nueva solicitud en la tabla TSOLITEL_SolicitudProveedor
        INSERT INTO dbo.TSOLITEL_SolicitudProveedor
        (
            TC_NumeroUnico, 
            TC_NumeroCaso, 
            TC_Imputado, 
            TC_Ofendido, 
            TC_Resennia, 
            TB_Urgente, 
            TB_Aprobado, 
            TF_FechaDeCreacion,  -- Nombre corregido según la tabla
            TN_IdUsuario, 
            TN_IdDelito, 
            TN_IdCategoriaDelito, 
            TN_IdModalida,      -- Nombre corregido según la tabla
            TN_IdSubModalidad, 
            TN_IdEstado, 
            TN_IdProveedor, 
            TN_IdFiscalia, 
            TN_IdOficina
        )
        VALUES
        (
            @PN_NumeroUnico, 
            @PN_NumeroCaso, 
            @PC_Imputado, 
            @PC_Ofendido, 
            @PC_Resennia, 
            @PB_Urgente, 
            @PB_Aprobado, 
            @PF_FechaCreacion, 
            @PN_IdUsuarioCreador, 
            @PN_IdDelito, 
            @PN_IdCategoriaDelito, 
            @PN_IdModalidad, 
            @PN_IdSubModalidad, 
            @PN_IdEstado, 
            @PN_IdProveedor, 
            @PN_IdFiscalia, 
            @PN_IdOficina
        );

        -- Capturar el ID de la solicitud insertada
        SET @IdSolicitudInsertada = SCOPE_IDENTITY();

        -- Confirmar la transacción de la inserción
        COMMIT TRANSACTION;

        -- Llamar a PA_CambiarEstadoSolicitudProveedor para cambiar el estado de la solicitud a 'Creado'
        EXEC PA_CambiarEstadoSolicitudProveedor 
            @TN_IdSolicitudProveedor = @IdSolicitudInsertada, 
            @TC_NombreEstado = 'Creado', 
            @TC_TipoEstado = 'Proveedor', 
            @TN_IdEstado = @ResultadoIdEstado OUTPUT;

        -- Verificar si el estado fue actualizado correctamente antes de insertar en el historial
        IF @ResultadoIdEstado IS NOT NULL
        BEGIN
            -- Insertar en el historial de la solicitud
            EXEC PA_InsertarHistoricoSolicitud 
                @PN_IdSolicitudProveedor = @IdSolicitudInsertada, 
                @PN_IdSolicitudAnalisis = NULL, 
                @PN_IdUsuario = @PN_IdUsuarioCreador, 
                @PC_Observacion = '', 
                @PN_IdEstado = @ResultadoIdEstado;
        END
        ELSE
        BEGIN
            RAISERROR('Error al cambiar el estado de la solicitud a "Creado".', 16, 1);
            RETURN;
        END

		IF EXISTS 
		(
			SELECT TOP 1 1 
			FROM TSOLITEL_Usuario_Oficina AS UO 
				INNER JOIN TSOLITEL_Rol AS RO ON UO.TN_IdRol = RO.TN_IdRol
				INNER JOIN TSOLITEL_Rol_Permiso AS ROPE ON RO.TN_IdRol = ROPE.TN_IdRol
				INNER JOIN TSOLITEL_Permiso AS PE ON ROPE.TN_IdPermiso = PE.TN_IdPermiso
			WHERE @PN_IdUsuarioCreador = UO.TN_IdUsuario 
			AND UO.TN_IdOficina = @PN_IdOficina 
			AND PE.TC_Nombre LIKE '%Aprobación Automatica%'
		)
        BEGIN
			BEGIN TRY
				-- RELIZA APROBACION AUTOMATICA DE LA SOLICITUD
				EXEC [dbo].[PA_AprobarSolicitudProveedor]
						@pTN_IdSolicitud = @IdSolicitudInsertada,
						@PN_IdUsuario = @PN_IdUsuarioCreador,
						@PC_Observacion = NULL
			END TRY
			BEGIN CATCH
				RAISERROR('Error al cambiar el estado de la solicitud a "Aprobado".', 16, 1);
				RETURN;
			END CATCH
		END

    END TRY
    BEGIN CATCH
        -- Si ocurre un error, revertir la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Manejar el error
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END