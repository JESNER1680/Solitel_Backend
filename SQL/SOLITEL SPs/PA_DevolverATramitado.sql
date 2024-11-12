CREATE OR ALTER PROCEDURE PA_DevolverATramitado
	@pTN_IdSolicitud INT,
	@pTN_IdUsuario INT,
	@pTC_Observacion VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
    DECLARE @IdEstado int;

	DECLARE @IdEstadoPendiente int = (SELECT TN_IdEstado FROM TSOLITEL_Estado WHERE TC_Nombre = 'Pendiente' AND TC_Tipo = 'Proveedor');
	DECLARE @IdEstadoCreado int = (SELECT TN_IdEstado FROM TSOLITEL_Estado WHERE TC_Nombre = 'Creado' AND TC_Tipo = 'Proveedor');


	IF EXISTS (
		SELECT sp.TN_IdSolicitud
		FROM TSOLITEL_SolicitudProveedor sp
			INNER JOIN (
				SELECT TN_IdSolicitud, TN_IdEstado, ROW_NUMBER() OVER (PARTITION BY TN_IdSolicitud ORDER BY TN_IdHistorial DESC) AS RowNum
				FROM TSOLITEL_Historial
			) ph 
			ON sp.TN_IdSolicitud = ph.TN_IdSolicitud
		WHERE ph.RowNum = 2 -- penultimo historil pero sin tener en cuenta su fecha de creacion
			AND ph.TN_IdEstado = @IdEstadoPendiente
			AND sp.TN_IdSolicitud = @pTN_IdSolicitud
	)
	BEGIN
		EXEC PA_CambiarEstadoSolicitudProveedor @pTN_IdSolicitud, 'Pendiente', 'Proveedor', @TN_IdEstado = @IdEstado OUTPUT;
		EXEC [PA_InsertarHistoricoSolicitud] @pTN_IdSolicitud, NULL, @pTN_IdUsuario, @pTC_Observacion, @IdEstado;
	END

	ELSE IF EXISTS(
		SELECT sp.TN_IdSolicitud
		FROM TSOLITEL_SolicitudProveedor sp
			INNER JOIN (
				SELECT TN_IdSolicitud, TN_IdEstado, ROW_NUMBER() OVER (PARTITION BY TN_IdSolicitud ORDER BY TN_IdHistorial DESC) AS RowNum
				FROM TSOLITEL_Historial
			) ph 
			ON sp.TN_IdSolicitud = ph.TN_IdSolicitud
		WHERE ph.RowNum = 2 -- penultimo historil pero sin tener en cuenta su fecha de creacion
			AND ph.TN_IdEstado = @IdEstadoCreado
			AND sp.TN_IdSolicitud = @pTN_IdSolicitud
	)
	BEGIN
		EXEC PA_CambiarEstadoSolicitudProveedor @pTN_IdSolicitud, 'Creado', 'Proveedor', @TN_IdEstado = @IdEstado OUTPUT;
		EXEC [PA_InsertarHistoricoSolicitud] @pTN_IdSolicitud, NULL, @pTN_IdUsuario, @pTC_Observacion, @IdEstado;
	END

	ELSE
	BEGIN 
		EXEC PA_CambiarEstadoSolicitudProveedor @pTN_IdSolicitud, 'Tramitado', 'Proveedor', @TN_IdEstado = @IdEstado OUTPUT;
		EXEC [PA_InsertarHistoricoSolicitud] @pTN_IdSolicitud, NULL, @pTN_IdUsuario, @pTC_Observacion, @IdEstado;
	END


	
    END TRY
    BEGIN CATCH

        -- En caso de error, hacer rollback
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Lanzar el error de SQL Server
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