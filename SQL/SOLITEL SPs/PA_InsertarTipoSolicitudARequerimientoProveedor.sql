CREATE OR ALTER PROCEDURE PA_InsertarTipoSolicitudARequerimientoProveedor
    @PN_IdTipoSolicitud INT,
    @PN_IdRequerimientoProveedor INT
AS
BEGIN
    BEGIN TRY
        -- Iniciar la transacción
        BEGIN TRANSACTION;

        -- Insertar el tipo de solicitud a la tabla de requerimiento proveedor
        INSERT INTO TSOLITEL_TipoSolicitud_RequerimientoProveedor (TN_IdTipoSolicitud, TN_IdRequerimientoProveedor)
        VALUES (@PN_IdTipoSolicitud, @PN_IdRequerimientoProveedor);

        -- Confirmar la transacción
        COMMIT TRANSACTION;
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
GO
