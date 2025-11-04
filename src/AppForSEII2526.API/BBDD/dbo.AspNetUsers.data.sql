INSERT INTO [AspNetUsers] (
    [Id], 
    [NombreCliente], 
    [ApellidoCliente], 
    [CorreoElectronico], 
    [Telefono], 
    [UserName], 
    [NormalizedUserName], 
    [Email], 
    [NormalizedEmail], 
    [EmailConfirmed], 
    [PasswordHash], 
    [SecurityStamp], 
    [ConcurrencyStamp], 
    [PhoneNumber], 
    [PhoneNumberConfirmed], 
    [TwoFactorEnabled], 
    [LockoutEnd], 
    [LockoutEnabled], 
    [AccessFailedCount]
)
VALUES 
(
    -- Usuario 1: Juan Pérez (Datos completos)
    NEWID(), -- Genera un Id único
    'Juan', 
    'Pérez', 
    'juan.perez@ejemplo.com', -- Tu campo personalizado
    '611222333',               -- Tu campo personalizado
    'juan.perez@ejemplo.com',  -- UserName (Identity)
    'JUAN.PEREZ@EJEMPLO.COM',  -- NormalizedUserName
    'juan.perez@ejemplo.com',  -- Email (Identity)
    'JUAN.PEREZ@EJEMPLO.COM',  -- NormalizedEmail
    1, -- EmailConfirmed (true)
    'AQAAAAIAAYagAAAAEACl9R3ToteMvNYKx9T+b/nN/n1/f8oHfboAnfQnF/E0ge1xGsdT+IIa2DPCIivMsw==', -- Hash para "P@ssword1!"
    NEWID(), -- SecurityStamp
    NEWID(), -- ConcurrencyStamp
    '611222333', -- PhoneNumber (Identity)
    0, -- PhoneNumberConfirmed (false)
    0, -- TwoFactorEnabled (false)
    NULL, -- LockoutEnd (no bloqueado)
    1, -- LockoutEnabled (true)
    0  -- AccessFailedCount
),
(
    -- Usuario 2: María García (Datos completos)
    NEWID(), 
    'María', 
    'García', 
    'maria.garcia@ejemplo.com',
    '644555666',
    'maria.garcia@ejemplo.com',
    'MARIA.GARCIA@EJEMPLO.COM',
    'maria.garcia@ejemplo.com',
    'MARIA.GARCIA@EJEMPLO.COM',
    1, 
    'AQAAAAIAAYagAAAAEACl9R3ToteMvNYKx9T+b/nN/n1/f8oHfboAnfQnF/E0ge1xGsdT+IIa2DPCIivMsw==', -- Hash para "P@ssword1!"
    NEWID(), 
    NEWID(), 
    '644555666', 
    0, 
    0, 
    NULL, 
    1, 
    0
),
(
    -- Usuario 3: Carlos Rodríguez (Teléfono NULL)
    NEWID(), 
    'Carlos', 
    'Rodríguez', 
    'carlos.r@ejemplo.com',
    NULL, -- Teléfono (tu campo) es NULL
    'carlos.r@ejemplo.com',
    'CARLOS.R@EJEMPLO.COM',
    'carlos.r@ejemplo.com',
    'CARLOS.R@EJEMPLO.COM',
    1, 
    'AQAAAAIAAYagAAAAEACl9R3ToteMvNYKx9T+b/nN/n1/f8oHfboAnfQnF/E0ge1xGsdT+IIa2DPCIivMsw==', -- Hash para "P@ssword1!"
    NEWID(), 
    NEWID(), 
    NULL, -- PhoneNumber (Identity) es NULL
    0, 
    0, 
    NULL, 
    1, 
    0
),
(
    -- Usuario 4: Ana Martínez (Email no confirmado)
    NEWID(), 
    'Ana', 
    'Martínez', 
    'ana.martinez@ejemplo.com',
    '677888999',
    'ana.martinez@ejemplo.com',
    'ANA.MARTINEZ@EJEMPLO.COM',
    'ana.martinez@ejemplo.com',
    'ANA.MARTINEZ@EJEMPLO.COM',
    0, -- EmailConfirmed (false)
    'AQAAAAIAAYagAAAAEACl9R3ToteMvNYKx9T+b/nN/n1/f8oHfboAnfQnF/E0ge1xGsdT+IIa2DPCIivMsw==', -- Hash para "P@ssword1!"
    NEWID(), 
    NEWID(), 
    '677888999', 
    0, 
    0, 
    NULL, 
    1, 
    0
),
(
    -- Usuario 5: David López (CorreoElectrónico NULL)
    NEWID(), 
    'David', 
    'López', 
    NULL, -- CorreoElectronico (tu campo) es NULL
    '600111222',
    'david.lopez@ejemplo.com',
    'DAVID.LOPEZ@EJEMPLO.COM',
    'david.lopez@ejemplo.com',
    'DAVID.LOPEZ@EJEMPLO.COM',
    1, 
    'AQAAAAIAAYagAAAAEACl9R3ToteMvNYKx9T+b/nN/n1/f8oHfboAnfQnF/E0ge1xGsdT+IIa2DPCIivMsw==', -- Hash para "P@ssword1!"
    NEWID(), 
    NEWID(), 
    '600111222', 
    0, 
    0, 
    NULL, 
    1, 
    0
);