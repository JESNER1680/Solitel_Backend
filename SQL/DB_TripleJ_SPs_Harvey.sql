CREATE OR ALTER PROCEDURE [dbo].[PA_CambiarEstadoSolicitudProveedor]
    @TN_IdSolicitudProveedor INT,      -- ID de la solicitud que queremos actualizar
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
            UPDATE [dbo].[TSOLITEL_SolicitudProveedor]
            SET TN_IdEstado = @TN_IdEstado
            WHERE TN_IdSolicitud = @TN_IdSolicitudProveedor;

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




CREATE OR ALTER PROCEDURE PA_ConsultarArchivoPorID
    @PN_IdArchivo INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta el archivo por ID
        SELECT TN_IdArchivo, 
               TC_Nombre AS TC_Nombre, 
               TV_DireccionFileStream, 
               TC_FormatoAchivo, 
               TF_FechaDeModificacion 
        FROM TSOLITEL_Archivo
        WHERE TN_IdArchivo = @PN_IdArchivo;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
GO


CREATE OR ALTER PROCEDURE PA_ConsultarArchivosDeSolicitudProveedor
    @PN_IdSolicitudProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los archivos asociados a una solicitud de proveedor
        SELECT A.TN_IdArchivo, A.TC_Nombre
        FROM TSOLITEL_SolicitudProveedor_RequerimientoProveedor SPRP
        INNER JOIN TSOLITEL_SolicitudProveedor SP
            ON SP.TN_IdSolicitud = SPRP.TN_IdSolicitud
        INNER JOIN TSOLITEL_RequerimientoProveedor_Archivo RPA
            ON RPA.TN_IdRequerimientoProveedor = SPRP.TN_IdRequerimientoProveedor
        INNER JOIN TSOLITEL_Archivo A
            ON A.TN_IdArchivo = RPA.TN_IdArchivo
        WHERE SP.TN_IdSolicitud = @PN_IdSolicitudProveedor;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
GO



CREATE OR ALTER PROCEDURE PA_ConsultarDatosRequeridos
    @PN_IdRequerimientoProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los datos requeridos asociados a un requerimiento de proveedor
        SELECT DR.TN_IdDatoRequerido, DR.TC_DatoRequerido, DR.TC_Motivacion, DR.TN_IdTipoDato 
        FROM TSOLITEL_DatoRequerido DR
        INNER JOIN TSOLITEL_RequerimientoProveedor_DatoRequerido RDR
            ON RDR.TN_IdDatoRequerido = DR.TN_IdDatoRequerido
        WHERE RDR.TN_IdRequerimientoProveedor = @PN_IdRequerimientoProveedor;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
GO



CREATE OR ALTER PROCEDURE PA_ConsultarEstados
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @Error INT;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY

		SELECT * FROM TSOLITEL_Estado
        
        COMMIT TRANSACTION;
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


CREATE OR ALTER PROCEDURE PA_ConsultarHistoricoSolicitudProveedor
    @PN_IdSolicitudProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta el historial de la solicitud de proveedor
        SELECT TN_IdHistorial, TC_Observacion, TF_FechaDeModificacion, TN_IdUsuario, TN_IdEstado, TN_IdAnalisis, TN_IdSolicitud
        FROM TSOLITEL_Historial
        WHERE TN_IdSolicitud = @PN_IdSolicitudProveedor;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
GO


CREATE OR ALTER PROCEDURE PA_ConsultarOficina
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta las oficinas que no están marcadas como borradas
        SELECT TN_IdOficina, TC_Nombre, TC_Tipo 
        FROM TSOLITEL_Oficina
        WHERE TB_Borrado = 0;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
GO



CREATE OR ALTER PROCEDURE PA_ConsultarProveedor
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los proveedores que no están marcados como borrados
        SELECT TN_IdProveedor, TC_Nombre 
        FROM TSOLITEL_Proveedor
        WHERE TB_Borrado = 0;

        -- Confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

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
GO



