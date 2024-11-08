CREATE OR ALTER   PROCEDURE [dbo].[PA_CambiarEstadoSolicitudAnalisis]
    @TN_IdSolicitudAnalisis INT,      -- ID de la solicitud que queremos actualizar
    @TC_NombreEstado VARCHAR(50),      -- Nombre del estado que buscamos en la tabla TSOLITEL_Estado
    @TC_TipoEstado VARCHAR(50),        -- Tipo de estado que buscamos en la tabla TSOLITEL_Estado
    @TN_IdEstado INT OUTPUT            -- Parámetro de salida para devolver el ID del estado
AS
BEGIN
    -- Inicia una transacción para asegurarse de que todas las operaciones se realicen correctamente
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Busca el ID del estado basado en el nombre y tipo
        SELECT @TN_IdEstado = TN_IdEstado
        FROM [dbo].[TSOLITEL_Estado]
        WHERE TC_Nombre = @TC_NombreEstado
          AND TC_Tipo = @TC_TipoEstado;

        -- Verifica si el estado fue encontrado
        IF @TN_IdEstado IS NOT NULL
        BEGIN
            -- Actualiza la solicitud con el nuevo estado
            UPDATE [dbo].[TSOLITEL_SolicitudAnalisis]
            SET TN_IdEstado = @TN_IdEstado
            WHERE TN_IdAnalisis = @TN_IdSolicitudAnalisis;

            -- Confirma la transacción
            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            -- Deshace la transacción si no se encuentra el estado
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END
        
        -- Captura el error y lo muestra
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


