-- =============================================
-- Author:		Harvey
-- Create date: 10/15/2024
-- Description:	Procedimiento Almacedado para consultar todas las solicitudes de proveedores en un estado concreto
-- =============================================
-- Revisado
CREATE OR ALTER PROCEDURE [dbo].[PA_ConsultarSolicitudesProveedor]
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
            TN_IdSolicitud,
            TC_NumeroUnico,
            TC_NumeroCaso,
            TC_Imputado,
            TC_Ofendido,
            TC_Resennia,
            TB_Urgente,
            TB_Aprobado,
            TF_FechaDeCreacion AS TF_FechaDeCreacion,
            Usuario.TN_IdUsuario,
			Usuario.TC_Nombre AS TC_NombreUsuario, 
			Usuario.TC_Apellido AS TC_ApellidoUsuario,
			Proveedor.TN_IdProveedor,
            Proveedor.TC_Nombre AS TC_NombreProveedor,
            Fiscalia.TN_IdFiscalia,
            Fiscalia.TC_Nombre AS TC_NombreFiscalia,
            Delito.TN_IdDelito AS TN_IdDelito,
            Delito.TC_Nombre AS TC_NombreDelito,
            CategoriaDelito.TN_IdCategoriaDelito,
            CategoriaDelito.TC_Nombre AS TC_NombreCategoriaDelito,
            Modalidad.TN_IdModalidad AS TN_IdModalidad,
            Modalidad.TC_Nombre AS TC_NombreModalidad,
            Estado.TN_IdEstado,
            Estado.TC_Nombre AS TC_NombreEstado,
            SubModalidad.TN_IdSubModalidad,
            SubModalidad.TC_Nombre AS TC_NombreSubModalidad,
			Oficina.TN_IdOficina,
			Oficina.TC_Nombre AS TC_NombreOficina,
			TN_IdSolicitud AS TN_NumeroSolicitud
            
        FROM TSOLITEL_SolicitudProveedor AS T
        INNER JOIN TSOLITEL_Proveedor AS Proveedor ON T.TN_IdProveedor = Proveedor.TN_IdProveedor
        INNER JOIN TSOLITEL_Fiscalia AS Fiscalia ON T.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        INNER JOIN TSOLITEL_Delito AS Delito ON T.TN_IdDelito = Delito.TN_IdDelito
        INNER JOIN TSOLITEL_CategoriaDelito AS CategoriaDelito ON T.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        LEFT JOIN TSOLITEL_Modalidad AS Modalidad ON T.TN_IdModalida = Modalidad.TN_IdModalidad
        INNER JOIN TSOLITEL_Estado AS Estado ON T.TN_IdEstado = Estado.TN_IdEstado
        LEFT JOIN TSOLITEL_SubModalidad AS SubModalidad ON T.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
		INNER JOIN TSOLITEL_Usuario AS Usuario ON T.TN_IdUsuario = Usuario.TN_IdUsuario
		INNER JOIN TSOLITEL_Oficina AS Oficina ON T.TN_IdOficina = Oficina.TN_IdOficina
		LEFT JOIN (
			SELECT 
				TN_IdSolicitud AS TN_IdSoli, 
				MAX(TF_FechaDeModificacion) AS UltimaFechaDeModificacion 
			FROM TSOLITEL_Historial GROUP BY TN_IdSolicitud
		) AS UltimoHistorial ON T.TN_IdSolicitud = UltimoHistorial.TN_IdSoli --SUB CONSULTA PARA OPTENER LA ULTIMA MODIFICACION
        WHERE (@pTN_IdEstado IS NULL OR Estado.TN_IdEstado = @pTN_IdEstado)-- SE NECESITAN SOLO LAS SOLICITUDES EN EL ESTADO ESPECIFICO
          AND (@pTF_FechaInicio IS NULL OR T.TF_FechaDeCreacion >= @pTF_FechaInicio)-- FILTRO POR INTERVALO DE FECHA
          AND (@pTF_FechaFin IS NULL OR T.TF_FechaDeCreacion <= @pTF_FechaFin)-- FILTRO POR INTERVALO DE FECHA 
          AND (@pTC_NumeroUnico IS NULL OR T.TC_NumeroUnico = @pTC_NumeroUnico)-- FILTRO POR NUMERO UNICO
		  AND (@pTN_IdOficina IS NULL OR T.TN_IdOficina = @pTN_IdOficina)-- FILTRO POR OFICINA
		  AND (@pTN_IdUsuario IS NULL OR T.TN_IdUsuario = @pTN_IdUsuario)-- FILTRO POR USUARIO
		  AND (@pTN_IdSolicitud IS NULL OR @pTN_IdSolicitud = TN_IdSolicitud)-- FILTRO POR NOMBRE
		ORDER BY UltimoHistorial.UltimaFechaDeModificacion DESC;-- ORDENAMIENTO POR ULTIMA MODIFICACION DESENDENTE

    END TRY
    BEGIN CATCH

        -- CAPTURA Y LAZA ERROR EN CASO DE OCURRIR
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