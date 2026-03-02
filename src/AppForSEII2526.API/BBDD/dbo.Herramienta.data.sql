

-- --------------------------------------------------
-- Insertar Herramientas
-- --------------------------------------------------

-- Habilitar la inserción de IDs manualmente para Herramienta
SET IDENTITY_INSERT [dbo].[Herramienta] ON;

-- Insertar las 15 herramientas
INSERT INTO [dbo].[Herramienta] (
    [Id], 
    [TiempoReparacion], 
    [Nombre], 
    [Material], 
    [Precio], 
    [FabricanteId]
)
VALUES 
    (1, N'30', N'Llave inglesa', N'Acero', 150, 1),
    (2, N'15', N'Martillo de uña', N'Acero y Goma', 90, 2),
    (3, N'5', N'Destornillador de estrella', N'Acero y Plástico', 45, 1),
    (4, N'10', N'Alicates universales', N'Acero', 75, 2),
    (5, N'60', N'Taladro percutor', N'Plástico y Metal', 350, 3),
    (6, N'20', N'Sierra de mano', N'Acero y Madera', 110, 1);
   
-- Deshabilitar la inserción manual de IDs
SET IDENTITY_INSERT [dbo].[Herramienta] OFF;

PRINT '6 Herramientas insertadas correctamente.';