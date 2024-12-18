CREATE OR ALTER PROCEDURE [dbo].[PA_ConsultarOficina]
	@pTN_IdOficina INT = NULL,
	@pTC_Tipo VARCHAR(100) = NULL
AS
BEGIN
    BEGIN TRY

        -- Consulta las oficinas que no están marcadas como borradas
        SELECT 
			TN_IdOficina, 
			TC_Nombre, 
			TC_Tipo 
        
		FROM TSOLITEL_Oficina
		WHERE TB_Borrado = 0 
		AND (@pTN_IdOficina IS NULL OR TN_IdOficina = @pTN_IdOficina)
		AND (@pTC_Tipo IS NULL OR TC_Tipo = @pTC_Tipo)
		ORDER BY TN_IdOficina DESC;

    END TRY
    BEGIN CATCH

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
