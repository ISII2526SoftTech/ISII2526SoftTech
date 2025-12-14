SET IDENTITY_INSERT [dbo].[Fabricante] ON
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (4, N'Arcos')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (2, N'Bosh')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (1, N'FABRICANTE2')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (5, N'Man')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (3, N'Union')
SET IDENTITY_INSERT [dbo].[Fabricante] OFF
SET IDENTITY_INSERT [dbo].[Herramienta] ON
INSERT INTO [dbo].[Herramienta] ([Id], [TiempoReparacion], [Nombre], [Material], [Precio], [FabricanteId]) VALUES (1, N'12', N'destornillador', N'hierro', 12, 1)
INSERT INTO [dbo].[Herramienta] ([Id], [TiempoReparacion], [Nombre], [Material], [Precio], [FabricanteId]) VALUES (2, N'12', N'martillo', N'hierro', 15, 1)
INSERT INTO [dbo].[Herramienta] ([Id], [TiempoReparacion], [Nombre], [Material], [Precio], [FabricanteId]) VALUES (3, N'1 semana', N'Taladro', N'metal', 100, 5)
INSERT INTO [dbo].[Herramienta] ([Id], [TiempoReparacion], [Nombre], [Material], [Precio], [FabricanteId]) VALUES (4, N'2 días', N'Sierra', N'Acero', 150, 5)
SET IDENTITY_INSERT [dbo].[Herramienta] OFF