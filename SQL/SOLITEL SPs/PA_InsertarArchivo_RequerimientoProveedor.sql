USE PruebasFileStream
GO

CREATE OR ALTER PROCEDURE PA_InsertarArchivo_RequerimientoProveedor
    @PC_NombreArchivo VARCHAR(255),
    @PV_Contenido VARBINARY(MAX),
    @PC_FormatoArchivo VARCHAR(20),
    @PF_FechaModificacion DATE
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdArchivoInsertado INT;

        -- Inserta el archivo en la tabla TSOLITEL_Archivo
        INSERT INTO TSOLITEL_Archivo (TC_HashArchivo, TC_NombreArchivo, TV_Contenido, TC_FormatoArchivo, TF_FechaModificacion)
        VALUES (@PC_NombreArchivo, @PV_Contenido, @PC_FormatoArchivo, @PF_FechaModificacion);


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
