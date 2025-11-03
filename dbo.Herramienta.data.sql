SET IDENTITY_INSERT [dbo].[Herramienta] ON
INSERT INTO [dbo].[Herramienta] ([Id], [TiempoReparacion], [Nombre], [Material], [Precio], [FabricanteId]) VALUES (1, N'30', N'Llave inglesa', N'Acero', 150, 1)
SET IDENTITY_INSERT [dbo].[Herramienta] OFF
INSERT INTO Herramientas (Id, TiempoReparacion, Nombre, Material, Precio, FabricanteId)
VALUES
(2, 15, 'Martillo de uña', 'Acero y Goma', 90, 2),
(3, 5, 'Destornillador de estrella', 'Acero y Plástico', 45, 1),
(4, 10, 'Alicates universales', 'Acero', 75, 2),
(5, 60, 'Taladro percutor', 'Plástico y Metal', 350, 3),
(6, 20, 'Sierra de mano', 'Acero y Madera', 110, 1);