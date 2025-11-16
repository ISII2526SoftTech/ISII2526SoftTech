-- Inserta ejemplos en dbo.CompraItem (ajusta comprarId y herramientaId según existan)
INSERT INTO dbo.CompraItem (cantidad, descripcion, compraId, precio, herramientaId)
VALUES
(3, N'una cinta', 1, CAST(60.00 AS DECIMAL(18,2)), 8),
(7, N'una llave', 1, CAST(150.00 AS DECIMAL(18,2)), 1),
(9, N'un martillo', 2, CAST(90.00 AS DECIMAL(18,2)), 2);