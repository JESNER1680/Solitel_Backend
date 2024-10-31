CREATE OR ALTER PROCEDURE PA_ConsultarRequerimientosProveedor
    @PN_IdSolicitudProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los requerimientos del proveedor asociados a la solicitud
        SELECT RP.TN_IdRequerimientoProveedor, RP.TF_FechaDeInicio, RP.TF_FechaDeFinal, RP.TC_Requerimiento
        FROM TSOLITEL_RequerimientoProveedor RP
        INNER JOIN TSOLITEL_SolicitudProveedor_RequerimientoProveedor SPRP
            ON SPRP.TN_IdRequerimientoProveedor = RP.TN_IdRequerimientoProveedor
        WHERE SPRP.TN_IdSolicitud = @PN_IdSolicitudProveedor;

        -- Confirma la transacción si todo fue bien
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
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
