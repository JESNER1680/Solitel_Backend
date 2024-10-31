CREATE OR ALTER PROCEDURE PA_ConsultarArchivosDeSolicitudProveedor
    @PN_IdSolicitudProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los archivos asociados a una solicitud de proveedor
        SELECT A.TN_IdArchivo, A.TC_Nombre
        FROM TSOLITEL_SolicitudProveedor_RequerimientoProveedor SPRP
        INNER JOIN TSOLITEL_SolicitudProveedor SP
            ON SP.TN_IdSolicitud = SPRP.TN_IdSolicitud
        INNER JOIN TSOLITEL_RequerimientoProveedor_Archivo RPA
            ON RPA.TN_IdRequerimientoProveedor = SPRP.TN_IdRequerimientoProveedor
        INNER JOIN TSOLITEL_Archivo A
            ON A.TN_IdArchivo = RPA.TN_IdArchivo
        WHERE SP.TN_IdSolicitud = @PN_IdSolicitudProveedor;

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