CREATE OR ALTER PROCEDURE PA_ConsultarRequerimientosProveedor
    @PN_IdSolicitudProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta los requerimientos del proveedor asociados a la solicitud
        SELECT RP.TN_IdRequerimientoProveedor, RP.TF_FechaDeInicio, RP.TF_FechaDeFinal, RP.TC_Requerimiento
        FROM TSOLITEL_RequerimientoProveedor RP
        INNER JOIN TSOLITEL_SolicitudProveedor_RequerimientoProveedor SPRP
            ON SPRP.TN_IdRequerimientoProveedor = RP.TN_IdRequerimientoProveedor
        WHERE SPRP.TN_IdSolicitud = @PN_IdSolicitudProveedor;

        -- Confirma la transacción si todo fue bien
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
GO



CREATE OR ALTER PROCEDURE PA_ConsultarSolicitudesProveedorPorNumeroUnico
    @PN_NumeroUnico VARCHAR(100)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta las solicitudes del proveedor basadas en el número único y el estado 'Tramitado'
        SELECT 
            TN_IdSolicitud,
            TN_NumeroUnico,
            TN_NumeroCaso,
            TC_Imputado,
            TC_Ofendido,
            TC_Resennia,
            TB_Urgente,
            TB_Aprobado,
            TF_FechaDeCrecion,
            Proveedor.TN_IdProveedor,
            Proveedor.TC_Nombre AS TC_NombreProveedor,
            Fiscalia.TN_IdFiscalia,
            Fiscalia.TC_Nombre AS TC_NombreFiscalia,
            Oficina.TN_IdOficina,
            Oficina.TC_Nombre AS TC_NombreOficina,
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
            UsuarioCreador.TN_IdUsuario AS TN_IdUsuarioCreador,
            UsuarioCreador.TC_Nombre AS TC_NombreUsuarioCreador
        FROM TSOLITEL_SolicitudProveedor AS T
        INNER JOIN TSOLITEL_Proveedor AS Proveedor ON T.TN_IdProveedor = Proveedor.TN_IdProveedor
        LEFT JOIN TSOLITEL_Fiscalia AS Fiscalia ON T.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        LEFT JOIN TSOLITEL_Oficina AS Oficina ON T.TN_IdOficina = Oficina.TN_IdOficina
        LEFT JOIN TSOLITEL_Delito AS Delito ON T.TN_IdDelito = Delito.TN_IdDelito
        LEFT JOIN TSOLITEL_CategoriaDelito AS CategoriaDelito ON T.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        LEFT JOIN TSOLITEL_Modalidad AS Modalidad ON T.TN_IdModalida = Modalidad.TN_IdModalidad
        LEFT JOIN TSOLITEL_Estado AS Estado ON T.TN_IdEstado = Estado.TN_IdEstado
        LEFT JOIN TSOLITEL_SubModalidad AS SubModalidad ON T.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
        LEFT JOIN TSOLITEL_Usuario AS UsuarioCreador ON T.TN_IdUsuario = UsuarioCreador.TN_IdUsuario
        WHERE TN_NumeroUnico = @PN_NumeroUnico 
        AND Estado.TC_Nombre = 'Tramitado'
        ORDER BY TN_IdSolicitud;

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
GO


CREATE OR ALTER PROCEDURE [dbo].[PA_ConsultarSolicitudesProveedor]
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

        -- Consulta con paginación
        SELECT 
            TN_IdSolicitud,
            TN_NumeroUnico,
            TN_NumeroCaso,
            TC_Imputado,
            TC_Ofendido,
            TC_Resennia,
            TB_Urgente,
            TB_Aprobado,
            TF_FechaDeCrecion,
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
            TN_IdUsuario
        FROM TSOLITEL_SolicitudProveedor AS T
        LEFT JOIN TSOLITEL_Proveedor AS Proveedor ON T.TN_IdProveedor = Proveedor.TN_IdProveedor
        LEFT JOIN TSOLITEL_Fiscalia AS Fiscalia ON T.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        LEFT JOIN TSOLITEL_Delito AS Delito ON T.TN_IdDelito = Delito.TN_IdDelito
        LEFT JOIN TSOLITEL_CategoriaDelito AS CategoriaDelito ON T.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        LEFT JOIN TSOLITEL_Modalidad AS Modalidad ON T.TN_IdModalida = Modalidad.TN_IdModalidad
        LEFT JOIN TSOLITEL_Estado AS Estado ON T.TN_IdEstado = Estado.TN_IdEstado
        LEFT JOIN TSOLITEL_SubModalidad AS SubModalidad ON T.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
        ORDER BY TN_IdSolicitud
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        -- Si todo está correcto, se confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si hay un error, se revierte la transacción
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



