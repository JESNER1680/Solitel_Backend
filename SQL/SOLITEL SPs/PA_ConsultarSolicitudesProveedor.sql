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
		
		WITH SolicitudesProveedorFiltradas AS 
		(
			SELECT 
				[TN_IdSolicitud]
				,[TC_NumeroUnico]
				,[TC_NumeroCaso]
				,[TC_Imputado]
				,[TC_Ofendido]
				,[TC_Resennia]
				,[TB_Urgente]
				,[TB_Aprobado]
				,[TF_FechaDeCreacion]
				,[TN_IdDelito]
				,[TN_IdCategoriaDelito]
				,[TN_IdModalida]
				,[TN_IdSubModalidad]
				,[TN_IdEstado]
				,[TN_IdProveedor]
				,[TN_IdFiscalia]
				,[TN_IdOficina]
				,[TN_IdUsuario]

			FROM TSOLITEL_SolicitudProveedor AS SP
			WHERE (@pTN_IdEstado IS NULL OR SP.TN_IdEstado = @pTN_IdEstado)-- SE NECESITAN SOLO LAS SOLICITUDES EN EL ESTADO ESPECIFICO
			  AND (@pTF_FechaInicio IS NULL OR SP.TF_FechaDeCreacion >= @pTF_FechaInicio)-- FILTRO POR INTERVALO DE FECHA
			  AND (@pTF_FechaFin IS NULL OR SP.TF_FechaDeCreacion <= @pTF_FechaFin)-- FILTRO POR INTERVALO DE FECHA 
			  AND (@pTC_NumeroUnico IS NULL OR SP.TC_NumeroUnico = @pTC_NumeroUnico)-- FILTRO POR NUMERO UNICO
			  AND (@pTN_IdOficina IS NULL OR SP.TN_IdOficina = @pTN_IdOficina)-- FILTRO POR OFICINA
			  AND (@pTN_IdUsuario IS NULL OR SP.TN_IdUsuario = @pTN_IdUsuario)-- FILTRO POR USUARIO
			  AND (@pTN_IdSolicitud IS NULL OR SP.TN_IdSolicitud = @pTN_IdSolicitud)-- FILTRO POR NOMBRE
		)-- SUB CONSULTA PARA OPTIMISAR LOS JOINS
        SELECT 
            SPF.TN_IdSolicitud,
            SPF.TC_NumeroUnico,
            SPF.TC_NumeroCaso,
            SPF.TC_Imputado,
            SPF.TC_Ofendido,
            SPF.TC_Resennia,
            SPF.TB_Urgente,
            SPF.TB_Aprobado,
            SPF.TF_FechaDeCreacion,
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
			SPF.TN_IdSolicitud AS TN_NumeroSolicitud
            
        FROM SolicitudesProveedorFiltradas AS SPF
        INNER JOIN TSOLITEL_Proveedor AS Proveedor ON SPF.TN_IdProveedor = Proveedor.TN_IdProveedor
        INNER JOIN TSOLITEL_Fiscalia AS Fiscalia ON SPF.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        INNER JOIN TSOLITEL_Delito AS Delito ON SPF.TN_IdDelito = Delito.TN_IdDelito
        INNER JOIN TSOLITEL_CategoriaDelito AS CategoriaDelito ON SPF.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        LEFT JOIN TSOLITEL_Modalidad AS Modalidad ON SPF.TN_IdModalida = Modalidad.TN_IdModalidad
        INNER JOIN TSOLITEL_Estado AS Estado ON SPF.TN_IdEstado = Estado.TN_IdEstado
        LEFT JOIN TSOLITEL_SubModalidad AS SubModalidad ON SPF.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
		INNER JOIN TSOLITEL_Usuario AS Usuario ON SPF.TN_IdUsuario = Usuario.TN_IdUsuario
		INNER JOIN TSOLITEL_Oficina AS Oficina ON SPF.TN_IdOficina = Oficina.TN_IdOficina
		LEFT JOIN (
			SELECT 
				TN_IdSolicitud AS TN_IdSoli, 
				MAX(TF_FechaDeModificacion) AS UltimaFechaDeModificacion 
			FROM TSOLITEL_Historial GROUP BY TN_IdSolicitud
		) AS UltimoHistorial ON SPF.TN_IdSolicitud = UltimoHistorial.TN_IdSoli --SUB CONSULTA PARA OPTENER LA ULTIMA MODIFICACION
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