CREATE OR ALTER PROCEDURE PA_InsetarTipoAnalisis 
    @PC_Nombre VARCHAR(50),
    @PC_Descripcion VARCHAR(255)
AS
BEGIN
    BEGIN TRY
        -- Iniciar la transacci�n
        BEGIN TRANSACTION;

        -- Insertar el nuevo tipo de an�lisis en la tabla
        INSERT INTO TSOLITEL_TipoAnalisis (TC_Nombre, TC_Descripcion)
        VALUES (@PC_Nombre, @PC_Descripcion);

        -- Confirmar la transacci�n
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, revertir la transacci�n
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
