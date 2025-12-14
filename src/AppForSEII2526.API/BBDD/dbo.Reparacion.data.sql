-- 1. Declarar variables con los precios EXACTOS (Extraídos de la imagen)
DECLARE @P1  DECIMAL(10, 2) = 150.00; -- Llave inglesa
DECLARE @P2  DECIMAL(10, 2) = 90.00;  -- Martillo de uña
DECLARE @P3  DECIMAL(10, 2) = 45.00;  -- Destornillador
DECLARE @P4  DECIMAL(10, 2) = 75.00;  -- Alicates universales
DECLARE @P5  DECIMAL(10, 2) = 350.00; -- Taladro percutor
DECLARE @P6  DECIMAL(10, 2) = 110.00; -- Sierra de mano
DECLARE @P7  DECIMAL(10, 2) = 400.00; -- Lijadora eléctrica
DECLARE @P8  DECIMAL(10, 2) = 60.00;  -- Cinta métrica
DECLARE @P9  DECIMAL(10, 2) = 80.00;  -- Nivel de burbuja
DECLARE @P10 DECIMAL(10, 2) = 120.00; -- Cepillo de carpintero
DECLARE @P11 DECIMAL(10, 2) = 200.00; -- Multímetro digital
DECLARE @P12 DECIMAL(10, 2) = 600.00; -- Compresor de aire
DECLARE @P13 DECIMAL(10, 2) = 250.00; -- Soldador eléctrico
DECLARE @P14 DECIMAL(10, 2) = 70.00;  -- Llave Allen
DECLARE @P15 DECIMAL(10, 2) = 300.00; -- Cortadora de azulejos

-- Variable para guardar el ID de la reparación actual
DECLARE @CurrentReparacionId INT;

-- ____________________________________________________________________
-- Reparacion 1 (BILLY - ID 1001) - 5 HERRAMIENTAS
-- Total calculado: 600 + 350 + 150 + 200 + 90 = 1390.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-10 00:00:00', '2025-11-05 00:00:00', '1001', 1390.00, 0
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [PrecioUnitario], [PrecioTotal]
)
VALUES
    -- Compresor (ID 12)
    (@CurrentReparacionId, 12, 1, 'Fuga de aire en válvula', @P12, (@P12 * 1)), 
    -- Taladro (ID 5)
    (@CurrentReparacionId, 5, 1, 'Motor huele a quemado', @P5, (@P5 * 1)),
    -- Llave inglesa (ID 1)
    (@CurrentReparacionId, 1, 1, 'Mecanismo atascado', @P1, (@P1 * 1)),
    -- Multímetro (ID 11)
    (@CurrentReparacionId, 11, 1, 'Pantalla rota', @P11, (@P11 * 1)),
    -- Martillo (ID 2)
    (@CurrentReparacionId, 2, 1, 'Mango astillado', @P2, (@P2 * 1));

-- ____________________________________________________________________
-- Reparacion 2 (SERGIO - ID 1002) - 4 HERRAMIENTAS
-- Total calculado: 400 + (120*2) + 110 + 80 = 830.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-12 00:00:00', '2025-11-06 00:00:00', '1002', 830.00, 1
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [PrecioUnitario], [PrecioTotal]
)
VALUES
    -- Lijadora (ID 7)
    (@CurrentReparacionId, 7, 1, 'Rodamiento ruidoso', @P7, (@P7 * 1)),
    -- Cepillo Carpintero (ID 10) -> Cantidad 2
    (@CurrentReparacionId, 10, 2, 'Base desnivelada', @P10, (@P10 * 2)),
    -- Sierra de mano (ID 6)
    (@CurrentReparacionId, 6, 1, 'Hoja doblada', @P6, (@P6 * 1)),
    -- Nivel (ID 9)
    (@CurrentReparacionId, 9, 1, 'Burbuja seca', @P9, (@P9 * 1));

