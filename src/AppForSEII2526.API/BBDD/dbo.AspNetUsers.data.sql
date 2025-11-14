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
    -- Usuario 1: Juan Pérez (Id = 1001)
    '1001', -- Id modificado
    'Juan', 
    'Pérez', 
    'juan.perez@ejemplo.com',
    '611222333',
    'juan.perez@ejemplo.com', 
    'JUAN.PEREZ@EJEMPLO.COM', 
    'juan.perez@ejemplo.com', 
    'JUAN.PEREZ@EJEMPLO.COM', 
    1, -- EmailConfirmed (true)
    'AQAAAAIAAYagAAAAEACl9R3ToteMvNYKx9T+b/nN/n1/f8oHfboAnfQnF/E0ge1xGsdT+IIa2DPCIivMsw==', -- Hash para "P@ssword1!"
    NEWID(), -- SecurityStamp (es mejor dejar que se genere uno nuevo)
    NEWID(), -- ConcurrencyStamp (es mejor dejar que se genere uno nuevo)
    '611222333', 
    0, -- PhoneNumberConfirmed (false)
    0, -- TwoFactorEnabled (false)
    NULL, -- LockoutEnd (no bloqueado)
    1, -- LockoutEnabled (true)
    0  -- AccessFailedCount
),
(
    -- Usuario 2: María García (Id = 1002)
    '1002', -- Id modificado
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
    -- Usuario 3: Carlos Rodríguez (Id = 1003)
    '1003', -- Id modificado
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
    -- Usuario 4: Ana Martínez (Id = 1004)
    '1004', -- Id modificado
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
    -- Usuario 5: David López (Id = 1005)
    '1005', -- Id modificado
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