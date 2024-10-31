CREATE OR ALTER PROCEDURE PA_ConsultarDatosRequeridos
    @PN_IdRequerimientoProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los datos requeridos asociados a un requerimiento de proveedor
        SELECT DR.TN_IdDatoRequerido, DR.TC_DatoRequerido, DR.TC_Motivacion, DR.TN_IdTipoDato 
        FROM TSOLITEL_DatoRequerido DR
        INNER JOIN TSOLITEL_RequerimientoProveedor_DatoRequerido RDR
            ON RDR.TN_IdDatoRequerido = DR.TN_IdDatoRequerido
        WHERE RDR.TN_IdRequerimientoProveedor = @PN_IdRequerimientoProveedor;

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