CREATE OR ALTER PROCEDURE dbo.PA_ConsultarSolicitudesProveedorPorEstado
    @pPageNumber INT,
    @pPageSize INT,
    @pIdEstado INT = NULL,
    @pNumeroUnico INT = NULL,
    @pFechaInicio DATETIME2 = NULL,
    @pFechaFin DATETIME2 = NULL,
    @pCaracterIngresado VARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- Inicia la transacción
        BEGIN TRANSACTION;

        DECLARE @Offset INT = (@pPageNumber - 1) * @pPageSize;

        -- Realiza la consulta de solicitudes con JOINs, filtros adicionales y paginación
        SELECT 
            T.TN_IdSolicitud,
            T.TN_NumeroUnico,
            T.TN_NumeroCaso,
            T.TC_Imputado,
            T.TC_Ofendido,
            T.TC_Resennia,
            T.TB_Urgente,
            T.TB_Aprobado,
            T.TF_FechaDeCrecion,
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
            Usuario.TN_IdUsuario,
			Usuario.TC_Nombre AS TC_NombreUsuario

        FROM dbo.TSOLITEL_SolicitudProveedor AS T
        LEFT JOIN dbo.TSOLITEL_Proveedor AS Proveedor ON T.TN_IdProveedor = Proveedor.TN_IdProveedor
        LEFT JOIN dbo.TSOLITEL_Fiscalia AS Fiscalia ON T.TN_IdFiscalia = Fiscalia.TN_IdFiscalia
        LEFT JOIN dbo.TSOLITEL_Delito AS Delito ON T.TN_IdDelito = Delito.TN_IdDelito
        LEFT JOIN dbo.TSOLITEL_CategoriaDelito AS CategoriaDelito ON T.TN_IdCategoriaDelito = CategoriaDelito.TN_IdCategoriaDelito
        LEFT JOIN dbo.TSOLITEL_Modalidad AS Modalidad ON T.TN_IdModalida = Modalidad.TN_IdModalidad
        LEFT JOIN dbo.TSOLITEL_Estado AS Estado ON T.TN_IdEstado = Estado.TN_IdEstado
        LEFT JOIN dbo.TSOLITEL_SubModalidad AS SubModalidad ON T.TN_IdSubModalidad = SubModalidad.TN_IdSubModalidad
		LEFT JOIN dbo.TSOLITEL_Usuario AS Usuario ON T.TN_IdUsuario = Usuario.TN_IdUsuario
        WHERE (@pIdEstado IS NULL OR T.TN_IdEstado = @pIdEstado)
          AND (@pNumeroUnico IS NULL OR T.TN_NumeroUnico = @pNumeroUnico)
          AND (@pFechaInicio IS NULL OR T.TF_FechaDeCrecion >= @pFechaInicio)
          AND (@pFechaFin IS NULL OR T.TF_FechaDeCrecion <= @pFechaFin)
          AND (@pCaracterIngresado IS NULL OR 
               T.TC_Imputado LIKE '%' + @pCaracterIngresado + '%' OR
               T.TC_Ofendido LIKE '%' + @pCaracterIngresado + '%' OR
               T.TC_Resennia LIKE '%' + @pCaracterIngresado + '%')
        ORDER BY T.TN_IdSolicitud
        OFFSET @Offset ROWS FETCH NEXT @pPageSize ROWS ONLY;

        -- Confirmar la transacción si no hubo errores
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- En caso de error, deshacer la transacción
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Manejo de errores
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT 
            @ErrorMessage = ERROR_MESSAGE(),
            @ErrorSeverity = ERROR_SEVERITY(),
            @ErrorState = ERROR_STATE();

        -- Lanzar el error para ser manejado fuera del procedimiento si es necesario
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE PA_ConsultarSolicitudesPorNumeroUnico
    @PN_NumeroUnico INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta para obtener solicitudes por número único
        SELECT 
            SP.TN_IdSolicitud, 
            SP.TN_NumeroUnico, 
            P.TC_Nombre 
        FROM TSOLITEL_SolicitudProveedor SP
        INNER JOIN TSOLITEL_Proveedor P ON P.TN_IdProveedor = SP.TN_IdProveedor
        WHERE SP.TN_NumeroUnico = @PN_NumeroUnico;

        -- Si todo está correcto, se confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si hay un error, se revierte la transacción
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



