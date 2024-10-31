CREATE OR ALTER PROCEDURE PA_RelacionarRequerimientosProveedor
@PN_IdSolicitudProveedor int,
@PN_IdRequerimientoProveedor int
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar la relación entre solicitud y requerimiento
        INSERT INTO TSOLITEL_SolicitudProveedor_RequerimientoProveedor 
        (TN_IdSolicitud, TN_IdRequerimientoProveedor)
        VALUES (@PN_IdSolicitudProveedor, @PN_IdRequerimientoProveedor);

        -- Confirmar la transacción
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
