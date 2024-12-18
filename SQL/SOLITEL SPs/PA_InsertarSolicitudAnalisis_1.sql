USE [Proyecto_Analisis]
GO
/****** Object:  StoredProcedure [dbo].[PA_InsertarSolicitudAnalisis]    Script Date: 11/16/2024 1:05:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[PA_InsertarSolicitudAnalisis]
    @PN_IdUsuarioCreador INT,
    @TF_FechaDelHecho DATETIME2,
    @TC_OtrosDetalles VARCHAR(255),
    @TC_OtrosObjetivosDeAnalisis VARCHAR(255) = NULL,
    @TB_Aprobado BIT,
    @TF_FechaCreacion DATETIME2 = NULL,
    @TN_IdOficinaSolicitante INT,
	@TN_IdOficinaCreacion INT,
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
            TN_IdOficinaSolicitante,
			TN_IdOficinaCreacion,
			TN_IdUsuario
        )
        VALUES 
        (
            @TF_FechaDelHecho, 
            @TC_OtrosDetalles, 
            @TC_OtrosObjetivosDeAnalisis, 
            @TB_Aprobado, 
            GETDATE(),
            @TN_IdOficinaSolicitante,
			@TN_IdOficinaCreacion,
			@PN_IdUsuarioCreador
        );

        -- Capturar el ID generado
        SET @TN_IdSolicitudAnalisis = SCOPE_IDENTITY();

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        -- Cambiar el estado de la solicitud
        EXEC PA_CambiarEstadoSolicitudAnalisis @TN_IdSolicitudAnalisis, 'Aprobar Analisis', 'Analisis', @TN_IdEstado = @ResultadoIdEstado OUTPUT;

        -- Insertar en el historial de la solicitud
        EXEC PA_InsertarHistoricoSolicitud NULL, @TN_IdSolicitudAnalisis, @PN_IdUsuarioCreador, '', @ResultadoIdEstado;


		IF EXISTS 
		(
			SELECT TOP 1 1 
			FROM TSOLITEL_Usuario_Oficina AS UO 
				INNER JOIN TSOLITEL_Rol AS RO ON UO.TN_IdRol = RO.TN_IdRol
				INNER JOIN TSOLITEL_Rol_Permiso AS ROPE ON RO.TN_IdRol = ROPE.TN_IdRol
				INNER JOIN TSOLITEL_Permiso AS PE ON ROPE.TN_IdPermiso = PE.TN_IdPermiso
			WHERE @PN_IdUsuarioCreador = UO.TN_IdUsuario 
			AND UO.TN_IdOficina = @TN_IdOficinaCreacion 
			AND PE.TC_Nombre LIKE '%Aprobación Automatica%'
		)
        BEGIN
			BEGIN TRY
				-- RELIZA APROBACION AUTOMATICA DE LA SOLICITUD
				EXEC [dbo].[PA_AprobarSolicitudAnalisis]
					@pTN_IdSolicitud =  @TN_IdSolicitudAnalisis,
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
            ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
