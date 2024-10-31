CREATE OR ALTER PROCEDURE PA_ConsultarHistoricoSolicitudProveedor
    @PN_IdSolicitudProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta el historial de la solicitud de proveedor
        SELECT TN_IdHistorial, TC_Observacion, TF_FechaDeModificacion, TN_IdUsuario, TN_IdEstado, TN_IdAnalisis, TN_IdSolicitud
        FROM TSOLITEL_Historial
        WHERE TN_IdSolicitud = @PN_IdSolicitudProveedor;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Captura el error y lo levanta
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
