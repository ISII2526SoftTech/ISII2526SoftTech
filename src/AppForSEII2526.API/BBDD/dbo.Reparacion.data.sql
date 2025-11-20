-- 1. Declarar variables para los precios unitarios
DECLARE @PrecioH1 DECIMAL(10, 2) = 100.00; -- Precio Herramienta ID 1 ("Llave Inglesa")
DECLARE @PrecioH2 DECIMAL(10, 2) = 150.00; -- Precio Herramienta ID 2 ("Martillo de uña")
DECLARE @PrecioH3 DECIMAL(10, 2) = 80.00;  -- Precio Herramienta ID 3 ("Sierra de Madera")

-- Variable para guardar el ID de la reparación recién creada
DECLARE @CurrentReparacionId INT;

-- ____________________________________________________________________
-- Reparacion 1 (Asignada a BILLY - ID 1001) 
-- (Antes era Juan)
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-10 00:00:00', '2025-11-05 00:00:00', '1001', 1580.00, 0
);

-- Capturamos el ID
SET @CurrentReparacionId = SCOPE_IDENTITY();

-- Insertamos items
INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 2, 10, 'No funciona', (@PrecioH2 * 10)),
    (@CurrentReparacionId, 3, 1, 'Sin filo', (@PrecioH3 * 1));

-- ____________________________________________________________________
-- Reparacion 2 (Asignada a SERGIO - ID 1002)
-- (Antes era María)
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-12 00:00:00', '2025-11-06 00:00:00', '1002', 200.00, 1
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 1, 2, 'Oxidada', (@PrecioH1 * 2));

-- ____________________________________________________________________
-- Reparacion 3 (Asignada a MANUEL - ID 1003)
-- (Antes era Carlos)
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-14 00:00:00', '2025-11-07 00:00:00', '1003', 150.00, 2
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 2, 1, 'Mango roto', (@PrecioH2 * 1));

-- ____________________________________________________________________
-- Reparacion 4 (Asignada a ANTONIO - ID 1004)
-- (Antes era Ana)
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-15 00:00:00', '2025-11-08 00:00:00', '1004', 330.00, 0
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
-- Reparacion 5 (Asignada a BILLY - ID 1001)
-- (Antes era Juan de nuevo)
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-18 00:00:00', '2025-11-10 00:00:00', '1001', 400.00, 1
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio]
)
VALUES
    (@CurrentReparacionId, 3, 5, 'Afilado multiple', (@PrecioH3 * 5));