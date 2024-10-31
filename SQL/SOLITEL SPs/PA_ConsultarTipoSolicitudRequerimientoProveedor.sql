CREATE OR ALTER PROCEDURE PA_ConsultarTipoSolicitudes
    @PN_IdRequerimientoProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta para obtener los tipos de solicitudes
        SELECT 
            TS.TN_IdTipoSolicitud, 
            TS.TC_Nombre, 
            TS.TC_Descripcion 
        FROM TSOLITEL_TipoSolicitud TS
        INNER JOIN TSOLITEL_TipoSolicitud_RequerimientoProveedor TSRP
            ON TS.TN_IdTipoSolicitud = TSRP.TN_IdTipoSolicitud
        WHERE TSRP.TN_IdRequerimientoProveedor = @PN_IdRequerimientoProveedor;

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
