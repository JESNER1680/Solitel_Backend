CREATE OR ALTER PROCEDURE PA_InsertarHistoricoSolicitud
    @PN_IdSolicitudProveedor INT,
    @PN_IdSolicitudAnalisis INT,
    @PN_IdUsuario INT,
    @PC_Observacion VARCHAR(255),
    @PN_IdEstado INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @PN_IdSolicitudProveedor <> 0
        BEGIN
            INSERT INTO TSOLITEL_Historial (TC_Observacion, TF_FechaDeModificacion, TN_IdUsuario, TN_IdEstado, TN_IdAnalisis, TN_IdSolicitud)
            VALUES (@PC_Observacion, GETDATE(), @PN_IdUsuario, @PN_IdEstado, 0, @PN_IdSolicitudProveedor);
        END
        ELSE IF @PN_IdSolicitudProveedor = 0 AND @PN_IdSolicitudAnalisis <> 0
        BEGIN
            INSERT INTO TSOLITEL_Historial (TC_Observacion, TF_FechaDeModificacion, TN_IdUsuario, TN_IdEstado, TN_IdAnalisis, TN_IdSolicitud)
            VALUES (@PC_Observacion, GETDATE(), @PN_IdUsuario, @PN_IdEstado, @PN_IdSolicitudAnalisis, 0);
        END

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