-- ____________________________________________________________________
-- Reparacion 3 (MANUEL - ID 1003) - 3 HERRAMIENTAS (MIN)
-- Total calculado: 250 + (75*2) + (45*4) = 580.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-14 00:00:00', '2025-11-07 00:00:00', '1003', 580.00, 2
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [PrecioUnitario], [PrecioTotal]
)
VALUES
    -- Soldador (ID 13)
    (@CurrentReparacionId, 13, 1, 'No calienta punta', @P13, (@P13 * 1)),
    -- Alicates (ID 4) -> Cantidad 2
    (@CurrentReparacionId, 4, 2, 'Eje bloqueado por óxido', @P4, (@P4 * 2)),
    -- Destornillador (ID 3) -> Cantidad 4
    (@CurrentReparacionId, 3, 4, 'Puntas melladas lote entero', @P3, (@P3 * 4));

-- ____________________________________________________________________
-- Reparacion 4 (ANTONIO - ID 1004) - 6 HERRAMIENTAS
-- Total calculado: 300 + 70 + 60 + 150 + 90 + 75 = 745.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-15 00:00:00', '2025-11-08 00:00:00', '1004', 745.00, 0
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [PrecioUnitario], [PrecioTotal]
)
VALUES
    -- Cortadora (ID 15)
    (@CurrentReparacionId, 15, 1, 'Disco no gira', @P15, (@P15 * 1)),
    -- Llave Allen (ID 14)
    (@CurrentReparacionId, 14, 1, 'Redondeada', @P14, (@P14 * 1)),
    -- Cinta metrica (ID 8)
    (@CurrentReparacionId, 8, 1, 'Muelle de retorno roto', @P8, (@P8 * 1)),
    -- Llave inglesa (ID 1)
    (@CurrentReparacionId, 1, 1, 'Mellada en boca', @P1, (@P1 * 1)),
    -- Martillo (ID 2)
    (@CurrentReparacionId, 2, 1, 'Cabeza suelta', @P2, (@P2 * 1)),
    -- Alicates (ID 4)
    (@CurrentReparacionId, 4, 1, 'Goma mango suelta', @P4, (@P4 * 1));

-- ____________________________________________________________________
-- Reparacion 5 (BILLY - ID 1001) - 7 HERRAMIENTAS (MAX)
-- Total calculado: 350 + 200 + 250 + (45*2) + (70*2) + 80 + 110 = 1220.00
-- ____________________________________________________________________
INSERT INTO [Reparacion] (
    [FechaEntrega], [FechaRecogida], [ApplicationUserId], [PrecioTotal], [metodoPago]
)
VALUES (
    '2025-11-18 00:00:00', '2025-11-10 00:00:00', '1001', 1220.00, 1
);

SET @CurrentReparacionId = SCOPE_IDENTITY();

INSERT INTO [ReparacionItem] (
    [ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [PrecioUnitario], [PrecioTotal]
)
VALUES
    -- Taladro (ID 5)
    (@CurrentReparacionId, 5, 1, 'Cambio de escobillas', @P5, (@P5 * 1)),
    -- Multímetro (ID 11)
    (@CurrentReparacionId, 11, 1, 'No mide continuidad', @P11, (@P11 * 1)),
    -- Soldador (ID 13)
    (@CurrentReparacionId, 13, 1, 'Cable alimentación cortado', @P13, (@P13 * 1)),
    -- Destornillador (ID 3) -> Cantidad 2
    (@CurrentReparacionId, 3, 2, 'Afilado puntas', @P3, (@P3 * 2)),
    -- Llave Allen (ID 14) -> Cantidad 2
    (@CurrentReparacionId, 14, 2, 'Juego incompleto', @P14, (@P14 * 2)),
    -- Nivel (ID 9)
    (@CurrentReparacionId, 9, 1, 'Golpe en estructura', @P9, (@P9 * 1)),
    -- Sierra (ID 6)
    (@CurrentReparacionId, 6, 1, 'Mango roto', @P6, (@P6 * 1));