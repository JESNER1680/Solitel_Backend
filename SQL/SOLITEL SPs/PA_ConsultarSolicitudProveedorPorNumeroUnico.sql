CREATE OR ALTER PROCEDURE [dbo].[PA_ConsultarSolicitudProveedorPorNumeroUnico] 
@PN_NumeroUnico VARCHAR(100)
AS
BEGIN
	BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta las solicitudes del proveedor basadas en el número único y el estado 'Tramitado'
        SELECT TOP 1
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

        WHERE T.TC_NumeroUnico = @PN_NumeroUnico 
        
        ORDER BY T.TF_FechaDeCreacion ASC; -- Trae el registro mas viejo

        -- Confirma la transacción si no hay errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
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
