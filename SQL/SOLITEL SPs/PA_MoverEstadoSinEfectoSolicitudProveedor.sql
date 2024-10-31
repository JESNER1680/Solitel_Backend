CREATE OR ALTER PROCEDURE PA_MoverEstadoSinEfectoSolicitudProveedor
@PN_IdSolicitudProveedor int
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @IdEstado int;

        -- Cambiar el estado de la solicitud a 'Sin Efecto'
        EXEC PA_CambiarEstadoSolicitudProveedor @PN_IdSolicitudProveedor, 'Sin Efecto', 'Proveedor', @TN_IdEstado = @IdEstado OUTPUT;

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
