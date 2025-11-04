SET IDENTITY_INSERT [dbo].[Herramienta] ON
INSERT INTO [dbo].[Herramienta] ([Id], [TiempoReparacion], [Nombre], [Material], [Precio], [FabricanteId]) VALUES (NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Herramienta] OFF
INSERT INTO [Herramienta] (TiempoReparacion, Nombre, Material, Precio, FabricanteId)
VALUES (60, 'Tornillo de banco', 'Acero', 150.99, 10);