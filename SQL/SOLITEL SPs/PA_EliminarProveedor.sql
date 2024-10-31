CREATE OR ALTER PROCEDURE PA_EliminarProveedor 
    @PN_IdProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Actualiza el proveedor marcándolo como borrado
        UPDATE TSOLITEL_Proveedor
        SET TB_Borrado = 1
        WHERE TN_IdProveedor = @PN_IdProveedor;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Revierte la transacción si hay un error
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
