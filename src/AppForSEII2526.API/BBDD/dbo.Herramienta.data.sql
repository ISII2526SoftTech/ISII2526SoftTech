<<<<<<< HEAD
﻿-- 1. Habilitar la inserción de IDs manualmente
SET IDENTITY_INSERT [dbo].[Herramienta] ON;

-- 2. Insertar TODOS los registros en una sola operación
=======
﻿-- --------------------------------------------------
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
>>>>>>> origin/development
INSERT INTO [dbo].[Herramienta] (
    [Id], 
    [TiempoReparacion], 
    [Nombre], 
    [Material], 
    [Precio], 
    [FabricanteId]
)
VALUES 
<<<<<<< HEAD
    (1, N'30', N'Llave inglesa', N'Acero', 150, 1),
=======
    (1, 30, N'Llave inglesa', N'Acero', 150, 1),
>>>>>>> origin/development
    (2, 15, 'Martillo de uña', 'Acero y Goma', 90, 2),
    (3, 5, 'Destornillador de estrella', 'Acero y Plástico', 45, 1),
    (4, 10, 'Alicates universales', 'Acero', 75, 2),
    (5, 60, 'Taladro percutor', 'Plástico y Metal', 350, 3),
    (6, 20, 'Sierra de mano', 'Acero y Madera', 110, 1),
    (7, 60, 'Lijadora eléctrica', 'Plástico y Metal', 400, 3),
    (8, 25, 'Cinta métrica', 'Plástico y Metal', 60, 2),
    (9, 15, 'Nivel de burbuja', 'Plástico y Metal', 80, 1),
    (10, 45, 'Cepillo de carpintero', 'Madera y Metal', 120, 2),
    (11, 30, 'Multímetro digital', 'Plástico y Metal', 200, 3),
    (12, 50, 'Compresor de aire', 'Metal y Goma', 600, 2),
    (13, 40, 'Soldador eléctrico', 'Plástico y Metal', 250, 1),
    (14, 15, 'Llave Allen', 'Acero', 70, 2),
    (15, 35, 'Cortadora de azulejos', 'Metal y Plástico', 300, 3);

<<<<<<< HEAD
-- 3. Deshabilitar la inserción manual de IDs
SET IDENTITY_INSERT [dbo].[Herramienta] OFF;
=======
-- Deshabilitar la inserción manual de IDs
SET IDENTITY_INSERT [dbo].[Herramienta] OFF;

PRINT '15 Herramientas insertadas correctamente.';
>>>>>>> origin/development
