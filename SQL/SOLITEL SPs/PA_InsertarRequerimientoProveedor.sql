CREATE OR ALTER PROCEDURE PA_InsertarRequerimientoProveedor 
    @PF_FechaInicio DATE,
    @PF_FechaFinal DATE,
    @PC_Requerimiento VARCHAR(255),
    @IdRequerimientoInsertado INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO TSOLITEL_RequerimientoProveedor (TF_FechaDeInicio, TF_FechaDeFinal, TC_Requerimiento)
        VALUES (@PF_FechaInicio, @PF_FechaFinal, @PC_Requerimiento);

        SET @IdRequerimientoInsertado = SCOPE_IDENTITY();

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
