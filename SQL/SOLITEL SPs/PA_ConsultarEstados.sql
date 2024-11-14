CREATE OR ALTER PROCEDURE PA_ConsultarEstados
	@pTN_IdUsuario INT = NULL,
	@pTN_IdOficina INT = NULL
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @Error INT;

    BEGIN TRY

		SELECT 
			EST.TN_IdEstado,
			EST.TC_Nombre,
			EST.TC_Descripcion,
			EST.TC_Tipo,
			(
				SELECT COUNT(*) 
				FROM TSOLITEL_SolicitudProveedor AS SP
				WHERE SP.TN_IdEstado = EST.TN_IdEstado
				AND (@pTN_IdUsuario IS NULL OR SP.TN_IdUsuario = @pTN_IdUsuario)
				AND (@pTN_IdOficina IS NULL OR SP.TN_IdOficina = @pTN_IdOficina)
			) +
			(
				SELECT COUNT(*) 
				FROM TSOLITEL_SolicitudAnalisis AS SA
				LEFT JOIN TSOLITEL_Asignacion AS ASIG ON SA.TN_IdAnalisis = ASIG.TN_IdAnalisis
				WHERE SA.TN_IdEstado = EST.TN_IdEstado
				AND (@pTN_IdUsuario IS NULL OR SA.TN_IdUsuario = @pTN_IdUsuario OR ASIG.TN_IdUsuario = @pTN_IdUsuario)
				AND (@pTN_IdOficina IS NULL OR SA.TN_IdOficinaCreacion = @pTN_IdOficina OR SA.TN_IdOficinaSolicitante = @pTN_IdOficina)
			) AS TN_CantidadDeSolicitudes
		FROM TSOLITEL_Estado AS EST
		ORDER BY EST.TN_IdEstado ASC;

    END TRY
    BEGIN CATCH
      
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
