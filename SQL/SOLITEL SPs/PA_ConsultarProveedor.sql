CREATE OR ALTER PROCEDURE PA_ConsultarProveedor
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los proveedores que no están marcados como borrados
        SELECT 
			TN_IdProveedor, 
			TC_Nombre
			
        FROM TSOLITEL_Proveedor
        WHERE TB_Borrado = 0
		ORDER BY TN_IdProveedor DESC;

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
