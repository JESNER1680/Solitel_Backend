CREATE OR ALTER PROCEDURE PA_ConsultarSolicitudesPorNumeroUnico
    @PN_NumeroUnico INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta para obtener solicitudes por número único
        SELECT 
            SP.TN_IdSolicitud, 
            SP.TN_NumeroUnico, 
            P.TC_Nombre 
        FROM TSOLITEL_SolicitudProveedor SP
        INNER JOIN TSOLITEL_Proveedor P ON P.TN_IdProveedor = SP.TN_IdProveedor
        WHERE SP.TN_NumeroUnico = @PN_NumeroUnico;

        -- Si todo está correcto, se confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si hay un error, se revierte la transacción
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
