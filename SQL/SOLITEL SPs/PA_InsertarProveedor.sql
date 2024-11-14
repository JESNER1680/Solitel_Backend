CREATE OR ALTER PROCEDURE PA_InsertarProveedor
    @pTN_IdProveedor INT OUTPUT,   -- Parámetro de salida para el ID generado
    @pTC_Nombre VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        -- Iniciar la transacción
        BEGIN TRANSACTION;

        -- Insertar el proveedor
        INSERT INTO TSOLITEL_Proveedor (TC_Nombre)
        VALUES (@pTC_Nombre);

        -- Obtener el ID generado para el proveedor
        SET @pTN_IdProveedor = SCOPE_IDENTITY();

        -- Si todo es exitoso, hacer commit
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, hacer rollback
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
