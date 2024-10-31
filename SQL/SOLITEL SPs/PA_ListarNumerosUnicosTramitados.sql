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
