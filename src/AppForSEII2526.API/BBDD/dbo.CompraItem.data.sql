-- Inserta ejemplos en dbo.CompraItem (ajusta comprarId y herramientaId según existan)
INSERT INTO dbo.CompraItem (cantidad, descripcion, compraId, precio, herramientaId)
VALUES
(3, N'una cinta', 1, CAST(60.00 AS DECIMAL(18,2)), 4),
(7, N'una llave', 2, CAST(150.00 AS DECIMAL(18,2)), 1),
(9, N'un martillo', 3, CAST(90.00 AS DECIMAL(18,2)), 2);