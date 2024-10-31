USE [Proyecto_Analisis]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Autor:                Jesner Melgara
-- Fecha de creación:    2024-10-16
-- Descripción:          Consulta solicitudes de proveedor por estado con filtros adicionales y paginación.
-- =============================================

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
