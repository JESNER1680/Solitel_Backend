CREATE OR ALTER PROCEDURE PA_InsertarOficina
@PC_Nombre varchar(100),
@PC_Tipo varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar nueva oficina en la tabla
        INSERT INTO TSOLITEL_Oficina(TC_Nombre, TC_Tipo, TB_Borrado)
        VALUES(@PC_Nombre, @PC_Tipo, 0);

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
