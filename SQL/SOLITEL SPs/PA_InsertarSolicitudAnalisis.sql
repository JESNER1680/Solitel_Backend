USE [Proyecto_Analisis]
GO
/****** Object:  StoredProcedure [dbo].[PA_InsertarSolicitudAnalisis]    Script Date: 11/10/2024 1:03:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[PA_InsertarSolicitudAnalisis]
    @PN_IdUsuarioCreador INT,
    @TF_FechaDelHecho DATE,
    @TC_OtrosDetalles VARCHAR(255),
    @TC_OtrosObjetivosDeAnalisis VARCHAR(255) = NULL,
    @TB_Aprobado BIT,
    @TF_FechaCreacion DATE = NULL,
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
            @TF_FechaCreacion,
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