CREATE OR ALTER PROCEDURE PA_ConsultarTipoSolicitudes
    @PN_IdRequerimientoProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Consulta para obtener los tipos de solicitudes
        SELECT 
            TS.TN_IdTipoSolicitud, 
            TS.TC_Nombre, 
            TS.TC_Descripcion 
        FROM TSOLITEL_TipoSolicitud TS
        INNER JOIN TSOLITEL_TipoSolicitud_RequerimientoProveedor TSRP
            ON TS.TN_IdTipoSolicitud = TSRP.TN_IdTipoSolicitud
        WHERE TSRP.TN_IdRequerimientoProveedor = @PN_IdRequerimientoProveedor;

        -- Si todo está correcto, se confirma la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si hay un error, se revierte la transacción
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


CREATE OR ALTER PROCEDURE PA_EliminarOficina
@PN_IdOficina int
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Actualizar el estado de la oficina a borrado
        UPDATE TSOLITEL_Oficina
        SET TB_Borrado = 1
        WHERE TN_IdOficina = @PN_IdOficina;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
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


CREATE OR ALTER PROCEDURE PA_EliminarProveedor 
    @PN_IdProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Actualiza el proveedor marcándolo como borrado
        UPDATE TSOLITEL_Proveedor
        SET TB_Borrado = 1
        WHERE TN_IdProveedor = @PN_IdProveedor;

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


CREATE OR ALTER PROCEDURE PA_InsertarArchivo_RequerimientoProveedor
    @PC_NombreArchivo VARCHAR(255),
    @PV_Contenido VARBINARY(MAX),
    @PC_FormatoArchivo VARCHAR(20),
    @PF_FechaModificacion DATE,
    @PN_IdRequerimientoProveedor INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdArchivoInsertado INT;

        -- Inserta el archivo en la tabla TSOLITEL_Archivo
        INSERT INTO TSOLITEL_Archivo (TC_Nombre, TV_DireccionFileStream, TC_FormatoAchivo, TF_FechaDeModificacion)
        VALUES (@PC_NombreArchivo, @PV_Contenido, @PC_FormatoArchivo, @PF_FechaModificacion);

        -- Obtiene el ID del archivo insertado
        SET @IdArchivoInsertado = SCOPE_IDENTITY();

        -- Inserta la relación entre requerimiento y archivo
        INSERT INTO TSOLITEL_RequerimientoProveedor_Archivo (TN_IdRequerimientoProveedor, TN_IdArchivo)
        VALUES (@PN_IdRequerimientoProveedor, @IdArchivoInsertado);

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


CREATE OR ALTER PROCEDURE PA_InsertarDatoRequerido
    @PC_DatoRequerido VARCHAR(255),
    @PC_Motivacion VARCHAR(255),
    @PN_TipoDato INT,
    @PN_IdRequerimiento INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdDatoRequerido INT;

        -- Inserta el dato requerido en la tabla TSOLITEL_DatoRequerido
        INSERT INTO TSOLITEL_DatoRequerido (TC_DatoRequerido, TC_Motivacion, TN_IdTipoDato)
        VALUES (@PC_DatoRequerido, @PC_Motivacion, @PN_TipoDato);

        -- Obtiene el ID del dato requerido insertado
        SET @IdDatoRequerido = SCOPE_IDENTITY();

        -- Inserta la relación entre requerimiento y dato requerido
        INSERT INTO TSOLITEL_RequerimientoProveedor_DatoRequerido (TN_IdRequerimientoProveedor, TN_IdDatoRequerido)
        VALUES (@PN_IdRequerimiento, @IdDatoRequerido);

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


