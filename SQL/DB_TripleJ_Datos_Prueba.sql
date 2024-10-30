-- Datos de prueba
USE [Proyecto_Analisis]
GO
INSERT INTO [dbo].[TSOLITEL_Estado]
           ([TC_Nombre]
           ,[TC_Descripcion]
           ,[TC_Tipo])
     VALUES
           ('Sin Efecto'
           ,'Una solicitud no tramitada deja de ser valida o necesaria'
           ,'Proveedor'),
		   ('Legajo'
           ,'Una solicitud tramitada deja de ser valida o necesaria'
           ,'Proveedor'),
		   ('Creado'
           ,'Una solicitud es aprobada por la jefatura correspondiente'
           ,'Proveedor'),
		   ('Pendiente'
           ,'Una solicitud es creada pero esta pendiente de aprobacion'
           ,'Proveedor'),
		   ('Tramitado'
           ,'Una solicitud fue espera ser respondida por un provedor de telecomunicaciones'
           ,'Proveedor'),
		   ('Finalizado'
           ,'Una solicitud tramitada que la fue analizada por el investigador o fiscal solicitante'
           ,'Proveedor'),
		   ('Solicitado'
           ,''
           ,'Proveedor')

INSERT INTO [dbo].[TSOLITEL_Estado]
           ([TC_Nombre]
           ,[TC_Descripcion]
           ,[TC_Tipo])
     VALUES
           ('Sin Efecto'
           ,'Una solicitud de analisis no tramitada deja de ser valida o necesaria'
           ,'Analisis'),
		   ('Legajo'
           ,'Una solicitud de analisis tramitada deja de ser valida o necesaria'
           ,'Analisis'),
		   ('Finalizado'
           ,'Una solicitud de analisis respondida ya fue estudiada por el fiscal o investigador'
           ,'Analisis'),
		   ('Aprobar Analisis'
           ,'Una solicitud debe ser aprobada por la jefatura correspondiente'
           ,'Analisis'),
		   ('En Analisis'
           ,'Una solicitud es aprobada por la jefatura correspondiente'
           ,'Analisis'),
		   ('Analizando'
           ,'Una solicitud esta en proceso de ser respondida por los analistas'
           ,'Analisis')



