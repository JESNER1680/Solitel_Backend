CREATE OR ALTER PROCEDURE PA_ActualizarEstadoAnalizadoSolicitudAnalisis
@PN_IdSolicitudAnalisis int,
@PN_IdUsuario int,
@PC_Observacion varchar(255) = NULL
AS
BEGIN
	BEGIN TRY
        DECLARE @IdEstado int;

        -- Cambiar el estado de la solicitud a 'Analizado'
        EXEC PA_CambiarEstadoSolicitudAnalisis @PN_IdSolicitudAnalisis, 'Analizado', 'Analisis', @TN_IdEstado = @IdEstado OUTPUT;

		EXEC [PA_InsertarHistoricoSolicitud] NULL, @PN_IdSolicitudAnalisis, @PN_IdUsuario, @PC_Observacion, @IdEstado;

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
