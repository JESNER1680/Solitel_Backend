CREATE OR ALTER PROCEDURE PA_EliminarTipoAnalisis
    @PN_IdTipoAnalisis INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Actualiza el tipo de análisis marcándolo como borrado
        UPDATE TSOLITEL_TipoAnalisis
        SET TB_Borrado = 1
        WHERE TN_IdTipoAnalisis = @PN_IdTipoAnalisis;

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
