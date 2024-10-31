CREATE OR ALTER PROCEDURE PA_InsertarSolicitudProveedor
    @PN_NumeroUnico        VARCHAR(100),  -- Número único de la solicitud
    @PN_NumeroCaso         VARCHAR(100),  -- Número de caso
    @PC_Imputado           VARCHAR(150),  -- Nombre del imputado
    @PC_Ofendido           VARCHAR(150),  -- Nombre del ofendido
    @PC_Resennia           VARCHAR(255),  -- Descripción o reseña de la solicitud
    @PB_Urgente            BIT,           -- Indicador si es urgente
    @PB_Aprobado           BIT,           -- Indicador si está aprobado
    @PF_FechaCreacion      DATE,          -- Fecha de creación
    @PN_IdUsuarioCreador   INT,           -- ID del usuario que creó la solicitud
    @PN_IdDelito           INT,           -- ID del delito
    @PN_IdCategoriaDelito  INT,           -- ID de la categoría del delito
    @PN_IdModalidad        INT,           -- ID de la modalidad
    @PN_IdSubModalidad     INT,           -- ID de la submodalidad
    @PN_IdEstado           INT,           -- ID del estado
    @PN_IdProveedor        INT,           -- ID del proveedor
    @PN_IdFiscalia         INT,           -- ID de la fiscalía
    @PN_IdOficina          INT,           -- ID de la oficina
    @IdSolicitudInsertada  INT OUTPUT
AS
BEGIN
    BEGIN TRY
        -- Iniciar la transacción
        BEGIN TRANSACTION;

        DECLARE @ResultadoIdEstado INT;

        -- Insertar nueva solicitud en la tabla
        INSERT INTO dbo.TSOLITEL_SolicitudProveedor
        (
            TN_NumeroUnico, 
            TN_NumeroCaso, 
            TC_Imputado, 
            TC_Ofendido, 
            TC_Resennia, 
            TB_Urgente, 
            TB_Aprobado, 
            TF_FechaDeCrecion, 
            TN_IdUsuario, 
            TN_IdDelito, 
            TN_IdCategoriaDelito, 
            TN_IdModalida, 
            TN_IdSubModalidad, 
            TN_IdEstado, 
            TN_IdProveedor, 
            TN_IdFiscalia, 
            TN_IdOficina
        )
        VALUES
        (
            @PN_NumeroUnico, 
            @PN_NumeroCaso, 
            @PC_Imputado, 
            @PC_Ofendido, 
            @PC_Resennia, 
            @PB_Urgente, 
            @PB_Aprobado, 
            @PF_FechaCreacion, 
            @PN_IdUsuarioCreador, 
            @PN_IdDelito, 
            @PN_IdCategoriaDelito, 
            @PN_IdModalidad, 
            @PN_IdSubModalidad, 
            @PN_IdEstado, 
            @PN_IdProveedor, 
            @PN_IdFiscalia, 
            @PN_IdOficina
        );

        -- Capturar el ID de la solicitud insertada
        SET @IdSolicitudInsertada = SCOPE_IDENTITY();

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        -- Cambiar el estado de la solicitud
        EXEC PA_CambiarEstadoSolicitudProveedor @IdSolicitudInsertada, 'Creado', 'Proveedor', @TN_IdEstado = @ResultadoIdEstado OUTPUT;

        -- Insertar en el historial de la solicitud
        EXEC PA_InsertarHistoricoSolicitud @IdSolicitudInsertada, 0, @PN_IdUsuarioCreador, '', @ResultadoIdEstado;

    END TRY
    BEGIN CATCH
        -- Si ocurre un error, revertir la transacción
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Manejar el error
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO