-- Inserta 3 compras en dbo.Comprar
-- Ajusta los nombres de columna o el valor de ApplicationUserId según tu esquema si difieren.

INSERT INTO dbo.Comprar (DireccionEnvio, FechaCompra, PrecioTotal, MetodoPago, ApplicationUserId)
VALUES
('Calle Falsa 123, Madrid', '2025-11-01 10:30:00', 125.50, 0, '2C89569B-D3D3-4A1F-BA0A-77655D60EFD9'); -- MetodoPago: 0 = TarjetaCredito

INSERT INTO dbo.Comprar (DireccionEnvio, FechaCompra, PrecioTotal, MetodoPago, ApplicationUserId)
VALUES
('Avenida Principal 45, Barcelona', '2025-11-05 15:45:00', 89.90, 1, '4CB29179-B757-461E-B63A-1EED1A360F22'); -- MetodoPago: 1 = PayPal

INSERT INTO dbo.Comprar (DireccionEnvio, FechaCompra, PrecioTotal, MetodoPago, ApplicationUserId)
VALUES
('Plaza Central 7, Valencia', '2025-11-10 09:15:00', 240.00, 2, '7602AD93-790C-438F-80FC-6FAE47B1018B'); -- MetodoPago: 2 = Efectivo

-- Si la columna de FK hacia el usuario tiene otro nombre (por ejemplo ApplicationUser_Id), cambia ApplicationUserId por el nombre correcto.
-- Si dbo.Comprar.Id es identidad, las filas recibirán Id automáticamente. Si no, añade el valor de Id en la lista de columnas y en cada INSERT.