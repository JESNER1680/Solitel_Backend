CREATE OR ALTER PROCEDURE PA_ConsultarOficina
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta las oficinas que no están marcadas como borradas
        SELECT TN_IdOficina, TC_Nombre, TC_Tipo 
        FROM TSOLITEL_Oficina
        WHERE TB_Borrado = 0;

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
