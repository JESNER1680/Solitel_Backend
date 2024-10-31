CREATE OR ALTER PROCEDURE PA_ConsultarArchivoPorID
    @PN_IdArchivo INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta el archivo por ID
        SELECT TN_IdArchivo, 
               TC_NombreArchivo AS TC_Nombre, 
               TV_Contenido, 
               TC_FormatoArchivo, 
               TF_FechaModificacion 
        FROM TSOLITEL_Archivo
        WHERE TN_IdArchivo = @PN_IdArchivo;

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
