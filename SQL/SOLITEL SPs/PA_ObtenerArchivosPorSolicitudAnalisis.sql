CREATE OR ALTER   PROCEDURE [dbo].[PA_ObtenerArchivosPorSolicitudAnalisis]
@PN_IdAnalisis INT,
@PC_Tipo varchar(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT 
            A.TN_IdArchivo,
            A.TC_Nombre,
            A.TC_FormatoAchivo AS TC_FormatoArchivo,
            A.TV_DireccionFileStream AS TV_Contenido,
            A.TF_FechaDeModificacion AS TF_FechaModificacion
        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_Archivo] A
        INNER JOIN 
            [Proyecto_Analisis].[dbo].[TSOLITEL_SolicitudAnalisis_Archivo] SAA
        ON 
            A.TN_IdArchivo = SAA.TN_IdArchivo
        WHERE 
            SAA.TN_IdAnalisis = @PN_IdAnalisis and SAA.TC_Tipo = @PC_Tipo
        ORDER BY 
            A.TN_IdArchivo ASC;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
