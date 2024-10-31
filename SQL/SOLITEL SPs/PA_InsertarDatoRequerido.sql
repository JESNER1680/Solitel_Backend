CREATE OR ALTER PROCEDURE PA_InsertarDatoRequerido
    @PC_DatoRequerido VARCHAR(255),
    @PC_Motivacion VARCHAR(255),
    @PN_TipoDato INT,
    @PN_IdRequerimiento INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdDatoRequerido INT;

        -- Inserta el dato requerido en la tabla TSOLITEL_DatoRequerido
        INSERT INTO TSOLITEL_DatoRequerido (TC_DatoRequerido, TC_Motivacion, TN_IdTipoDato)
        VALUES (@PC_DatoRequerido, @PC_Motivacion, @PN_TipoDato);

        -- Obtiene el ID del dato requerido insertado
        SET @IdDatoRequerido = SCOPE_IDENTITY();

        -- Inserta la relación entre requerimiento y dato requerido
        INSERT INTO TSOLITEL_RequerimientoProveedor_DatoRequerido (TN_IdRequerimientoProveedor, TN_IdDatoRequerido)
        VALUES (@PN_IdRequerimiento, @IdDatoRequerido);

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
