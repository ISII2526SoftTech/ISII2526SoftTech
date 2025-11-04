-- 1. Declarar variables para guardar los IDs (GUIDs) de los usuarios
DECLARE @UserIdJuan NVARCHAR(450);
DECLARE @UserIdMaria NVARCHAR(450);
DECLARE @UserIdCarlos NVARCHAR(450);
DECLARE @UserIdAna NVARCHAR(450);

-- 2. Buscar los IDs reales de la tabla AspNetUsers
SELECT @UserIdJuan = Id FROM AspNetUsers WHERE UserName = 'juan.perez@ejemplo.com';
SELECT @UserIdMaria = Id FROM AspNetUsers WHERE UserName = 'maria.garcia@ejemplo.com';
SELECT @UserIdCarlos = Id FROM AspNetUsers WHERE UserName = 'carlos.r@ejemplo.com';
SELECT @UserIdAna = Id FROM AspNetUsers WHERE UserName = 'ana.martinez@ejemplo.com';

-- 3. Insertar 5 reparaciones usando el formato ISO 'YYYY-MM-DD HH:MM:SS'
INSERT INTO [Reparacion] (
    [FechaEntrega],
    [FechaRecogida],
    [ApplicationUserId],
    [PrecioTotal],
    [metodoPago]
)
VALUES
(
    -- Reparacion 1 (Juan, Metodo 0)
    '2025-11-10 00:00:00', -- 10 de Noviembre de 2025
    '2025-11-05 00:00:00', -- 05 de Noviembre de 2025
    @UserIdJuan,
    150.75,
    0 
),
(
    -- Reparacion 2 (Maria, Metodo 1)
    '2025-11-12 00:00:00', -- 12 de Noviembre de 2025
    '2025-11-06 00:00:00', -- 06 de Noviembre de 2025
    @UserIdMaria,
    80.00,
    1
),
(
    -- Reparacion 3 (Carlos, Metodo 2)
    '2025-11-14 00:00:00', -- 14 de Noviembre de 2025
    '2025-11-07 00:00:00', -- 07 de Noviembre de 2025
    @UserIdCarlos,
    210.50,
    2
),
(
    -- Reparacion 4 (Ana, Metodo 0)
    '2025-11-15 00:00:00', -- 15 de Noviembre de 2025
    '2025-11-08 00:00:00', -- 08 de Noviembre de 2025
    @UserIdAna,
    45.20,
    0
),
(
    -- Reparacion 5 (Juan, otra vez, Metodo 1)
    '2025-11-18 00:00:00', -- 18 de Noviembre de 2025
    '2025-11-10 00:00:00', -- 10 de Noviembre de 2025
    @UserIdJuan,
    120.00,
    1
);