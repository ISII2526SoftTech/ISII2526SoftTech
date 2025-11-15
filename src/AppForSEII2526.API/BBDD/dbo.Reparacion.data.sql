-- 1. Declarar variables para los precios unitarios (AJUSTA ESTO a tus precios reales)
DECLARE @PrecioH1 DECIMAL(10, 2) = 100.00; -- Precio Herramienta ID 1 ("Llave Inglesa")
DECLARE @PrecioH2 DECIMAL(10, 2) = 150.00; -- Precio Herramienta ID 2 ("Martillo de uña")
DECLARE @PrecioH3 DECIMAL(10, 2) = 80.00;  -- Precio Herramienta ID 3 ("Sierra de Madera")

-- 2. Buscar los IDs reales de la tabla AspNetUsers (los usuarios que me pasaste)
DECLARE @UserIdJuan NVARCHAR(450), @UserIdMaria NVARCHAR(450), @UserIdCarlos NVARCHAR(450), @UserIdAna NVARCHAR(450);
SELECT @UserIdJuan = Id FROM AspNetUsers WHERE UserName = 'juan.perez@ejemplo.com';
SELECT @UserIdMaria = Id FROM AspNetUsers WHERE UserName = 'maria.garcia@ejemplo.com';
SELECT @UserIdCarlos = Id FROM AspNetUsers WHERE UserName = 'carlos.r@ejemplo.com';
SELECT @UserIdAna = Id FROM AspNetUsers WHERE UserName = 'ana.martinez@ejemplo.com';

-- Variable para guardar el ID de la reparación recién creada
DECLARE @CurrentReparacionId INT;

-- ____________________________________________________________________
-- Reparacion 1 (Juan) - CON DOS ITEMS
-- Item 1: 10 del Martillo (ID 2) -> 10 * 150 = 1500
-- Item 2: 1 de la Sierra (ID 3)  -> 1 * 80 = 80
-- PRECIO TOTAL: 1580.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-10 00:00:00', '2025-11-05 00:00:00', @UserIdJuan, 1580.00, 0
);

-- Capturamos el ID de la Reparación que acabamos de crear
SET @CurrentReparacionId = SCOPE_IDENTITY();

-- Insertamos sus items
INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    -- Item 1 (como tu captura)
    (@CurrentReparacionId, 2, 10, 'No funciona', (@PrecioH2 * 10)),
    -- Item 2 (la "combinación" que pedías)
    (@CurrentReparacionId, 3, 1, 'Sin filo', (@PrecioH3 * 1));

-- ____________________________________________________________________
-- Reparacion 2 (Maria) - CON UN ITEM
-- Item 1: 2 de la Llave (ID 1) -> 2 * 100 = 200
-- PRECIO TOTAL: 200.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-12 00:00:00', '2025-11-06 00:00:00', @UserIdMaria, 200.00, 1
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 1, 2, 'Oxidada', (@PrecioH1 * 2));

-- ____________________________________________________________________
-- Reparacion 3 (Carlos) - CON UN ITEM
-- Item 1: 1 del Martillo (ID 2) -> 1 * 150 = 150
-- PRECIO TOTAL: 150.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-14 00:00:00', '2025-11-07 00:00:00', @UserIdCarlos, 150.00, 2
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 2, 1, 'Mango roto', (@PrecioH2 * 1));

-- ____________________________________________________________________
-- Reparacion 4 (Ana) - CON TRES ITEMS
-- Item 1: 1 de la Llave (ID 1) -> 1 * 100 = 100
-- Item 2: 1 del Martillo (ID 2) -> 1 * 150 = 150
-- Item 3: 1 de la Sierra (ID 3)  -> 1 * 80 = 80
-- PRECIO TOTAL: 330.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-15 00:00:00', '2025-11-08 00:00:00', @UserIdAna, 330.00, 0
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 1, 1, 'Mellada', (@PrecioH1 * 1)),
    (@CurrentReparacionId, 2, 1, 'Desgastado', (@PrecioH2 * 1)),
    (@CurrentReparacionId, 3, 1, 'Mella en hoja', (@PrecioH3 * 1));

-- ____________________________________________________________________
-- Reparacion 5 (Juan, otra vez) - CON UN ITEM
-- Item 1: 5 de la Sierra (ID 3) -> 5 * 80 = 400
-- PRECIO TOTAL: 400.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-18 00:00:00', '2025-11-10 00:00:00', @UserIdJuan, 400.00, 1
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 3, 5, 'Afilado multiple', (@PrecioH3 * 5));