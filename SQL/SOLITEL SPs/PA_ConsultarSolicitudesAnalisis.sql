CREATE OR ALTER PROCEDURE [dbo].[PA_ConsultarSolicitudesAnalisis]
	@pTN_IdSolicitud INT = NULL,
	@pTN_IdEstado INT = NULL,
    @pTF_FechaInicio DATETIME2 = NULL,
    @pTF_FechaFin DATETIME2 = NULL,
    @pTC_NumeroUnico VARCHAR(100) = NULL,
	@pTN_IdOficina INT = NULL,
	@pTN_IdUsuario INT = NULL
AS
BEGIN
    BEGIN TRY

        SELECT 
            SOLI.TN_IdAnalisis,
            TF_FechaDeHecho,
            TC_OtrosDetalles,
            TC_OtrosObjetivosDeAnalisis,
            TF_FechaDeCreacion,
            TB_Aprobado,
            ES.TN_IdEstado,
			ES.TC_Nombre AS TC_NombreEstado,
            TN_IdOficinaSolicitante

        FROM 
            [Proyecto_Analisis].[dbo].[TSOLITEL_SolicitudAnalisis] AS SOLI
			INNER JOIN TSOLITEL_Estado AS ES ON ES.TN_IdEstado = SOLI.TN_IdEstado
			LEFT JOIN (
                    SELECT TN_IdAnalisis, MAX(TF_FechaDeModificacion) AS UltimaFechaDeModificacion
                    FROM TSOLITEL_Historial
                    GROUP BY TN_IdAnalisis
                    ) AS UltimoHistorial
			ON SOLI.TN_IdAnalisis = UltimoHistorial.TN_IdAnalisis

		WHERE (@pTN_IdEstado IS NULL OR SOLI.TN_IdEstado = @pTN_IdEstado)-- SE NECESITAN SOLO LAS SOLICITUDES EN EL ESTADO ESPECIFICO
          AND (@pTF_FechaInicio IS NULL OR SOLI.TF_FechaDeCreacion >= @pTF_FechaInicio)-- FILTRO POR INTERVALO DE FECHA
          AND (@pTF_FechaFin IS NULL OR SOLI.TF_FechaDeCreacion <= @pTF_FechaFin)-- FILTRO POR INTERVALO DE FECHA 
		  AND (@pTN_IdOficina IS NULL OR SOLI.TN_IdOficinaCreacion = @pTN_IdOficina)-- FILTRO POR OFICINA
		  AND (@pTN_IdUsuario IS NULL OR SOLI.TN_IdUsuario = @pTN_IdUsuario)-- FILTRO POR USUARIO
		  AND (@pTN_IdSolicitud IS NULL OR SOLI.TN_IdAnalisis = @pTN_IdSolicitud)-- FILTRO POR NOMBRE
		  AND (@pTC_NumeroUnico IS NULL OR EXISTS (
			SELECT 1
			FROM TSOLITEL_SolicitudAnalisis_SolicitudProveedor AS SA_SP
			INNER JOIN TSOLITEL_SolicitudProveedor AS SP ON SP.TN_IdSolicitud = SA_SP.TN_IdSolicitud
			WHERE SA_SP.TN_IdAnalisis = SOLI.TN_IdAnalisis
			AND SP.TC_NumeroUnico = @pTC_NumeroUnico
		  ))-- FILTRO POR NUMERO UNICO
        ORDER BY 
			UltimoHistorial.UltimaFechaDeModificacion DESC;

    END TRY
    BEGIN CATCH

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
