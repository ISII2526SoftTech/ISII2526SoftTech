-- --------------------------------------------------
-- 1. Insertar Fabricantes
-- --------------------------------------------------

-- Habilitar la inserción de IDs manualmente para Fabricante
SET IDENTITY_INSERT [dbo].[Fabricante] ON;

-- Insertar los 3 fabricantes
INSERT INTO [dbo].[Fabricante] (
    [Id], 
    [Nombre] 
)
VALUES 
    (1, N'BOSCH'),
    (2, N'BISERMA'),
    (3, N'MANOLO94');

-- Deshabilitar la inserción manual de IDs
SET IDENTITY_INSERT [dbo].[Fabricante] OFF;

PRINT '3 Fabricantes insertados correctamente.';

-- --------------------------------------------------
-- 2. Insertar Herramientas
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
    (6, N'20', N'Sierra de mano', N'Acero y Madera', 110, 1),
    (7, N'60', N'Lijadora eléctrica', N'Plástico y Metal', 400, 3),
    (8, N'25', N'Cinta métrica', N'Plástico y Metal', 60, 2),
    (9, N'15', N'Nivel de burbuja', N'Plástico y Metal', 80, 1),
    (10, N'45', N'Cepillo de carpintero', N'Madera y Metal', 120, 2),
    (11, N'30', N'Multímetro digital', N'Plástico y Metal', 200, 3),
    (12, N'50', N'Compresor de aire', N'Metal y Goma', 600, 2),
    (13, N'40', N'Soldador eléctrico', N'Plástico y Metal', 250, 1),
    (14, N'15', N'Llave Allen', N'Acero', 70, 2),
    (15, N'35', N'Cortadora de azulejos', N'Metal y Plástico', 300, 3);

-- Deshabilitar la inserción manual de IDs
SET IDENTITY_INSERT [dbo].[Herramienta] OFF;

PRINT '15 Herramientas insertadas correctamente.';