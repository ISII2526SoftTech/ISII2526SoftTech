INSERT INTO [dbo].[AspNetUsers] ([Id], [NombreCliente], [ApellidoCliente], [CorreoElectronico], [Telefono], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
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
    -- Usuario 1: Billy Chalabi
    '1001', -- Id
    'Billy', 
    'Chalabi', 
    'billy.chalabi@ejemplo.com', -- Tu campo personalizado
    '611222333',                -- Tu campo personalizado
    'billy.chalabi@ejemplo.com', -- UserName (Identity)
    'BILLY.CHALABI@EJEMPLO.COM', -- NormalizedUserName
    'billy.chalabi@ejemplo.com', -- Email (Identity)
    'BILLY.CHALABI@EJEMPLO.COM', -- NormalizedEmail
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
    -- Usuario 2: Sergio Sanchez
    '1002', 
    'Sergio', 
    'Sanchez', 
    'sergio.sanchez@ejemplo.com',
    '644555666',
    'sergio.sanchez@ejemplo.com',
    'SERGIO.SANCHEZ@EJEMPLO.COM',
    'sergio.sanchez@ejemplo.com',
    'SERGIO.SANCHEZ@EJEMPLO.COM',
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
    -- Usuario 3: Manuel Castano (sin tilde)
    '1003', 
    'Manuel', 
    'Castano', 
    'manuel.castano@ejemplo.com',
    NULL, -- Teléfono (tu campo) es NULL
    'manuel.castano@ejemplo.com',
    'MANUEL.CASTANO@EJEMPLO.COM',
    'manuel.castano@ejemplo.com',
    'MANUEL.CASTANO@EJEMPLO.COM',
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
    -- Usuario 4: Antonio Recio
    '1004', 
    'Antonio', 
    'Recio', 
    'antonio.recio@ejemplo.com',
    '677888999',
    'antonio.recio@ejemplo.com',
    'ANTONIO.RECIO@EJEMPLO.COM',
    'antonio.recio@ejemplo.com',
    'ANTONIO.RECIO@EJEMPLO.COM',
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
    -- Usuario 5: Amador Rivas
    '1005', 
    'Amador', 
    'Rivas', 
    NULL, -- CorreoElectronico (tu campo) es NULL
    '600111222',
    'amador.rivas@ejemplo.com', -- Asignado nuevo UserName/Email
    'AMADOR.RIVAS@EJEMPLO.COM',
    'amador.rivas@ejemplo.com',
    'AMADOR.RIVAS@EJEMPLO.COM',
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