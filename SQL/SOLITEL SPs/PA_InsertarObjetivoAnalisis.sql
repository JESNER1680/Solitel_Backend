CREATE OR ALTER PROCEDURE PA_InsertarObjetivoAnalisis
    @PC_Nombre VARCHAR(50),
    @PC_Descripcion VARCHAR(255)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO TSOLITEL_ObjetivoAnalisis (TC_Nombre, TC_Descripcion)
        VALUES (@PC_Nombre, @PC_Descripcion);

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