CREATE OR ALTER PROCEDURE PA_InsertarOficina
@PC_Nombre varchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar nueva oficina en la tabla
        INSERT INTO TSOLITEL_Oficina(TC_Nombre)
        VALUES(@PC_Nombre);

        -- Confirmar la transacción
        COMMIT TRANSACTION;
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


CREATE OR ALTER PROCEDURE PA_InsertarProveedor
    @PC_Nombre VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO TSOLITEL_Proveedor (TC_Nombre)
        VALUES (@PC_Nombre);

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


CREATE OR ALTER PROCEDURE PA_InsertarRequerimientoProveedor 
    @PF_FechaInicio DATE,
    @PF_FechaFinal DATE,
    @PC_Requerimiento VARCHAR(255),
    @IdRequerimientoInsertado INT OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO TSOLITEL_RequerimientoProveedor (TF_FechaDeInicio, TF_FechaDeFinal, TC_Requerimiento)
        VALUES (@PF_FechaInicio, @PF_FechaFinal, @PC_Requerimiento);

        SET @IdRequerimientoInsertado = SCOPE_IDENTITY();

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


CREATE OR ALTER PROCEDURE PA_InsertarTipoSolicitudARequerimientoProveedor
    @PN_IdTipoSolicitud INT,
    @PN_IdRequerimientoProveedor INT
AS
BEGIN
    BEGIN TRY
        -- Iniciar la transacción
        BEGIN TRANSACTION;

        -- Insertar el tipo de solicitud a la tabla de requerimiento proveedor
        INSERT INTO TSOLITEL_TipoSolicitud_RequerimientoProveedor (TN_IdTipoSolicitud, TN_IdRequerimientoProveedor)
        VALUES (@PN_IdTipoSolicitud, @PN_IdRequerimientoProveedor);

        -- Confirmar la transacción
        COMMIT TRANSACTION;
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

CREATE OR ALTER PROCEDURE PA_ListarNumerosUnicosTramitados
AS
BEGIN
    BEGIN TRY
        -- Ejecutar la consulta para listar los números únicos tramitados
        SELECT TN_NumeroUnico 
        FROM TSOLITEL_SolicitudProveedor SP
        INNER JOIN TSOLITEL_Estado E ON SP.TN_IdEstado = E.TN_IdEstado
        WHERE E.TC_Nombre = 'Tramitado'
        GROUP BY TN_NumeroUnico;
    END TRY
    BEGIN CATCH
        -- Manejar el error
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE PA_MoverEstadoSinEfectoSolicitudProveedor
@PN_IdSolicitudProveedor int
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @IdEstado int;

        -- Cambiar el estado de la solicitud a 'Sin Efecto'
        EXEC PA_CambiarEstadoSolicitudProveedor @PN_IdSolicitudProveedor, 'Sin Efecto', 'Proveedor', @TN_IdEstado = @IdEstado OUTPUT;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
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


CREATE OR ALTER PROCEDURE PA_RelacionarRequerimientosProveedor
@PN_IdSolicitudProveedor int,
@PN_IdRequerimientoProveedor int
AS
BEGIN
    SET NOCOUNT ON;

    -- Iniciar la transacción
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Insertar la relación entre solicitud y requerimiento
        INSERT INTO TSOLITEL_SolicitudProveedor_RequerimientoProveedor 
        (TN_IdSolicitud, TN_IdRequerimientoProveedor)
        VALUES (@PN_IdSolicitudProveedor, @PN_IdRequerimientoProveedor);

        -- Confirmar la transacción
        COMMIT TRANSACTION;
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

