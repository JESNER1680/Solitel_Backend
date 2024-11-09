CREATE OR ALTER PROCEDURE PA_InsertarArchivoRespuestaSolicitudAnalisis
    @PC_NombreArchivo VARCHAR(255),
    @PV_Contenido VARBINARY(MAX),
    @PC_FormatoArchivo VARCHAR(20),
    @PF_FechaModificacion DATE,
    @PN_IdSolicitudAnalisis INT,
	@PC_Tipo VARCHAR(50)
AS
BEGIN
	BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdArchivoInsertado INT;

        -- Inserta el archivo en la tabla TSOLITEL_Archivo
        INSERT INTO TSOLITEL_Archivo (TC_Nombre, TV_DireccionFileStream, TC_FormatoAchivo, TF_FechaDeModificacion)
        VALUES (@PC_NombreArchivo, @PV_Contenido, @PC_FormatoArchivo, @PF_FechaModificacion);

        -- Obtiene el ID del archivo insertado
        SET @IdArchivoInsertado = SCOPE_IDENTITY();

        -- Inserta la relación entre solicitudAnalisis y archivo
        INSERT INTO TSOLITEL_SolicitudAnalisis_Archivo (TN_IdAnalisis, TN_IdArchivo, TC_Tipo)
        VALUES (@PN_IdSolicitudAnalisis, @IdArchivoInsertado, @PC_Tipo);

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
